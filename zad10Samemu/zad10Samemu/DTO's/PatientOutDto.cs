using System.ComponentModel.DataAnnotations;

namespace zad10Samemu.DTO_s;

public class PatientOutDto
{
    [Required]
    public int IdPatient { get; set; }
    [MaxLength(100)][Required]
    public string FirstName { get; set; }
    [MaxLength(100)][Required]
    public string LastName { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
}