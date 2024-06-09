using System.ComponentModel.DataAnnotations;

namespace doKolosa2.DTOs;

public class PatientOutDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public IEnumerable<PrescriptionOutDto> PrescriptionOutDtos { get; set; }
    
}