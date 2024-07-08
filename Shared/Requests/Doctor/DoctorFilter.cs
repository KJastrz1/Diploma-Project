namespace Shared.Requests.Doctor;

public class DoctorFilter
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? MedicalLicenseNumber { get; set; }
    public string? Specialty { get; set; }
    public string? OfficeNumber { get; set; }
    public Guid? ClinicId { get; set; }
}
