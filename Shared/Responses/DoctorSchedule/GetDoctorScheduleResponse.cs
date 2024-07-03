namespace Shared.Responses.DoctorSchedule;
public class GetDoctorScheduleResponse
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public TimeSpan VisitDuration { get; set; }
}

