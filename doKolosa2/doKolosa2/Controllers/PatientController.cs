using doKolosa2.Exceptions;
using doKolosa2.Services;
using Microsoft.AspNetCore.Mvc;

namespace doKolosa2.Controllers;
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
            await _patientService.GetPatientDetails(id);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}