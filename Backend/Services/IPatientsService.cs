using Shared.Requests.Patient;
using Shared.Models;

namespace Backend.Services;

public interface IPatientsService
{
    Task<Patient> CreatePatientAsync(CreatePatientRequest request);
    Task<Patient?> GetPatientByIdAsync(Guid id);
}

