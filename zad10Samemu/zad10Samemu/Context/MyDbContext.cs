using Microsoft.EntityFrameworkCore;
using zad10Samemu.EfConfiguration;
using zad10Samemu.Models;

namespace zad10Samemu.Context;

public class MyDbContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<AppUser> Users { get; set; }
    
    protected MyDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DoctorEfConfig());
        modelBuilder.ApplyConfiguration(new PatientEfConfig());
        modelBuilder.ApplyConfiguration(new PrescriptionEfConfig());
        modelBuilder.ApplyConfiguration(new Prescription_MedicamentEfConfig());
        modelBuilder.ApplyConfiguration(new MedicamentEfConfig());
        modelBuilder.ApplyConfiguration(new AppUserEfConfig());
    }

    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
}