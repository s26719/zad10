namespace doKolosa2.DTOs;

public class PrescriptionOutDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public IEnumerable<MedicamentOutDto> MedicamentOutDtos { get; set; }
    public DoctorOutDto DoctorDto { get; set; }
}

