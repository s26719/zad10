using doKolosa2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace doKolosa2.EfConfiguration;
// najwazniejsze metody:
// toTable => 
public class MedicamentEfConfig : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.ToTable("Medicament"); // ustalam nazwe tabelki

        builder.HasKey(m => m.IdMedicament); // klucz glowny
        builder.Property(m => m.IdMedicament).ValueGeneratedOnAdd(); // auto numeracja (wazne)

        builder.Property(m => m.Name).HasMaxLength(100).IsRequired(); // isRequired(false) mowi ze moze byc nullable
        builder.Property(m => m.Description).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Type).HasMaxLength(100).IsRequired();

        builder.HasMany(m => m.PrescriptionMedicaments)
            .WithOne(pm => pm.IdMedicamentNavigation)
            .HasForeignKey(pm => pm.IdMedicament)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasData(new List<Medicament>()
        {
            new Medicament()
            {
                IdMedicament = 1,
                Name = "Apap",
                Description = "Przeciwbólowy",
                Type = "Tabletki"
            },
            new Medicament()
            {
                IdMedicament = 2,
                Name = "Ibuprom",
                Description = "Przeciwzapalny",
                Type = "Tabletki"
            },
            new Medicament()
            {
                IdMedicament = 3,
                Name = "Rutinoscorbin",
                Description = "Witamina C",
                Type = "Tabletki"
            }
        });
    }
}