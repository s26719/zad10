using System.ComponentModel.DataAnnotations;

namespace doKolosa2.DTOs;

public class MedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }
    [Required]
    public int Dose { get; set; }
    [Required][MaxLength(100)]
    public string Description { get; set; }
}