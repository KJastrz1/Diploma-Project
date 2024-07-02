using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Doctor;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorsService _doctorsService;

    public DoctorsController(IDoctorsService doctorsService)
    {
        _doctorsService = doctorsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDoctors([FromQuery] DoctorFilter filter, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var result = await _doctorsService.GetDoctorsAsync(filter, pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the doctors. Please try again later." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctor(Guid id)
    {
        try
        {
            var doctor = await _doctorsService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the doctor. Please try again later." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var newDoctor = await _doctorsService.CreateDoctorAsync(request);
            return CreatedAtAction(nameof(GetDoctor), new { id = newDoctor.Id }, newDoctor);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Specified clinic does not exist"))
            {
                return BadRequest(new { error = "The specified clinic does not exist." });
            }
            return StatusCode(500, new { error = "An error occurred while creating the doctor. Please try again later." });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] UpdateDoctorRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var updatedDoctor = await _doctorsService.UpdateDoctorAsync(id, request);
            if (updatedDoctor == null)
            {
                return NotFound();
            }
            return Ok(updatedDoctor);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Specified clinic does not exist"))
            {
                return BadRequest(new { error = "The specified clinic does not exist." });
            }
            return StatusCode(500, new { error = "An error occurred while updating the doctor. Please try again later." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        try
        {
            var result = await _doctorsService.DeleteDoctorAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while deleting the doctor. Please try again later." });
        }
    }
}
