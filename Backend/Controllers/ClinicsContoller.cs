using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Clinic;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/clinics")]
public class ClinicsController : ControllerBase
{
    private readonly IClinicsService _clinicsService;

    public ClinicsController(IClinicsService clinicsService)
    {
        _clinicsService = clinicsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetClinics([FromQuery] ClinicFilter filter, int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var result = await _clinicsService.GetClinicsAsync(filter, pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the clinics. Please try again later." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClinic(Guid id)
    {
        try
        {
            var clinic = await _clinicsService.GetClinicByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return Ok(clinic);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the clinic. Please try again later." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateClinic([FromBody] CreateClinicRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var newClinic = await _clinicsService.CreateClinicAsync(request);
            return CreatedAtAction(nameof(GetClinic), new { id = newClinic.Id }, newClinic);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while creating the clinic. Please try again later." });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClinic(Guid id, [FromBody] UpdateClinicRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var updatedClinic = await _clinicsService.UpdateClinicAsync(id, request);
            if (updatedClinic == null)
            {
                return NotFound();
            }
            return Ok(updatedClinic);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while updating the clinic. Please try again later." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClinic(Guid id)
    {
        try
        {
            var result = await _clinicsService.DeleteClinicAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while deleting the clinic. Please try again later." });
        }
    }
}
