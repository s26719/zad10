using Microsoft.EntityFrameworkCore;
using zad10Samemu.Context;
using zad10Samemu.DTO_s;
using zad10Samemu.Exceptions;

namespace zad10Samemu.Repositories;

public class PatientRepository : IPatientRepository
{

    private readonly MyDbContext _myDbContext;

    public PatientRepository(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<PatientInDto> GetPatietnsWithDetailsAsync(int id)
    {
        //sprawdzamy czy pacjent o podanym id istnieje
        var patientExist = await _myDbContext.Patients.FirstOrDefaultAsync(p => p.IdPatient == id);
        if (patientExist == null)
        {
            throw new NotFoundException("pacjent o podanym id nie istnieje");
        }

        var patientInDto = await _myDbContext.Patients
            .Where(p => p.IdPatient == id)
            .Select(p => new PatientInDto
            {
                IdPatient = id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birthdate = p.Birthdate,
                Prescriptions = p.Prescriptions.Select(pr => new PrescriptionInDto
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Medicaments = pr.PrescriptionMedicaments.Select(m => new MedicamentsInDto
                    {
                        IdMedicament = m.IdMedicament,
                        Name = m.IdMedicamentNavigation.Name,
                        Dose = m.Dose,
                        Description = m.IdMedicamentNavigation.Description
                    }).ToList(),
                    Doctor = new DoctorInDto
                    {
                        IdDoctor = pr.IdDoctorNavigation.IdDoctor,
                        FirstName = pr.IdDoctorNavigation.FirstName
                    }

                }).OrderBy(pr=> pr.DueDate).ToList()
            }).FirstOrDefaultAsync();
        return patientInDto!;
    }
}