using doKolosa2.DTOs;
using doKolosa2.Exceptions;
using doKolosa2.Repositories;

namespace doKolosa2.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task AddPrescriptionAsync(PrescriptionToAddDto prescriptionToAddDto)
    {
        var mediCount = prescriptionToAddDto.MedicamentDto.Count();
        if (mediCount > 10)
        {
            throw new BadRequestException("za duzo lekow");
        }

        if (prescriptionToAddDto.Date > prescriptionToAddDto.DueDate)
        {
            throw new BadRequestException("podano zle daty");
        }

        await _prescriptionRepository.AddPrescriptionAsync(prescriptionToAddDto);
    }
}