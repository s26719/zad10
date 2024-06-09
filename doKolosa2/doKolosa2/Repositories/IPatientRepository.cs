using doKolosa2.DTOs;

namespace doKolosa2.Repositories;

public interface IPatientRepository
{
    Task<PatientOutDto> GetPatientDetailsAsync(int id);
}