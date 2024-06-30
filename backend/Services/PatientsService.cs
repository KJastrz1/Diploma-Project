using AutoMapper;
using Backend.Data;
using Shared.Models;
using Shared.Requests.Patient;

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

    public async Task<Patient> CreatePatientAsync(CreatePatientRequest request)
    {
        var patient = _mapper.Map<Patient>(request);
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient?> GetPatientByIdAsync(Guid id)
    {
        return await _context.Patients.FindAsync(id);
    }
}

