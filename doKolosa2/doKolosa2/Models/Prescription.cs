﻿namespace doKolosa2.Models;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public virtual Patient IdPatientNavigation { get; set; }
    public virtual Doctor IdDoctorNavigation { get; set; }
    public virtual ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; }
}