using System.Linq.Expressions;
using Shared.Models;
using Shared.Requests.Patient;

namespace Backend.Utils;

public static class PatientFilterExtensions
{
    public static IQueryable<Patient> ApplyFilter(this IQueryable<Patient> query, PatientFilter filter)
    {
        if (filter == null)
            return query;

        foreach (var property in typeof(PatientFilter).GetProperties())
        {
            var value = property.GetValue(filter);
            if (value == null || (property.PropertyType == typeof(string) && string.IsNullOrEmpty(value as string)))
                continue;

            var parameter = Expression.Parameter(typeof(Patient), "p");
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

            var lambda = Expression.Lambda<Func<Patient, bool>>(condition, parameter);
            query = query.Where(lambda);
        }

        return query;
    }
}
