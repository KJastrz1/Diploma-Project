using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Appointment;
using Backend.Services;

namespace Backend.Controllers;
[ApiController]
[Route("api/appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentsService _appointmentsService;

    public AppointmentsController(IAppointmentsService appointmentsService)
    {
        _appointmentsService = appointmentsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointments([FromQuery] AppointmentFilter filter, int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var result = await _appointmentsService.GetAppointmentsAsync(filter, pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the appointments. Please try again later." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        try
        {
            var appointment = await _appointmentsService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the appointment. Please try again later." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var newAppointment = await _appointmentsService.CreateAppointmentAsync(request);
            return CreatedAtAction(nameof(GetAppointment), new { id = newAppointment.Id }, newAppointment);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("does not exist"))
            {
                return BadRequest(new { error = ex.Message });
            }
            return StatusCode(500, new { error = "An error occurred while creating the appointment. Please try again later." });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] UpdateAppointmentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var updatedAppointment = await _appointmentsService.UpdateAppointmentAsync(id, request);
            if (updatedAppointment == null)
            {
                return NotFound();
            }
            return Ok(updatedAppointment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while updating the appointment. Please try again later." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(Guid id)
    {
        try
        {
            var result = await _appointmentsService.DeleteAppointmentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while deleting the appointment. Please try again later." });
        }
    }
}
