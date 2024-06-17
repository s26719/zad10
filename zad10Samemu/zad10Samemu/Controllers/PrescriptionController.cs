using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zad10Samemu.DTO_s;
using zad10Samemu.Exceptions;
using zad10Samemu.Services;

namespace zad10Samemu.Controllers;
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
    public async Task<IActionResult> AddPrescriptionAsync(PrescriptionOutDto prescriptionOutDto)
    {
        try
        {
            await _prescriptionService.AddPrescriptionAsync(prescriptionOutDto);
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
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        await _prescriptionService.RegisterUser(model);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _prescriptionService.LoginUser(loginRequest);

        if (result == null)
            return Unauthorized();

        return Ok(result);
    }
    
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
    {
        var result = await _prescriptionService.RefreshUser(refreshToken);

        return Ok(result);
    }
}