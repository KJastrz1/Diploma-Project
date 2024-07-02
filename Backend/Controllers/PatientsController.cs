using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Patient;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IPatientsService _patientsService;

    public PatientsController(IPatientsService patientsService)
    {
        _patientsService = patientsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] PatientFilter filter, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var result = await _patientsService.GetPatientsAsync(filter, pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the patients. Please try again later." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(Guid id)
    {
        try
        {
            var patient = await _patientsService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the patient. Please try again later." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var newPatient = await _patientsService.CreatePatientAsync(request);
            return CreatedAtAction(nameof(GetPatient), new { id = newPatient.Id }, newPatient);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while creating the patient. Please try again later." });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] UpdatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedPatient = await _patientsService.UpdatePatientAsync(id, request);
            if (updatedPatient == null)
            {
                return NotFound();
            }
            return Ok(updatedPatient);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while updating the patient. Please try again later." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        try
        {
            var result = await _patientsService.DeletePatientAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while deleting the patient. Please try again later." });
        }
    }
}
