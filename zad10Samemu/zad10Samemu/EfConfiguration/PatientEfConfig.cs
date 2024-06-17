using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using zad10Samemu.Models;

namespace zad10Samemu.EfConfiguration;

public class PatientEfConfig : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient");
        builder.HasKey(p => p.IdPatient);
        builder.Property(p => p.IdPatient).ValueGeneratedOnAdd();

        builder.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Birthdate).IsRequired();

        builder.HasMany(p => p.Prescriptions)
            .WithOne(pr => pr.IdPatienNavigation)
            .HasForeignKey(pr => pr.IdPatient)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasData(new List<Patient>()
        {
            new Patient()
            {
                IdPatient = 1,
                FirstName = "Marek",
                LastName = "Rybak",
                Birthdate = new DateTime(2000, 1, 1)
            },
            new Patient()
            {
                IdPatient = 2,
                FirstName = "Kasia",
                LastName = "Bąk",
                Birthdate = new DateTime(2002, 2, 2)
            }
        });
    }
}