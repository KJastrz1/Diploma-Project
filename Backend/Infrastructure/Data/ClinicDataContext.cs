using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.DataSeeder;

namespace Infrastructure.Data;
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
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    public DbSet<Vacation> Vacations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new Admin());
        modelBuilder.ApplyConfiguration(new Doctor());
        modelBuilder.ApplyConfiguration(new Patient());
        modelBuilder.ApplyConfiguration(new Clinic());
        modelBuilder.ApplyConfiguration(new Appointment());
        modelBuilder.ApplyConfiguration(new DoctorSchedule());
        modelBuilder.ApplyConfiguration(new PatientDocument());
        modelBuilder.ApplyConfiguration(new Vacation());

        DataSeeder.DataSeeder.Seed(modelBuilder);
    }
}
