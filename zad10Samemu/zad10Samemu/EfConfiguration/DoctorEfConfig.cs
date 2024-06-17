using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using zad10Samemu.Models;

namespace zad10Samemu.EfConfiguration;

public class DoctorEfConfig : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");
        builder.HasKey(d=>d.IdDoctor);
        builder.Property(p => p.IdDoctor).ValueGeneratedOnAdd();

        builder.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Email).IsRequired();

        builder.HasMany(d => d.Prescriptions)
            .WithOne(pr => pr.IdDoctorNavigation)
            .HasForeignKey(pr => pr.IdDoctor)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasData(new List<Doctor>()
        {
            new Doctor()
            {
                IdDoctor = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jankow@wp.pl"
            },
            new Doctor()
            {
                IdDoctor = 2,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anowak@gmail.com"
            }
        });
    }
}