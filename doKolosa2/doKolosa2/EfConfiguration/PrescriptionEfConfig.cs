using doKolosa2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doKolosa2.EfConfiguration;

public class PrescriptionEfConfig : IEntityTypeConfiguration<Models.Prescription>
{
    public void Configure(EntityTypeBuilder<Models.Prescription> builder)
    {
        builder.ToTable("Prescription");
        builder.HasKey(p => p.IdPrescription);
        builder.Property(p => p.IdPrescription).ValueGeneratedOnAdd();

        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.DueDate).IsRequired();

        builder.HasOne(p => p.IdDoctorNavigation)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.IdDoctor);

        builder.HasOne(p => p.IdPatientNavigation)
            .WithMany(pat => pat.Prescriptions)
            .HasForeignKey(p => p.IdPatient);

        builder.HasMany(p => p.PrescriptionMedicaments)
            .WithOne(pm => pm.IdPrescriptionNavigation)
            .HasForeignKey(pm => pm.IdPrescription)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasData(new List<Prescription>()
        {
            new Prescription()
            {
                IdPrescription = 1,
                Date = new DateTime(2024, 5, 28),
                DueDate = new DateTime(2024, 6, 28),
                IdPatient = 1,
                IdDoctor = 1
            }
        });
    }
}