using zad10Samemu.DTO_s;

namespace zad10Samemu.Services;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(PrescriptionOutDto prescriptionOutDto);
    Task RegisterUser(RegisterRequest model); 
    Task<TokenResponse> LoginUser(LoginRequest loginRequest); 
    Task<TokenResponse> RefreshUser(RefreshTokenRequest refreshToken); 
}