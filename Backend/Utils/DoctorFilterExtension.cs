using System.Linq.Expressions;
using Shared.Models;
using Shared.Requests.Doctor;

namespace Backend.Utils;

public static class DoctorFilterExtensions
{
    public static IQueryable<Doctor> ApplyFilter(this IQueryable<Doctor> query, DoctorFilter filter)
    {
        if (filter == null)
            return query;

        foreach (var property in typeof(DoctorFilter).GetProperties())
        {
            var value = property.GetValue(filter);
            if (value == null || (property.PropertyType == typeof(string) && string.IsNullOrEmpty(value as string)))
                continue;

            var parameter = Expression.Parameter(typeof(Doctor), "d");
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

            var lambda = Expression.Lambda<Func<Doctor, bool>>(condition, parameter);
            query = query.Where(lambda);
        }

        if (!string.IsNullOrEmpty(filter.ClinicAddress))
        {
            var parameter = Expression.Parameter(typeof(Doctor), "d");
            var clinicPropertyAccess = Expression.Property(parameter, nameof(Doctor.Clinic));
            var addressPropertyAccess = Expression.Property(clinicPropertyAccess, nameof(Clinic.Address));
            var constant = Expression.Constant(filter.ClinicAddress);

            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var condition = Expression.Call(addressPropertyAccess, containsMethod, constant);

            var lambda = Expression.Lambda<Func<Doctor, bool>>(condition, parameter);
            query = query.Where(lambda);
        }

        return query;
    }
}
