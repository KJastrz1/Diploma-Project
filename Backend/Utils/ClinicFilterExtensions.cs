using System.Linq.Expressions;
using Shared.Models;
using Shared.Requests.Clinic;

namespace Backend.Utils;

public static class ClinicFilterExtensions
{
    public static IQueryable<Clinic> ApplyFilter(this IQueryable<Clinic> query, ClinicFilter filter)
    {
        if (filter == null)
            return query;

        foreach (var property in typeof(ClinicFilter).GetProperties())
        {
            var value = property.GetValue(filter);
            if (value == null || (property.PropertyType == typeof(string) && string.IsNullOrEmpty(value as string)))
                continue;

            var parameter = Expression.Parameter(typeof(Clinic), "c");
            var propertyAccess = Expression.Property(parameter, property.Name);
            var constant = Expression.Constant(value);

            Expression condition;
            if (property.PropertyType == typeof(string))
            {
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                condition = Expression.Call(propertyAccess, containsMethod, constant);
            }
            else
            {
                condition = Expression.Equal(propertyAccess, constant);
            }

            var lambda = Expression.Lambda<Func<Clinic, bool>>(condition, parameter);
            query = query.Where(lambda);
        }

        return query;
    }
}
