using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Backend.Data;

public class ClinicDataContext : DbContext
{
    public ClinicDataContext(DbContextOptions<ClinicDataContext> options)
        : base(options)
    {
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<PatientDocument> PatientDocuments { get; set; }
    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<Vacation> Vacations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new Admin());
        modelBuilder.ApplyConfiguration(new Doctor());
        modelBuilder.ApplyConfiguration(new Patient());
        modelBuilder.ApplyConfiguration(new Clinic());
        modelBuilder.ApplyConfiguration(new Appointment());
        modelBuilder.ApplyConfiguration(new Availability());
        modelBuilder.ApplyConfiguration(new PatientDocument());
        modelBuilder.ApplyConfiguration(new Vacation());
    }
}
