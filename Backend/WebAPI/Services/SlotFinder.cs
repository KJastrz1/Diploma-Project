using Shared.Entities;
using Shared.Responses.DoctorSchedule;

namespace Backend.Services;
public static class SlotFinder
{
    public static List<GetAvailableSlotResponse> FindAvailableSlots(
        List<DoctorSchedule> doctorSchedules,
        List<Appointment> appointments,
        List<Vacation> vacations,
        DateTime startDate,
        DateTime endDate)
    {
        var availableSlots = new List<GetAvailableSlotResponse>();

        foreach (var schedule in doctorSchedules)
        {
            var currentDate = startDate.Date;
            while (currentDate <= endDate.Date)
            {
                if (currentDate.DayOfWeek == schedule.Day)
                {
                    var startTime = currentDate.Add(schedule.StartTime);
                    var endTime = currentDate.Add(schedule.EndTime);

                    while (startTime.Add(schedule.VisitDuration) <= endTime)
                    {
                        var slotEndTime = startTime.Add(schedule.VisitDuration);
                        if (!appointments.Any(a => a.AppointmentDate < slotEndTime && a.EndDate > startTime) &&
                            !vacations.Any(v => v.StartDate < slotEndTime && v.EndDate > startTime))
                        {
                            availableSlots.Add(new GetAvailableSlotResponse
                            {
                                StartTime = startTime,
                                EndTime = slotEndTime
                            });
                        }
                        startTime = slotEndTime;
                    }
                }
                currentDate = currentDate.AddDays(1);
            }
        }

        return availableSlots;
    }
}
