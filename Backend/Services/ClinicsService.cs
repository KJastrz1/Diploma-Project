using AutoMapper;
using Backend.Data;
using Backend.Utils;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Helpers;
using Shared.Requests.Clinic;
using Shared.Responses.Clinic;

namespace Backend.Services;
public interface IClinicsService
{
    Task<PagedResult<GetClinicResponse>> GetClinicsAsync(ClinicFilter filter, int pageNumber, int pageSize);
    Task<GetClinicResponse?> GetClinicByIdAsync(Guid id);
    Task<GetClinicResponse> CreateClinicAsync(CreateClinicRequest request);
    Task<GetClinicResponse> UpdateClinicAsync(Guid id, UpdateClinicRequest request);
    Task<bool> DeleteClinicAsync(Guid id);
}

public class ClinicsService : IClinicsService
{
    private readonly ClinicDataContext _context;
    private readonly IMapper _mapper;

    public ClinicsService(ClinicDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetClinicResponse>> GetClinicsAsync(ClinicFilter filter, int pageNumber, int pageSize)
    {
        var query = _context.Clinics.AsQueryable()
            .ApplyFilter(filter);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var mappedItems = _mapper.Map<List<GetClinicResponse>>(items);

        return new PagedResult<GetClinicResponse>
        {
            Items = mappedItems,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }

    public async Task<GetClinicResponse?> GetClinicByIdAsync(Guid id)
    {
        var clinic = await _context.Clinics.FindAsync(id);
        if (clinic == null)
        {
            return null;
        }
        GetClinicResponse response = _mapper.Map<GetClinicResponse>(clinic);
        return response;
    }

    public async Task<GetClinicResponse> CreateClinicAsync(CreateClinicRequest request)
    {
        Clinic clinic = _mapper.Map<Clinic>(request);

        _context.Clinics.Add(clinic);
        await _context.SaveChangesAsync();
        GetClinicResponse response = _mapper.Map<GetClinicResponse>(clinic);
        return response;
    }

    public async Task<GetClinicResponse> UpdateClinicAsync(Guid id, UpdateClinicRequest request)
    {
        var clinic = await _context.Clinics.FindAsync(id);
        if (clinic == null)
        {
            return null;
        }

        _mapper.Map(request, clinic);

        _context.Clinics.Update(clinic);
        await _context.SaveChangesAsync();

        GetClinicResponse response = _mapper.Map<GetClinicResponse>(clinic);

        return response;
    }

    public async Task<bool> DeleteClinicAsync(Guid id)
    {
        var clinic = await _context.Clinics.FindAsync(id);
        if (clinic == null)
        {
            return false;
        }

        _context.Clinics.Remove(clinic);
        await _context.SaveChangesAsync();

        return true;
    }
}


