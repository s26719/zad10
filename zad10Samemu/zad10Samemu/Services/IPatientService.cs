using zad10Samemu.DTO_s;

namespace zad10Samemu.Services;

public interface IPatientService
{
    Task<PatientInDto> GetPatientDetailsAsync(int id);
}