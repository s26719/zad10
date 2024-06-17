using zad10Samemu.DTO_s;

namespace zad10Samemu.Repositories;

public interface IPrescriptionRepository
{
    Task AddPrescriptionAsync(PrescriptionOutDto prescriptionOutDto);

    Task RegisterUser(RegisterRequest model); 
    Task<TokenResponse> LoginUser(LoginRequest loginRequest); 
    Task<TokenResponse> RefreshUser(RefreshTokenRequest refreshToken); 
}