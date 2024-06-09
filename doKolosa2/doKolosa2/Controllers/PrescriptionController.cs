using System.ComponentModel.DataAnnotations;
using doKolosa2.DTOs;
using doKolosa2.Exceptions;
using doKolosa2.Services;
using Microsoft.AspNetCore.Mvc;

namespace doKolosa2.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescriptionAsync(PrescriptionToAddDto prescriptionToAddDto)
    {
        try
        {
            await _prescriptionService.AddPrescriptionAsync(prescriptionToAddDto);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
    }
}