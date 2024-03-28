using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Backend.Application.Extensions;

namespace Backend.Infrastructure.Expressions;

public static class PropertyExpressionGenerator<TArgument>
{
    private static readonly string propertyRegex = @"\w+(\.\w+)*";

    public static (Expression, Type) ParseComplexProperty(
        string properties,
        ParameterExpression parameter
    )
    {
        int? prevIndex = null;
        (Expression, Type)? result = null;
        foreach (var propertyMatch in Regex.Matches(properties, propertyRegex).Cast<Match>())
        {
            var property = ParseSimpleProperty(propertyMatch.Value, parameter);
            result ??= property;

            if (prevIndex != null)
            {
                var operatorName = properties[prevIndex.Value..propertyMatch.Index];
                var currentPropertyExpression = result.Value.Item1;
                var incomingExpression = property.Item1;

                Expression newExpression = operatorName.Trim() switch
                {
                    "+"
                        => Expression.Add(
                            currentPropertyExpression,
                            incomingExpression,
                            result.Value.Item2 == typeof(string)
                                ? typeof(string).GetMethod(
                                    "Concat",
                                    [typeof(string), typeof(string)]
                                )
                                : null
                        ),
                    "-" => Expression.Subtract(currentPropertyExpression, incomingExpression),
                    "*" => Expression.Multiply(currentPropertyExpression, incomingExpression),
                    "/" => Expression.Divide(currentPropertyExpression, incomingExpression),
                    _
                        => throw new ArgumentException(
                            $"Operator {operatorName} is not supported"
                        )
                };
                result = (newExpression, property.Item2);
            }

            prevIndex = propertyMatch.Index + propertyMatch.Length;
        }

        if (result == null)
            throw new ArgumentException($"Invalid property expression {properties}");
        return result.Value;
    }

    private static (Expression, Type) ParseSimpleProperty(
        string propertiesReference,
        ParameterExpression parameter
    )
    {
        var references = propertiesReference
            .Split('.')
            .Select(property => property.ToPascalCase());
        (Expression, Type) initialValues = (parameter, typeof(TArgument));
        return references.Aggregate(
            initialValues,
            (accum, reference) =>
            {
                var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                var propertyInfo =
                    accum.Item2.GetProperty(reference, bindingFlags)
                    ?? throw new ArgumentException(
                        $"Could not find property '{propertiesReference}' on type '{typeof(TArgument).Name.ToCamelCase()}'"
                    );
                var propertyType = propertyInfo.PropertyType;
                Expression result = Expression.MakeMemberAccess(accum.Item1, propertyInfo);

                var possibleUnderlyingType = Nullable.GetUnderlyingType(propertyType);
                if (possibleUnderlyingType != null)
                {
                    result = Expression.Property(result, "Value");
                    propertyType = possibleUnderlyingType;
                }

                return (result, propertyType);
            }
        );
    }
}