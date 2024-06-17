using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using zad10Samemu.Models;

namespace zad10Samemu.EfConfiguration;

public class Prescription_MedicamentEfConfig : IEntityTypeConfiguration<Prescription_Medicament>
{
    public void Configure(EntityTypeBuilder<Prescription_Medicament> builder)
    {
        builder.ToTable("Prescription_Medicament");
        builder.HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        builder.Property(pm => pm.Dose).IsRequired(false);
        builder.Property(pm => pm.Details).HasMaxLength(100).IsRequired();

        builder.HasOne(pm => pm.IdMedicamentNavigation)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);

        builder.HasOne(pm => pm.IdPrescriptionNavigation)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription);
        
        builder.HasData(new List<Prescription_Medicament>()
        {
            new Prescription_Medicament()
            {
                IdPrescription = 1,
                IdMedicament = 1,
                Dose = 2,
                Details = "Po jedzeniu"
            }
        });
    }
}