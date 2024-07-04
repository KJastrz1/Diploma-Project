using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.DoctorSchedule;
public class UpdateDoctorScheduleRequest
{
    public DayOfWeek? Day { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public TimeSpan? VisitDuration { get; set; }
}

