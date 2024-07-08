using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Entities;

public class DoctorSchedule : IEntityTypeConfiguration<DoctorSchedule>
{
       public Guid Id { get; set; }
       public Doctor Doctor { get; set; }
       public Guid DoctorId { get; set; }
       public DayOfWeek Day { get; set; }
       public TimeSpan StartTime { get; set; }
       public TimeSpan EndTime { get; set; }
       public TimeSpan VisitDuration { get; set; }

       public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
       {
              builder.HasKey(a => a.Id);

              builder.Property(a => a.Day)
                     .IsRequired();

              builder.Property(a => a.StartTime)
                     .IsRequired();

              builder.Property(a => a.EndTime)
                     .IsRequired();

              builder.Property(a => a.VisitDuration)
                     .IsRequired();

              builder.HasOne(a => a.Doctor)
                     .WithMany(d => d.DoctorSchedules)
                     .HasForeignKey(a => a.DoctorId);
       }
}
