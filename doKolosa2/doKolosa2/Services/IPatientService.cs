using doKolosa2.DTOs;

namespace doKolosa2.Services;

public interface IPatientService
{
    Task<PatientOutDto> GetPatientDetails(int id);
}