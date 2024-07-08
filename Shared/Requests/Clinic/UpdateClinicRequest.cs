using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Clinic;
public class UpdateClinicRequest
{
    [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
    public string? Address { get; set; }

    [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
    public string? PhoneNumber { get; set; }
}
