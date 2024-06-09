using doKolosa2.Context;
using doKolosa2.DTOs;
using doKolosa2.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace doKolosa2.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly MyDbContext _mydbContext;

    public PatientRepository(MyDbContext mydbContext)
    {
        _mydbContext = mydbContext;
    }

    public async Task<PatientOutDto> GetPatientDetailsAsync(int id)
    {
        var patient = await _mydbContext.Patients.FirstOrDefaultAsync(p => p.IdPatient == id);
        if (patient == null)
        {
            throw new NotFoundException("nie ma pacjenta o podanym id");
        }
        var patientDto = _mydbContext.Patients
            .Where(p => p.IdPatient == id)
            .Select(p => new PatientOutDto
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                PrescriptionOutDtos = p.Prescriptions.Select(pr => new PrescriptionOutDto
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    MedicamentOutDtos = pr.PrescriptionMedicaments.Select(pm => new MedicamentOutDto
                    {
                        IdMedicament = pm.IdMedicament,
                        Name = pm.IdMedicamentNavigation.Name,
                        Dose = pm.Dose,
                        Description = pm.Details
                    }).ToList(),
                    DoctorDto = new DoctorOutDto
                    {
                        IdDoctor = pr.IdDoctorNavigation.IdDoctor,
                        FirstName = pr.IdDoctorNavigation.FirstName
                    }
                }).ToList()
            })
            .FirstOrDefault();
        return patientDto!;
    }
}