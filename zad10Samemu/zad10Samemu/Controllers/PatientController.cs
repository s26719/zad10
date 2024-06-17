using Microsoft.AspNetCore.Mvc;
using zad10Samemu.Exceptions;
using zad10Samemu.Services;

namespace zad10Samemu.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    [HttpGet]
    public async Task<IActionResult> GetPatientDetailsAsync(int id)
    {
        try
        {
           var patients = await _patientService.GetPatientDetailsAsync(id);
           return Ok(patients);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}