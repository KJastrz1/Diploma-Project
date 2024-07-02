using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Doctor
{
    public class UpdateDoctorRequest
    {
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [StringLength(100, ErrorMessage = "Surname cannot be longer than 100 characters.")]
        public string? Surname { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string? Email { get; set; }

        [StringLength(50, ErrorMessage = "Medical license number cannot be longer than 50 characters.")]
        public string? MedicalLicenseNumber { get; set; }

        [StringLength(100, ErrorMessage = "Specialty cannot be longer than 100 characters.")]
        public string? Specialty { get; set; }

        [StringLength(10, ErrorMessage = "Office number cannot be longer than 10 characters.")]
        public string? OfficeNumber { get; set; }

        public Guid? ClinicId { get; set; }
    }
}
