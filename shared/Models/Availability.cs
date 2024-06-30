namespace Shared.Models;

public class Availability
{
    public Guid Id { get; private set; }
    public Doctor Doctor { get; set; }
    public Guid DoctorId { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public TimeSpan VisitDuration { get; set; }

    public Availability(Doctor doctor, DayOfWeek day, TimeSpan startTime, TimeSpan endTime, TimeSpan visitDuration)
    {
        Id = Guid.NewGuid();
        Doctor = doctor;
        DoctorId = doctor.Id;
        Day = day;
        StartTime = startTime;
        EndTime = endTime;
        VisitDuration = visitDuration;
    }
}
