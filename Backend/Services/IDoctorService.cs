using Shared.Requests.Doctor;
using Shared.Responses.Doctor;
using Shared.Models;

namespace Backend.Services;

public interface IDoctorsService
{
    Task<PagedResult<GetDoctorResponse>> GetDoctorsAsync(DoctorFilter filter, int pageNumber, int pageSize);
    Task<GetDoctorResponse?> GetDoctorByIdAsync(Guid id);
    Task<GetDoctorResponse> CreateDoctorAsync(CreateDoctorRequest request);
    Task<GetDoctorResponse> UpdateDoctorAsync(Guid id, UpdateDoctorRequest request);
    Task<bool> DeleteDoctorAsync(Guid id);
}
