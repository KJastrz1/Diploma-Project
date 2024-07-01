using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Models;

public class Admin : UserBase, IEntityTypeConfiguration<Admin>
{
       public Admin? AssignedBy { get; set; }
       public Guid AssignedByAdminId { get; set; }
       public DateTime? LastLogin { get; set; }
       public bool IsActive { get; set; }

       public Admin()
       {
              Role = UserRole.Admin;
       }

       public void Configure(EntityTypeBuilder<Admin> builder)
       {
              builder.HasKey(a => a.Id);

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

              builder.HasOne(a => a.AssignedBy)
                     .WithMany()
                     .HasForeignKey(a => a.AssignedByAdminId);

              builder.Property(a => a.LastLogin);

              builder.Property(a => a.IsActive)
                     .IsRequired();


       }
}
