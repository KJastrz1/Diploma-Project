public class Availability
{
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public TimeSpan VisitDuration { get; set; }

    public Availability(DayOfWeek day, TimeSpan startTime, TimeSpan endTime, TimeSpan visitDuration)
    {
        Day = day;
        StartTime = startTime;
        EndTime = endTime;
        VisitDuration = visitDuration;
    }
}