using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Appointment;
public class CreateAppointmentRequest
{
    [Required(ErrorMessage = "Clinic ID is required.")]
    public required Guid ClinicId { get; set; }

    [Required(ErrorMessage = "Doctor ID is required.")]
    public required Guid DoctorId { get; set; }

    [Required(ErrorMessage = "Patient ID is required.")]
    public required Guid PatientId { get; set; }

    [Required(ErrorMessage = "Appointment date is required.")]
    public required DateTime AppointmentDate { get; set; }

    [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
    public string? Notes { get; set; }
}
