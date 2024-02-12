using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using backend.Utilities.Extensions;

namespace backend.Utilities.Expressions
{
    public static class FilterExpressionGenerator<TArgument>
    {
        public static Expression<Func<TArgument, bool>> Truthy { get => (arg) => true; }

        private static readonly ParameterExpression parameter = Expression.Parameter(typeof(TArgument), "arg");

        private static readonly Dictionary<string, ExpressionOperationInfo> rpnOperationInfos = new() {
            [" and "] = new() { Cost = 1, Type = OperationType.Binary },
            [" or "] = new() { Cost = 1, Type = OperationType.Binary },
            ["not "] = new() { Cost = 2, Type = OperationType.Unary }
        };

        private static readonly string propertiesRegex = @"\w+(\.\w+)*(\s*[+\-*/]\s*\w+(\.\w+)*)*";

        private static readonly string expressionRegex = propertiesRegex
                                                         + @"\s+(\S+)\s+([0-9]{2}\.[0-9]{2}\.[0-9]{4}|\d+|'((?!').)*')";

        public static Expression<Func<TArgument, bool>> GenerateFilterExpression(string filterString) {
            if (filterString == String.Empty) return Truthy;
            return RpnExpressionComputer.Compute(
                filterString,
                rpnOperationInfos,
                expressionRegex,
                GenerateSimpleFilterExpression,
                (operatorType, operand1, operand2) => Expression.Lambda<Func<TArgument, bool>>(operatorType.Trim() switch {
                    "and" => Expression.AndAlso(operand1.Body, operand2.Body),
                    "or" => Expression.OrElse(operand1.Body, operand2.Body),
                    "not" => Expression.Not(operand1.Body),
                    _ => throw new ArgumentException($"Unknown operator '{operatorType}'")
                }, parameter)
            );
        }

        private static Expression<Func<TArgument, bool>> GenerateSimpleFilterExpression(string simpleExpression) {
            string value = "", propertyName = "";
            try {
                var propertyMatch = Regex.Match(simpleExpression, propertiesRegex);
                propertyName = propertyMatch.Value;

                string[] operatorAndValue = simpleExpression[(propertyMatch.Index + propertyMatch.Length)..]
                                       .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string operatorName = operatorAndValue[0].ToPascalCase();
                value = String.Join(' ', operatorAndValue[1..]);

                (Expression propertyAccess, Type propertyType) = PropertyExpressionGenerator<TArgument>
                                                                .ParseComplexProperty(propertyName, parameter);

                if (value.StartsWith("'") && value.EndsWith("'")) value = value[1..^1];
                var convertedValue = propertyType == typeof(DateOnly) ? 
                                     DateOnly.Parse(value, CultureInfo.CreateSpecificCulture("es-es"))
                                     : Convert.ChangeType(value, propertyType);
                var constant = Expression.Constant(convertedValue, propertyType);

                Expression operation = operatorName switch
                {
                    "==" => Expression.Equal(propertyAccess, constant),
                    "!=" => Expression.NotEqual(propertyAccess, constant),
                    "<" => Expression.LessThan(propertyAccess, constant),
                    ">" => Expression.GreaterThan(propertyAccess, constant),
                    "<=" => Expression.LessThanOrEqual(propertyAccess, constant),
                    ">=" => Expression.GreaterThanOrEqual(propertyAccess, constant),
                    "Contains" or "StartsWith" or "EndsWith" => 
                    Expression.Call(propertyAccess, typeof(string).GetMethod(operatorName, [typeof(string)])!, constant),
                    _ => throw new ArgumentException($"Operator '{operatorName.ToCamelCase()}' is not supported"),
                };
                return Expression.Lambda<Func<TArgument, bool>>(operation, parameter);
            }
            catch(IndexOutOfRangeException) {
                throw new ArgumentException("Filter expressions should include property info, operator and value, separated by space");
            }
            catch(InvalidOperationException) {
                throw new ArgumentException(
                    $"Operation '{simpleExpression}' can't be done because of unmatched types or such operation is not supported for these properties"
                );
            }
            catch(FormatException) {
                throw new ArgumentException(
                    $"Value {value} can't be converted to properties' type"
                );    
            }
            catch (InvalidCastException) {
                throw new ArgumentException($"Conversion for type of field '{propertyName}' is not supported");
            }
        }
    }
}