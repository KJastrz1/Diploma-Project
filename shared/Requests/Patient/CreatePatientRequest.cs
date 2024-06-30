using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Patient;

public class CreatePatientRequest
{
    [Required(ErrorMessage = "Name is required.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Surname is required.")]
    public required string Surname { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    public required string Address { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "PESEL is required.")]
    public required string PESEL { get; set; }

    [Required(ErrorMessage = "Date of birth is required.")]
    public required DateTime DateOfBirth { get; set; }
}

