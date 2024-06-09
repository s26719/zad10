namespace doKolosa2.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    // mozna pozniej nie od razu czyl po stworzeniu prescriptionmedicament.. to mowi o tym ze medicament jest kluczem obcym w prescription<_Medicament
    public virtual ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; }
}