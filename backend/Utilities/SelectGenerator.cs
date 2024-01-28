using System.Linq.Expressions;
using System.Reflection;
using backend.Utilities.Extensions;

namespace backend.Utilities
{
    public static class SelectGenerator
    {
        public static Expression<Func<TObject, TProperty>> GeneratePropertiesExpression<TObject, TProperty>(string field) {
            ParameterExpression parameter = Expression.Parameter(typeof(TObject), "x");
            MemberExpression property = Expression.Property(parameter, field.ToPascalCase());
            Expression convertedProperty = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TObject, TProperty>>(convertedProperty, parameter);
        }

        public static Expression<Func<T, T>> GeneratePropertiesExpression<T>(string[] fields)
        {
            var parameter = Expression.Parameter(typeof(T), "field");
            var returnValue = Expression.New(typeof(T));

            var bindings = fields.Select(field => field.Trim().ToPascalCase())
                                 .Select(field => {
                PropertyInfo? propertyInfo = typeof(T).GetProperty(field);
                if (propertyInfo == null) throw new ArgumentException($"Property '{field}' is not valid");

                var xOriginal = Expression.Property(parameter, propertyInfo);
                return Expression.Bind(propertyInfo, xOriginal);
            });

            var xInit = Expression.MemberInit(returnValue, bindings);    
            var lambda = Expression.Lambda<Func<T, T>>(xInit, parameter);    
            return lambda;
        }
    }
}