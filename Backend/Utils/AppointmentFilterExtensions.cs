using Shared.Models;
using Shared.Requests.Appointment;

namespace Backend.Utils;

public static class AppointmentFilterExtensions
{
    public static IQueryable<Appointment> ApplyFilter(this IQueryable<Appointment> query, AppointmentFilter filter)
    {
        if (filter == null)
            return query;

        if (filter.ClinicId.HasValue)
        {
            query = query.Where(a => a.ClinicId == filter.ClinicId.Value);
        }

        if (filter.DoctorId.HasValue)
        {
            query = query.Where(a => a.DoctorId == filter.DoctorId.Value);
        }

        if (filter.PatientId.HasValue)
        {
            query = query.Where(a => a.PatientId == filter.PatientId.Value);
        }

        if (filter.StartRange.HasValue)
        {
            query = query.Where(a => a.AppointmentDate >= filter.StartRange.Value);
        }

        if (filter.EndRange.HasValue)
        {
            query = query.Where(a => a.AppointmentDate <= filter.EndRange.Value);
        }

        return query;
    }
}
