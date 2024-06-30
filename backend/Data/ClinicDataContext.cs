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

        modelBuilder.Entity<Doctor>()
            .HasMany(d => d.Availabilities)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId);

        modelBuilder.Entity<Doctor>()
            .HasMany(d => d.Vacations)
            .WithOne(v => v.Doctor)
            .HasForeignKey(v => v.DoctorId);

        modelBuilder.Entity<Doctor>()
            .HasMany(d => d.Appointments)
            .WithOne(ap => ap.Doctor)
            .HasForeignKey(ap => ap.DoctorId);

        modelBuilder.Entity<Appointment>()
            .HasOne(ap => ap.Clinic)
            .WithMany(c => c.Appointments)
            .HasForeignKey(ap => ap.ClinicId);

        modelBuilder.Entity<Appointment>()
            .HasOne(ap => ap.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(ap => ap.DoctorId);

        modelBuilder.Entity<Appointment>()
            .HasOne(ap => ap.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(ap => ap.PatientId);

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Documents)
            .WithOne(pd => pd.Patient)
            .HasForeignKey(pd => pd.PatientId);

        modelBuilder.Entity<Clinic>()
            .HasMany(c => c.Doctors)
            .WithOne(d => d.Clinic)
            .HasForeignKey(d => d.ClinicId);

        modelBuilder.Entity<PatientDocument>()
            .HasOne(pd => pd.Patient)
            .WithMany(p => p.Documents)
            .HasForeignKey(pd => pd.PatientId);

        modelBuilder.Entity<Vacation>()
            .HasOne(v => v.Doctor)
            .WithMany(d => d.Vacations)
            .HasForeignKey(v => v.DoctorId);

        modelBuilder.Entity<Availability>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Appointment>()
            .HasKey(ap => ap.Id);

        modelBuilder.Entity<PatientDocument>()
            .HasKey(pd => pd.Id);

        modelBuilder.Entity<Vacation>()
            .HasKey(v => v.Id);

        modelBuilder.Entity<Doctor>()
            .Property(d => d.MedicalLicenseNumber)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Doctor>()
            .Property(d => d.Specialty)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Doctor>()
            .Property(d => d.OfficeNumber)
            .IsRequired()
            .HasMaxLength(20);

        modelBuilder.Entity<Clinic>()
            .Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Clinic>()
            .Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

        modelBuilder.Entity<Patient>()
            .Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Patient>()
            .Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

        modelBuilder.Entity<Patient>()
            .Property(p => p.PESEL)
            .IsRequired()
            .HasMaxLength(11);

        modelBuilder.Entity<Admin>()
            .Property(a => a.DateAssigned)
            .IsRequired();

        modelBuilder.Entity<Admin>()
            .HasOne(a => a.AssignedBy)
            .WithMany()
            .HasForeignKey(a => a.AssignedByAdminId);
    }
}
