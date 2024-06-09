using doKolosa2.DTOs;

namespace doKolosa2.Repositories;

public interface IPrescriptionRepository
{
    Task AddPrescriptionAsync(PrescriptionToAddDto prescriptionToAddDto);

}