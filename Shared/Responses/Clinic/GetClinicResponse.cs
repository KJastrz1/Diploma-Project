namespace Shared.Responses.Clinic;
public class GetClinicResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}
