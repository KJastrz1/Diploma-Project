using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Patient
{
    public class UpdatePatientRequest
    {
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [StringLength(100, ErrorMessage = "Surname cannot be longer than 100 characters.")]
        public string? Surname { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string? Email { get; set; }

        [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
        public string? PhoneNumber { get; set; }

        [StringLength(11, ErrorMessage = "PESEL must be 11 characters long.", MinimumLength = 11)]
        public string? PESEL { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
