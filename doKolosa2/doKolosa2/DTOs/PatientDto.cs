using System.ComponentModel.DataAnnotations;

namespace doKolosa2.DTOs;

public class PatientDto
{
    [Required]
    public int  IdPatient { get; set; }
    [MaxLength(100)][Required]
    public string FirstName { get; set; }
    [MaxLength(100)][Required]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    
}