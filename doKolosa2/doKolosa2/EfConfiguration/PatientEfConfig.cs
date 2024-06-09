using doKolosa2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doKolosa2.EfConfiguration;

public class PatientEfConfig :IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient");
        builder.HasKey(p => p.IdPatient);
        builder.Property(p => p.IdPatient).ValueGeneratedOnAdd();

        builder.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.BirthDate).IsRequired();

        builder.HasMany(p => p.Prescriptions)
            .WithOne(pm => pm.IdPatientNavigation)
            .HasForeignKey(pm => pm.IdPatient)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasData(new List<Patient>()
        {
            new Patient()
            {
                IdPatient = 1,
                FirstName = "Marek",
                LastName = "Rybak",
                BirthDate = new DateTime(2000, 1, 1)
            },
            new Patient()
            {
                IdPatient = 2,
                FirstName = "Kasia",
                LastName = "Bąk",
                BirthDate = new DateTime(2002, 2, 2)
            }
        });
    }
}