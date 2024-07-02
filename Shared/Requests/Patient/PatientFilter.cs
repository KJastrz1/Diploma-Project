namespace Shared.Requests.Patient;
public class PatientFilter
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PESEL { get; set; }
    public DateTime? DateOfBirth { get; set; }
}

