using System.Linq.Expressions;
using Backend.Application.Extensions;

namespace Backend.Infrastructure.Expressions;

public static class SelectExpressionGenerator<TObject>
{
    public static Expression<Func<TObject, TProperty>> GenerateSelectExpression<TProperty>(
        string field
    )
    {
        var parameter = Expression.Parameter(typeof(TObject), "x");
        var property = Expression.Property(parameter, field.ToPascalCase());
        Expression convertedProperty = Expression.Convert(property, typeof(TProperty));
        return Expression.Lambda<Func<TObject, TProperty>>(convertedProperty, parameter);
    }

    public static Expression<Func<TObject, TObject>> GenerateSelectExpression(string[] fields)
    {
        var parameter = Expression.Parameter(typeof(TObject), "field");
        var returnValue = Expression.New(typeof(TObject));

        IEnumerable<MemberBinding> bindings = fields
            .Select(field => field.Trim().ToPascalCase())
            .Select(field =>
            {
                var propertyInfo =
                    typeof(TObject).GetProperty(field)
                    ?? throw new ArgumentException(
                        $"Property '{field.ToCamelCase()}' is not valid"
                    );
                var xOriginal = Expression.Property(parameter, propertyInfo);
                return Expression.Bind(propertyInfo, xOriginal);
            });

        var xInit = Expression.MemberInit(returnValue, bindings);
        var lambda = Expression.Lambda<Func<TObject, TObject>>(xInit, parameter);
        return lambda;
    }
}