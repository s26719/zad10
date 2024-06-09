using System.Globalization;
using doKolosa2.Context;
using doKolosa2.DTOs;
using doKolosa2.Exceptions;
using doKolosa2.Models;
using Microsoft.EntityFrameworkCore;

namespace doKolosa2.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly MyDbContext _myDbContext;

    public PrescriptionRepository(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task AddPrescriptionAsync(PrescriptionToAddDto prescriptionToAddDto)
    {
        using var transaction = await _myDbContext.Database.BeginTransactionAsync();

        try
        {
            var maxIdPrescription = _myDbContext.Prescriptions.Max(p => p.IdPrescription) + 1;
        
            var patient = prescriptionToAddDto.PatientDto;
            var query = await _myDbContext.Patients.FirstOrDefaultAsync(p => p.IdPatient == patient.IdPatient);
            if (query == null)
            {
                var Patient = new Patient
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    BirthDate = patient.BirthDate
                };
                await _myDbContext.Patients.AddAsync(Patient);
                await _myDbContext.SaveChangesAsync();
            }

            foreach (var medicament in prescriptionToAddDto.MedicamentDto)
            {
                var mediExist =
                    await _myDbContext.Medicaments.FirstOrDefaultAsync(m => m.IdMedicament == medicament.IdMedicament);
                if (query == null)
                {
                    throw new NotFoundException($"lek o id: {medicament.IdMedicament} nie istnieje");
                }
            }
            
            var prescription = new Prescription
            {
                Date = prescriptionToAddDto.Date,
                DueDate = prescriptionToAddDto.DueDate,
                IdPatient = prescriptionToAddDto.PatientDto.IdPatient,
                IdDoctor = 1
            };
            await _myDbContext.Prescriptions.AddAsync(prescription);
            await _myDbContext.SaveChangesAsync();

            foreach (var medicament in prescriptionToAddDto.MedicamentDto)
            {   
                var prescriptionMedicament = new Prescription_Medicament
                {
                    IdMedicament = medicament.IdMedicament,
                    IdPrescription = maxIdPrescription,
                    Dose = medicament.Dose,
                    Details = medicament.Description
                };
                await _myDbContext.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
                await _myDbContext.SaveChangesAsync();
            }
            
        
        

            

            await transaction.CommitAsync();
        }
        catch (NotFoundException e)
        {
            await transaction.RollbackAsync();
            throw new NotFoundException(e.Message);
            
        }
        
    }
}