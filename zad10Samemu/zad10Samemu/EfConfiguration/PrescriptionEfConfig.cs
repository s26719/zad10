using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using zad10Samemu.Models;

namespace zad10Samemu.EfConfiguration;

public class PrescriptionEfConfig : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescription");
        builder.HasKey(p => p.IdPrescription);
        builder.Property(p => p.IdPrescription).ValueGeneratedOnAdd();

        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.DueDate).IsRequired();

        builder.HasOne(p => p.IdDoctorNavigation)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.IdDoctor);
        
        builder.HasOne(p => p.IdPatienNavigation)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.IdPatient);
        
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