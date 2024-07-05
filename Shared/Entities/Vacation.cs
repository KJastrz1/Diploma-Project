using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Entities;

public class Vacation : IEntityTypeConfiguration<Vacation>
{
       public Guid Id { get; private set; }
       public Doctor Doctor { get; set; }
       public Guid DoctorId { get; set; }
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
       public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
       public bool IsApproved { get; set; }
       public bool IsDenied { get; set; }


       public void Configure(EntityTypeBuilder<Vacation> builder)
       {
              builder.HasKey(v => v.Id);

              builder.Property(v => v.CreatedAt)
                     .IsRequired();

              builder.Property(v => v.IsApproved)
                     .IsRequired();

              builder.Property(v => v.IsDenied)
                     .IsRequired();

              builder.Property(v => v.StartDate)
                     .IsRequired();

              builder.Property(v => v.EndDate)
                     .IsRequired();

              builder.HasOne(v => v.Doctor)
                     .WithMany(d => d.Vacations)
                     .HasForeignKey(v => v.DoctorId);
       }
}
