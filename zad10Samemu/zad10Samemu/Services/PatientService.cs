using zad10Samemu.Context;
using zad10Samemu.DTO_s;
using zad10Samemu.Repositories;

namespace zad10Samemu.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientInDto> GetPatientDetailsAsync(int id)
    {
        var patients =await _patientRepository.GetPatietnsWithDetailsAsync(id);
        return patients;
    }
}