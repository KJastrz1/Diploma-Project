using System.Linq.Expressions;
using Shared.Entities;
using Shared.Requests.Doctor;

namespace Backend.Utils;
public static class DoctorFilterExtensions
{
    public static IQueryable<Doctor> ApplyFilter(this IQueryable<Doctor> query, DoctorFilter filter)
    {
        if (filter == null)
            return query;

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(d => d.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrEmpty(filter.Surname))
        {
            query = query.Where(d => d.Surname.Contains(filter.Surname));
        }

        if (!string.IsNullOrEmpty(filter.Email))
        {
            query = query.Where(d => d.Email.Contains(filter.Email));
        }

        if (!string.IsNullOrEmpty(filter.MedicalLicenseNumber))
        {
            query = query.Where(d => d.MedicalLicenseNumber.Contains(filter.MedicalLicenseNumber));
        }

        if (!string.IsNullOrEmpty(filter.Specialty))
        {
            query = query.Where(d => d.Specialty.Contains(filter.Specialty));
        }

        if (!string.IsNullOrEmpty(filter.OfficeNumber))
        {
            query = query.Where(d => d.OfficeNumber.Contains(filter.OfficeNumber));
        }

        if (filter.ClinicId.HasValue)
        {
            query = query.Where(d => d.ClinicId == filter.ClinicId);
        }

        return query;
    }
}