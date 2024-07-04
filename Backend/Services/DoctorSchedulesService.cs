using AutoMapper;
using Backend.Data;
using Backend.Utils;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Requests.DoctorSchedule;
using Shared.Responses.DoctorSchedule;

namespace Backend.Services;
public interface IDoctorSchedulesService
{
    Task<PagedResult<GetDoctorScheduleResponse>> GetDoctorSchedulesAsync(DoctorScheduleFilter filter, int pageNumber, int pageSize);
    Task<GetDoctorScheduleResponse?> GetDoctorScheduleByIdAsync(Guid id);
    Task<GetDoctorScheduleResponse> CreateDoctorScheduleAsync(CreateDoctorScheduleRequest request);
    Task<GetDoctorScheduleResponse> UpdateDoctorScheduleAsync(Guid id, UpdateDoctorScheduleRequest request);
    Task<bool> DeleteDoctorScheduleAsync(Guid id);
    Task<IEnumerable<GetAvailableSlotResponse>> GetAvailableSlotsAsync(Guid doctorId, DateTime startDate, DateTime endDate);
}

public class DoctorSchedulesService : IDoctorSchedulesService
{
    private readonly ClinicDataContext _context;
    private readonly IMapper _mapper;

    public DoctorSchedulesService(ClinicDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetDoctorScheduleResponse>> GetDoctorSchedulesAsync(DoctorScheduleFilter filter, int pageNumber, int pageSize)
    {
        var query = _context.DoctorSchedules.AsQueryable()
            .ApplyFilter(filter);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var mappedItems = _mapper.Map<List<GetDoctorScheduleResponse>>(items);

        return new PagedResult<GetDoctorScheduleResponse>
        {
            Items = mappedItems,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }

    public async Task<GetDoctorScheduleResponse?> GetDoctorScheduleByIdAsync(Guid id)
    {
        var doctorSchedule = await _context.DoctorSchedules.FindAsync(id);
        if (doctorSchedule == null)
        {
            return null;
        }
        GetDoctorScheduleResponse response = _mapper.Map<GetDoctorScheduleResponse>(doctorSchedule);
        return response;
    }

    public async Task<GetDoctorScheduleResponse> CreateDoctorScheduleAsync(CreateDoctorScheduleRequest request)
    {
        var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == request.DoctorId);
        if (!doctorExists)
        {
            throw new Exception("Specified doctor does not exist.");
        }

        DoctorSchedule doctorSchedule = _mapper.Map<DoctorSchedule>(request);

        _context.DoctorSchedules.Add(doctorSchedule);
        await _context.SaveChangesAsync();
        GetDoctorScheduleResponse response = _mapper.Map<GetDoctorScheduleResponse>(doctorSchedule);
        return response;
    }

    public async Task<GetDoctorScheduleResponse> UpdateDoctorScheduleAsync(Guid id, UpdateDoctorScheduleRequest request)
    {
        var doctorSchedule = await _context.DoctorSchedules.FindAsync(id);
        if (doctorSchedule == null)
        {
            return null;
        }

        _mapper.Map(request, doctorSchedule);

        _context.DoctorSchedules.Update(doctorSchedule);
        await _context.SaveChangesAsync();

        GetDoctorScheduleResponse response = _mapper.Map<GetDoctorScheduleResponse>(doctorSchedule);

        return response;
    }

    public async Task<bool> DeleteDoctorScheduleAsync(Guid id)
    {
        var doctorSchedule = await _context.DoctorSchedules.FindAsync(id);
        if (doctorSchedule == null)
        {
            return false;
        }

        _context.DoctorSchedules.Remove(doctorSchedule);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<GetAvailableSlotResponse>> GetAvailableSlotsAsync(Guid doctorId, DateTime startDate, DateTime endDate)
    {
        var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == doctorId);
        if (!doctorExists)
        {
            throw new Exception("Specified doctor does not exist");
        }
        var doctorSchedules = await _context.DoctorSchedules
            .Where(ds => ds.DoctorId == doctorId)
            .ToListAsync();

        var appointments = await _context.Appointments
            .Where(a => a.DoctorId == doctorId && a.AppointmentDate >= startDate && a.EndDate <= endDate)
            .ToListAsync();

        var vacations = await _context.Vacations
            .Where(v => v.DoctorId == doctorId && v.StartDate <= endDate && v.EndDate >= startDate)
            .ToListAsync();

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


