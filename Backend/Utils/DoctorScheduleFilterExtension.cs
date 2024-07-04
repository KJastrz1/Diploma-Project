using System.Linq.Expressions;
using Shared.Models;
using Shared.Requests.DoctorSchedule;

namespace Backend.Utils;
public static class DoctorScheduleFilterExtensions
{
    public static IQueryable<DoctorSchedule> ApplyFilter(this IQueryable<DoctorSchedule> query, DoctorScheduleFilter filter)
    {
        if (filter == null)
            return query;

        query = query.Where(ds => ds.DoctorId == filter.DoctorId);

        if (filter.Day.HasValue)
        {
            query = query.Where(ds => ds.Day == filter.Day.Value);
        }

        return query;
    }
}