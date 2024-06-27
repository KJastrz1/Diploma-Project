public class Appointment
{
    public Guid Id { get; private set; }
    public Clinic Clinic { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Notes { get; set; }

    public Appointment(Clinic clinic, Doctor doctor, Patient patient, DateTime appointmentDate, string notes)
    {
        Id = Guid.NewGuid();
        Clinic = clinic;
        Doctor = doctor;
        Patient = patient;
        AppointmentDate = appointmentDate;
        Notes = notes;
    }
}
