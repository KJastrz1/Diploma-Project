using System.Linq.Expressions;
using Shared.Entities;
using Shared.Requests.Clinic;

namespace Backend.Utils;
public static class ClinicFilterExtensions
{
    public static IQueryable<Clinic> ApplyFilter(this IQueryable<Clinic> query, ClinicFilter filter)
    {
        if (filter == null)
            return query;

        if (!string.IsNullOrEmpty(filter.Address))
        {
            query = query.Where(c => c.Address.Contains(filter.Address));
        }

        if (!string.IsNullOrEmpty(filter.PhoneNumber))
        {
            query = query.Where(c => c.PhoneNumber.Contains(filter.PhoneNumber));
        }

        return query;
    }
}
