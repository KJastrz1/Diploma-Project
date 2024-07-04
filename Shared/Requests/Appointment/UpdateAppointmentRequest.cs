using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Appointment;
public class UpdateAppointmentRequest
{
    public DateTime? AppointmentDate { get; set; }

    [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
    public string? Notes { get; set; }
}
