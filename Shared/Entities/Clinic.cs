using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Entities;

public class Clinic : IEntityTypeConfiguration<Clinic>
{
       public Guid Id { get; set; }
       public string Address { get; set; }
       public string PhoneNumber { get; set; }
       public List<Doctor> Doctors { get; set; }
       public List<Appointment> Appointments { get; set; }

       public void Configure(EntityTypeBuilder<Clinic> builder)
       {
              builder.HasKey(c => c.Id);

              builder.Property(c => c.Address)
                     .IsRequired()
                     .HasMaxLength(200);

              builder.Property(c => c.PhoneNumber)
                     .IsRequired()
                     .HasMaxLength(15);

              builder.HasMany(c => c.Doctors)
                     .WithOne(d => d.Clinic)
                     .HasForeignKey(d => d.ClinicId);

              builder.HasMany(c => c.Appointments)
                     .WithOne(a => a.Clinic)
                     .HasForeignKey(a => a.ClinicId);
       }
}
