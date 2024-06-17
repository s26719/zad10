using zad10Samemu.DTO_s;
using zad10Samemu.Exceptions;
using zad10Samemu.Repositories;

namespace zad10Samemu.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task AddPrescriptionAsync(PrescriptionOutDto prescriptionOutDto)
    {
        //sprawdzamy warunek na max 10 lekow
        var medicamentcount = prescriptionOutDto.Medicaments.Count;
        if (medicamentcount > 10)
        {
            throw new BadRequestException("za duzo lekow");
        }
        //sprwadzamy czy DueDate > Date
        if (prescriptionOutDto.DueDate <= prescriptionOutDto.Date)
        {
            throw new BadRequestException("zla data");
        }
        // wszystko sprawdzone mozna dodawac
        await _prescriptionRepository.AddPrescriptionAsync(prescriptionOutDto);
    }

    public async Task RegisterUser(RegisterRequest model)
    {
        await _prescriptionRepository.RegisterUser(model);
    }

    public async Task<TokenResponse> LoginUser(LoginRequest loginRequest)
    {
        return await _prescriptionRepository.LoginUser(loginRequest);
    }

    public async Task<TokenResponse> RefreshUser(RefreshTokenRequest refreshToken)
    {
        return await _prescriptionRepository.RefreshUser(refreshToken);
    }
}