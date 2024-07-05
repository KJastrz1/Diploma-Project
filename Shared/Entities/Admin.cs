using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums;

namespace Shared.Entities;
public class Admin : UserBase, IEntityTypeConfiguration<Admin>
{
       public Admin AssignedBy { get; set; }
       public Guid AssignedByAdminId { get; set; }
       public DateTime? LastLogin { get; set; }
       public bool IsActive { get; set; }

       public Admin()
       {
              Role = UserRole.Admin;
       }

       public void Configure(EntityTypeBuilder<Admin> builder)
       {
             
              builder.HasOne(a => a.AssignedBy)
                     .WithMany()
                     .HasForeignKey(a => a.AssignedByAdminId);

              builder.Property(a => a.LastLogin);

              builder.Property(a => a.IsActive)
                     .IsRequired();
       }
}
