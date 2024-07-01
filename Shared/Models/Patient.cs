using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Shared.Models;
public class Patient : UserBase, IEntityTypeConfiguration<Patient>
{
       public string PhoneNumber { get; set; }
       public string PESEL { get; set; }
       public DateTime DateOfBirth { get; set; }
       public List<PatientDocument> Documents { get; set; }
       public List<Appointment> Appointments { get; set; }

       public Patient()
       {
              Role = UserRole.Patient;
       }

       public void Configure(EntityTypeBuilder<Patient> builder)
       {
              builder.HasKey(p => p.Id);

              builder.Property(a => a.Name)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(a => a.Surname)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(a => a.Email)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(a => a.CreatedAt)
                     .IsRequired();

              builder.Property(a => a.Role)
                     .IsRequired()
                     .HasConversion<string>();

              builder.Property(p => p.PhoneNumber)
                     .IsRequired()
                     .HasMaxLength(15);

              builder.Property(p => p.PESEL)
                     .IsRequired()
                     .HasMaxLength(11);

              builder.Property(p => p.DateOfBirth)
                     .IsRequired();


              builder.HasMany(p => p.Documents)
                     .WithOne(d => d.Patient)
                     .HasForeignKey(d => d.PatientId);

              builder.HasMany(p => p.Appointments)
                     .WithOne(a => a.Patient)
                     .HasForeignKey(a => a.PatientId);
       }
}
