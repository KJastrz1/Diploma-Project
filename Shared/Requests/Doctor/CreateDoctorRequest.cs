using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Doctor;

public class CreateDoctorRequest
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

    [Required(ErrorMessage = "Medical license number is required.")]
    [StringLength(50, ErrorMessage = "Medical license number cannot be longer than 50 characters.")]
    public required string MedicalLicenseNumber { get; set; }

    [Required(ErrorMessage = "Specialty is required.")]
    [StringLength(100, ErrorMessage = "Specialty cannot be longer than 100 characters.")]
    public required string Specialty { get; set; }

    [Required(ErrorMessage = "Office number is required.")]
    [StringLength(10, ErrorMessage = "Office number cannot be longer than 10 characters.")]
    public required string OfficeNumber { get; set; }

    [Required(ErrorMessage = "Clinic ID is required.")]
    public required Guid ClinicId { get; set; }
}
