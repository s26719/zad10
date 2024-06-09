using doKolosa2.DTOs;
using doKolosa2.Repositories;

namespace doKolosa2.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async  Task<PatientOutDto> GetPatientDetails(int id)
    {
        return await _patientRepository.GetPatientDetailsAsync(id);
    }
}