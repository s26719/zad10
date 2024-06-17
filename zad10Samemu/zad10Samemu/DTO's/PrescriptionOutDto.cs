using System.ComponentModel.DataAnnotations;

namespace zad10Samemu.DTO_s;

public class PrescriptionOutDto
{
    public PatientOutDto PatientOutDto { get; set; }
    public List<MedicamentOutDto> Medicaments { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
}