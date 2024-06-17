using zad10Samemu.DTO_s;

namespace zad10Samemu.Repositories;

public interface IPatientRepository
{
    Task<PatientInDto> GetPatietnsWithDetailsAsync(int id);
}