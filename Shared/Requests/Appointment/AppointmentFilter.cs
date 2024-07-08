namespace Shared.Requests.Appointment;
public class AppointmentFilter
{
    public Guid? ClinicId { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? PatientId { get; set; }
    public DateTime? StartRange { get; set; }
    public DateTime? EndRange { get; set; }
}
