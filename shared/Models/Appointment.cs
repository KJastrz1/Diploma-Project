namespace Shared.Models
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public Guid ClinicId { get; set; }
        public Clinic Clinic { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Notes { get; set; }

  
        public Appointment()
        {
            Id = Guid.NewGuid();
        }

        
        public Appointment(Guid clinicId, Guid doctorId, Guid patientId, DateTime appointmentDate, string? notes)
        {
            Id = Guid.NewGuid();
            ClinicId = clinicId;
            DoctorId = doctorId;
            PatientId = patientId;
            AppointmentDate = appointmentDate;
            Notes = notes;
        }
    }
}
