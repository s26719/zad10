using doKolosa2.EfConfiguration;
using doKolosa2.Models;
using Microsoft.EntityFrameworkCore;

namespace doKolosa2.Context;

public class MyDbContext : DbContext
{

    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    
    
    protected MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MedicamentEfConfig());
        modelBuilder.ApplyConfiguration(new PrescriptionEfConfig());
        modelBuilder.ApplyConfiguration(new PatientEfConfig());
        modelBuilder.ApplyConfiguration(new Prescription_MedicamentEfConfig());
        modelBuilder.ApplyConfiguration(new DoctorEfConfig());
    }
}