using AutoMapper;
using Backend.Data;
using Backend.Utils;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Requests.Doctor;
using Shared.Responses.Doctor;


namespace Backend.Services;
public interface IDoctorsService
{
    Task<PagedResult<GetDoctorResponse>> GetDoctorsAsync(DoctorFilter filter, int pageNumber, int pageSize);
    Task<GetDoctorResponse?> GetDoctorByIdAsync(Guid id);
    Task<GetDoctorResponse> CreateDoctorAsync(CreateDoctorRequest request);
    Task<GetDoctorResponse> UpdateDoctorAsync(Guid id, UpdateDoctorRequest request);
    Task<bool> DeleteDoctorAsync(Guid id);
}

public class DoctorsService : IDoctorsService
{
    private readonly ClinicDataContext _context;
    private readonly IMapper _mapper;

    public DoctorsService(ClinicDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetDoctorResponse>> GetDoctorsAsync(DoctorFilter filter, int pageNumber, int pageSize)
    {
        var query = _context.Doctors.AsQueryable()
            .ApplyFilter(filter);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var mappedItems = _mapper.Map<List<GetDoctorResponse>>(items);

        return new PagedResult<GetDoctorResponse>
        {
            Items = mappedItems,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }

    public async Task<GetDoctorResponse?> GetDoctorByIdAsync(Guid id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
        {
            return null;
        }
        GetDoctorResponse response = _mapper.Map<GetDoctorResponse>(doctor);
        return response;
    }

    public async Task<GetDoctorResponse> CreateDoctorAsync(CreateDoctorRequest request)
    {
        var clinicExists = await _context.Clinics.AnyAsync(c => c.Id == request.ClinicId);
        if (!clinicExists)
        {
            throw new Exception("Specified clinic does not exist.");
        }

        Doctor doctor = _mapper.Map<Doctor>(request);

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        GetDoctorResponse response = _mapper.Map<GetDoctorResponse>(doctor);
        return response;
    }


    public async Task<GetDoctorResponse> UpdateDoctorAsync(Guid id, UpdateDoctorRequest request)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
        {
            return null;
        }

        if (request.ClinicId.HasValue)
        {
            var clinicExists = await _context.Clinics.AnyAsync(c => c.Id == request.ClinicId.Value);
            if (!clinicExists)
            {
                throw new Exception("Specified clinic does not exist.");
            }
        }

        _mapper.Map(request, doctor);

        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();

        GetDoctorResponse response = _mapper.Map<GetDoctorResponse>(doctor);

        return response;
    }


    public async Task<bool> DeleteDoctorAsync(Guid id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
        {
            return false;
        }

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();

        return true;
    }
}
