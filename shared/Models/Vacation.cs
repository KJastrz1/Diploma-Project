namespace Shared.Models;
public class Vacation
{
    public Guid Id { get; private set; }
    public Doctor Doctor { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Vacation(Doctor doctor, DateTime startDate, DateTime endDate)
    {
        Id = Guid.NewGuid();
        Doctor = doctor;
        DoctorId = doctor.Id;
        StartDate = startDate;
        EndDate = endDate;
    }
}
