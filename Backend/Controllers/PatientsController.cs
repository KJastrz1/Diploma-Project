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
        var result = await _patientsService.GetPatientsAsync(pageNumber, pageSize, filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(Guid id)
    {
        var patient = await _patientsService.GetPatientByIdAsync(id);
        if (patient == null)
        {
            return NotFound();
        }
        return Ok(patient);
    }
    [HttpPost]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newPatient = await _patientsService.CreatePatientAsync(request);
        return CreatedAtAction(nameof(GetPatient), new { id = newPatient.Id }, newPatient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] UpdatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedPatient = await _patientsService.UpdatePatientAsync(id, request);
        return Ok(updatedPatient);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        var result = await _patientsService.DeletePatientAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
