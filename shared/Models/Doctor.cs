namespace Shared.Models;

public class Doctor : UserBase
{
    public string MedicalLicenseNumber { get; set; }
    public string Specialty { get; set; }
    public string OfficeNumber { get; set; }
    public Clinic Clinic { get; set; }
    public Guid ClinicId { get; set; }
    public List<Availability> Availabilities { get; set; }
    public List<Vacation> Vacations { get; set; }
    public List<Appointment> Appointments { get; set; }



    public void AddAvailability(DayOfWeek day, TimeSpan startTime, TimeSpan endTime, TimeSpan visitDuration)
    {
        Availabilities.Add(new Availability(this, day, startTime, endTime, visitDuration));
    }

    public void AddVacation(DateTime startDate, DateTime endDate)
    {
        Vacations.Add(new Vacation(this, startDate, endDate));
    }

    public void AddAppointment(Appointment appointment)
    {
        Appointments.Add(appointment);
    }

    public List<Availability> GetAvailabilityForWeek(DateTime startDate, DateTime endDate)
    {
        var result = new List<Availability>();

        foreach (var availability in Availabilities)
        {
            var availabilityDate = GetNextDateForDay(startDate, availability.Day);
            while (availabilityDate <= endDate)
            {
                if (!IsWithinVacation(availabilityDate, availability) && !IsAppointmentScheduled(availabilityDate, availability))
                {
                    result.Add(new Availability(
                        this,
                        availability.Day,
                        availability.StartTime,
                        availability.EndTime,
                        availability.VisitDuration
                    ));
                }
                availabilityDate = availabilityDate.AddDays(7);
            }
        }

        return result;
    }

    private DateTime GetNextDateForDay(DateTime startDate, DayOfWeek day)
    {
        int daysToAdd = ((int)day - (int)startDate.DayOfWeek + 7) % 7;
        return startDate.AddDays(daysToAdd);
    }

    private bool IsWithinVacation(DateTime date, Availability availability)
    {
        var availabilityStart = date.Date.Add(availability.StartTime);
        var availabilityEnd = date.Date.Add(availability.EndTime);

        foreach (var vacation in Vacations)
        {
            if (availabilityStart < vacation.EndDate && availabilityEnd > vacation.StartDate)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsAppointmentScheduled(DateTime date, Availability availability)
    {
        var availabilityStart = date.Date.Add(availability.StartTime);
        var availabilityEnd = date.Date.Add(availability.EndTime);

        foreach (var appointment in Appointments)
        {
            if (appointment.AppointmentDate >= availabilityStart && appointment.AppointmentDate < availabilityEnd)
            {
                return true;
            }
        }

        return false;
    }
}
