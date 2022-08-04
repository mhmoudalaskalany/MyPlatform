using System;
using System.Linq;
using System.Linq.Expressions;


namespace Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereEquals<T>(this IQueryable<T> source, string member, object value)
        {
            var item = Expression.Parameter(typeof(T), "item");
            var memberValue = member.Split('.').Aggregate((Expression)item, Expression.PropertyOrField);
            var memberType = memberValue.Type;
            if (value != null && value.GetType() != memberType)
                value = Convert.ChangeType(value, memberType);
            var condition = Expression.Equal(memberValue, Expression.Constant(value, memberType));
            var predicate = Expression.Lambda<Func<T, bool>>(condition, item);
            return source.Where(predicate);
        }
    }
}
