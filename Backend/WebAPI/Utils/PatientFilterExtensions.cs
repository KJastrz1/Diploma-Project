using System.Linq.Expressions;
using Shared.Entities;
using Shared.Requests.Patient;

namespace Backend.Utils;
public static class PatientFilterExtensions
{
    public static IQueryable<Patient> ApplyFilter(this IQueryable<Patient> query, PatientFilter filter)
    {
        if (filter == null)
            return query;

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(p => p.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrEmpty(filter.Surname))
        {
            query = query.Where(p => p.Surname.Contains(filter.Surname));
        }

        if (!string.IsNullOrEmpty(filter.Email))
        {
            query = query.Where(p => p.Email.Contains(filter.Email));
        }

        if (!string.IsNullOrEmpty(filter.PhoneNumber))
        {
            query = query.Where(p => p.PhoneNumber.Contains(filter.PhoneNumber));
        }

        if (!string.IsNullOrEmpty(filter.PESEL))
        {
            query = query.Where(p => p.PESEL.Contains(filter.PESEL));
        }

        if (filter.DateOfBirth.HasValue)
        {
            query = query.Where(p => p.DateOfBirth == filter.DateOfBirth);
        }

        return query;
    }
}