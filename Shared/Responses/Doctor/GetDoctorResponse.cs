using Shared.Models;

namespace Shared.Responses.Doctor;

public class GetDoctorResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string MedicalLicenseNumber { get; set; }
    public string Specialty { get; set; }
    public string OfficeNumber { get; set; }
    public Guid ClinicId { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserRole Role { get; set; }
}
