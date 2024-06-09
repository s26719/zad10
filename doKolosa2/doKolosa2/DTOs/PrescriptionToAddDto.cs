using System.ComponentModel.DataAnnotations;
using doKolosa2.Models;

namespace doKolosa2.DTOs;

public class PrescriptionToAddDto
{
    [Required]
    public PatientDto PatientDto { get; set; }
    [Required]
    public IEnumerable<MedicamentDto> MedicamentDto { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
}