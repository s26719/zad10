namespace zad10Samemu.Models;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; }
    public virtual Patient IdPatienNavigation { get; set; }
    public virtual ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    
}