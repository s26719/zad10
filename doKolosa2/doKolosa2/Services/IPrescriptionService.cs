using doKolosa2.DTOs;

namespace doKolosa2.Services;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(PrescriptionToAddDto prescriptionToAddDto);
}