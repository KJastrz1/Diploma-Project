using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Patient;

public class CreatePatientRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Surname is required.")]
    [StringLength(100, ErrorMessage = "Surname cannot be longer than 100 characters.")]
    public required string Surname { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "PESEL is required.")]
    [StringLength(11, ErrorMessage = "PESEL must be 11 characters long.", MinimumLength = 11)]
    public required string PESEL { get; set; }

    [Required(ErrorMessage = "Date of birth is required.")]
    public required DateTime DateOfBirth { get; set; }
}
