using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Models;

public class Appointment : IEntityTypeConfiguration<Appointment>
{
       public Guid Id { get; set; }
       public Guid ClinicId { get; set; }
       public Clinic Clinic { get; set; }
       public Guid DoctorId { get; set; }
       public Doctor Doctor { get; set; }
       public Guid PatientId { get; set; }
       public Patient Patient { get; set; }
       public DateTime AppointmentDate { get; set; }
       public DateTime CreatedAt { get; set; }
       public string? Notes { get; set; }

       public Appointment()
       {
              CreatedAt = DateTime.Now;
       }

       public void Configure(EntityTypeBuilder<Appointment> builder)
       {
              builder.HasKey(a => a.Id);

              builder.Property(a => a.AppointmentDate)
                     .IsRequired();

              builder.Property(a => a.CreatedAt);

              builder.Property(a => a.Notes)
                     .HasMaxLength(1000);

              builder.HasOne(a => a.Clinic)
                     .WithMany(c => c.Appointments)
                     .HasForeignKey(a => a.ClinicId);

              builder.HasOne(a => a.Doctor)
                     .WithMany(d => d.Appointments)
                     .HasForeignKey(a => a.DoctorId);

              builder.HasOne(a => a.Patient)
                     .WithMany(p => p.Appointments)
                     .HasForeignKey(a => a.PatientId);
       }
}
