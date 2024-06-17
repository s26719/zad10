namespace zad10Samemu.DTO_s;

public class PatientInDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionInDto> Prescriptions { get; set; }
}