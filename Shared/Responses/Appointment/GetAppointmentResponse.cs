using Shared.Responses.Clinic;
using Shared.Responses.Doctor;
using Shared.Responses.Patient;

namespace Shared.Responses.Appointment;
public class GetAppointmentResponse
{
    public Guid Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Notes { get; set; }
    public GetClinicResponse Clinic { get; set; }
    public GetDoctorResponse Doctor { get; set; }
    public GetPatientResponse Patient { get; set; }
}
