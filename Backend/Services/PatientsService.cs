using AutoMapper;
using Backend.Data;
using Backend.Utils;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Requests.Patient;
using Shared.Responses;
using Shared.Responses.Patient;
using System.Threading.Tasks;

namespace Backend.Services;
public class PatientsService : IPatientsService
{
    private readonly ClinicDataContext _context;
    private readonly IMapper _mapper;

    public PatientsService(ClinicDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetPatientResponse>> GetPatientsAsync(PatientFilter filter, int pageNumber, int pageSize)
    {
        var query = _context.Patients.AsQueryable()
            .ApplyFilter(filter);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var mappedItems = _mapper.Map<List<GetPatientResponse>>(items);

        return new PagedResult<GetPatientResponse>
        {
            Items = mappedItems,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }

    public async Task<GetPatientResponse?> GetPatientByIdAsync(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return null;
        }
        GetPatientResponse response = _mapper.Map<GetPatientResponse>(patient);
        return response;
    }

    public async Task<GetPatientResponse> CreatePatientAsync(CreatePatientRequest request)
    {
        Patient patient = _mapper.Map<Patient>(request);

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        GetPatientResponse response = _mapper.Map<GetPatientResponse>(patient);
        return response;
    }

    public async Task<GetPatientResponse> UpdatePatientAsync(Guid id, UpdatePatientRequest request)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return null;
        }

        _mapper.Map(request, patient);

        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();

        GetPatientResponse response = _mapper.Map<GetPatientResponse>(patient);

        return response;
    }

    public async Task<bool> DeletePatientAsync(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return false;
        }

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return true;
    }
}
