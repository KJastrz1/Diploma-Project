using Shared.Requests.Patient;
using Shared.Responses.Patient;
using Shared.Models;

namespace Backend.Services;

public interface IPatientsService
{
    Task<PagedResult<GetPatientResponse>> GetPatientsAsync(PatientFilter filter, int pageNumber, int pageSize);
    Task<GetPatientResponse?> GetPatientByIdAsync(Guid id);
    Task<GetPatientResponse> CreatePatientAsync(CreatePatientRequest request);
    Task<GetPatientResponse> UpdatePatientAsync(Guid id, UpdatePatientRequest request);
    Task<bool> DeletePatientAsync(Guid id);
}

