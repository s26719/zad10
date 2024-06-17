using System.ComponentModel.DataAnnotations;

namespace zad10Samemu.DTO_s;

public class MedicamentOutDto
{
    [Required]
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    [MaxLength(100)][Required]
    public string Description { get; set; }
}