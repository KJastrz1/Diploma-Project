using AutoMapper;
using Backend.Data;
using Backend.Utils;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Requests.Appointment;
using Shared.Responses.Appointment;


namespace Backend.Services;
public interface IAppointmentsService
{
    Task<PagedResult<GetAppointmentResponse>> GetAppointmentsAsync(AppointmentFilter filter, int pageNumber, int pageSize);
    Task<GetAppointmentResponse?> GetAppointmentByIdAsync(Guid id);
    Task<GetAppointmentResponse> CreateAppointmentAsync(CreateAppointmentRequest request);
    Task<GetAppointmentResponse> UpdateAppointmentAsync(Guid id, UpdateAppointmentRequest request);
    Task<bool> DeleteAppointmentAsync(Guid id);
}
public class AppointmentsService : IAppointmentsService
{
    private readonly ClinicDataContext _context;
    private readonly IMapper _mapper;

    public AppointmentsService(ClinicDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetAppointmentResponse>> GetAppointmentsAsync(AppointmentFilter filter, int pageNumber, int pageSize)
    {
        var query = _context.Appointments.AsQueryable()
            .ApplyFilter(filter);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var mappedItems = _mapper.Map<List<GetAppointmentResponse>>(items);

        return new PagedResult<GetAppointmentResponse>
        {
            Items = mappedItems,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }

    public async Task<GetAppointmentResponse?> GetAppointmentByIdAsync(Guid id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Clinic)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null)
        {
            return null;
        }

        GetAppointmentResponse response = _mapper.Map<GetAppointmentResponse>(appointment);
        return response;
    }

    public async Task<GetAppointmentResponse> CreateAppointmentAsync(CreateAppointmentRequest request)
    {
        var clinicExists = await _context.Clinics.AnyAsync(c => c.Id == request.ClinicId);
        if (!clinicExists)
        {
            throw new Exception("Specified clinic does not exist.");
        }

        var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == request.DoctorId);
        if (!doctorExists)
        {
            throw new Exception("Specified doctor does not exist.");
        }

        var patientExists = await _context.Patients.AnyAsync(p => p.Id == request.PatientId);
        if (!patientExists)
        {
            throw new Exception("Specified patient does not exist.");
        }

        Appointment appointment = _mapper.Map<Appointment>(request);

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        GetAppointmentResponse response = _mapper.Map<GetAppointmentResponse>(appointment);
        return response;
    }


    public async Task<GetAppointmentResponse> UpdateAppointmentAsync(Guid id, UpdateAppointmentRequest request)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return null;
        }

        _mapper.Map(request, appointment);

        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();

        GetAppointmentResponse response = _mapper.Map<GetAppointmentResponse>(appointment);

        return response;
    }

    public async Task<bool> DeleteAppointmentAsync(Guid id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return false;
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return true;
    }
}
