using System.Linq.Expressions;

namespace backend.Utilities.Extensions
{
    public static class QueryableExtension
    {
        public static IOrderedQueryable<T> OrderBy<T>
        (
        this IQueryable<T> source, 
        Expression<Func<T, dynamic?>> expression,
        SortOrder order
        )
	    {
	    	return order == SortOrder.Descending ?
                   Queryable.OrderByDescending(source, expression) :
                   Queryable.OrderBy(source, expression);
	    }

	    public static IOrderedQueryable<T> ThenBy<T>
        (
        this IOrderedQueryable<T> source,
        Expression<Func<T, dynamic?>> expression,
        SortOrder order
        )
	    {
	    	return order == SortOrder.Descending ?
                   Queryable.ThenByDescending(source, expression) :
                   Queryable.ThenBy(source, expression);
	    }
    }
}