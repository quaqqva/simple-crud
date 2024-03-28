using System.Linq.Expressions;
using Backend.Application.Enums;

namespace Backend.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IOrderedQueryable<TKey> OrderBy<TKey, TResult>(
        this IQueryable<TKey> source,
        Expression<Func<TKey, TResult>> expression,
        SortOrder order
    )
    {
        return order == SortOrder.Descending
            ? source.OrderByDescending(expression)
            : source.OrderBy(expression);
    }

    public static IOrderedQueryable<TKey> ThenBy<TKey, TResult>(
        this IOrderedQueryable<TKey> source,
        Expression<Func<TKey, TResult>> expression,
        SortOrder order
    )
    {
        return order == SortOrder.Descending
            ? source.ThenByDescending(expression)
            : source.ThenBy(expression);
    }
}