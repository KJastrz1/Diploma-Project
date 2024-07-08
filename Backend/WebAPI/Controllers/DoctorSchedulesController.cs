using Microsoft.AspNetCore.Mvc;
using Shared.Requests.DoctorSchedule;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/doctor-schedules")]
public class DoctorSchedulesController : ControllerBase
{
    private readonly IDoctorSchedulesService _doctorSchedulesService;

    public DoctorSchedulesController(IDoctorSchedulesService doctorSchedulesService)
    {
        _doctorSchedulesService = doctorSchedulesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDoctorSchedules([FromQuery] DoctorScheduleFilter filter, int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var result = await _doctorSchedulesService.GetDoctorSchedulesAsync(filter, pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the doctor schedules. Please try again later." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorSchedule(Guid id)
    {
        try
        {
            var doctorSchedule = await _doctorSchedulesService.GetDoctorScheduleByIdAsync(id);
            if (doctorSchedule == null)
            {
                return NotFound();
            }
            return Ok(doctorSchedule);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the doctor schedule. Please try again later." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctorSchedule([FromBody] CreateDoctorScheduleRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var newDoctorSchedule = await _doctorSchedulesService.CreateDoctorScheduleAsync(request);
            return CreatedAtAction(nameof(GetDoctorSchedule), new { id = newDoctorSchedule.Id }, newDoctorSchedule);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Specified doctor does not exist"))
            {
                return BadRequest(new { error = "Specified doctor does not exist." });
            }
            return StatusCode(500, new { error = "An error occurred while creating the doctor schedule. Please try again later." });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctorSchedule(Guid id, [FromBody] UpdateDoctorScheduleRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var updatedDoctorSchedule = await _doctorSchedulesService.UpdateDoctorScheduleAsync(id, request);
            if (updatedDoctorSchedule == null)
            {
                return NotFound();
            }
            return Ok(updatedDoctorSchedule);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Specified doctor does not exist"))
            {
                return BadRequest(new { error = "Specified doctor does not exist." });
            }
            return StatusCode(500, new { error = "An error occurred while updating the doctor schedule. Please try again later." });
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorSchedule(Guid id)
    {
        try
        {
            var result = await _doctorSchedulesService.DeleteDoctorScheduleAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while deleting the doctor schedule. Please try again later." });
        }
    }

    [HttpGet("available-slots")]
    public async Task<IActionResult> GetAvailableSlots([FromQuery] Guid doctorId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        try
        {
            var availableSlots = await _doctorSchedulesService.GetAvailableSlotsAsync(doctorId, startDate, endDate);
            return Ok(availableSlots);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Specified doctor does not exist"))
            {
                return BadRequest(new { error = "Specified doctor does not exist." });
            }
            return StatusCode(500, new { error = "An error occurred while retrieving the available slots. Please try again later." });
        }
    }
}
