namespace zad10Samemu.DTO_s;

public class PrescriptionInDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentsInDto> Medicaments { get; set; }
    public DoctorInDto Doctor { get; set; }
}