using System.Linq.Expressions;
using Backend.Application.Enums;

namespace Backend.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TKey> OrderBy<TKey, TResult>(
            this IQueryable<TKey> source,
            Expression<Func<TKey, TResult>> expression,
            SortOrder order
        )
        {
            return order == SortOrder.Descending
                ? Queryable.OrderByDescending(source, expression)
                : Queryable.OrderBy(source, expression);
        }

        public static IOrderedQueryable<TKey> ThenBy<TKey, TResult>(
            this IOrderedQueryable<TKey> source,
            Expression<Func<TKey, TResult>> expression,
            SortOrder order
        )
        {
            return order == SortOrder.Descending
                ? Queryable.ThenByDescending(source, expression)
                : Queryable.ThenBy(source, expression);
        }
    }
}
