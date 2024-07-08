using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums;

namespace Shared.Entities;
public abstract class UserBase
{
       public Guid Id { get; set; }
       public string Name { get; set; }
       public string Surname { get; set; }
       public string Email { get; set; }
       public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
       public UserRole Role { get; set; }
}

public class UserBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : UserBase
{
       public virtual void Configure(EntityTypeBuilder<T> builder)
       {
              builder.HasKey(e => e.Id);

              builder.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

              builder.Property(e => e.Surname)
                  .IsRequired()
                  .HasMaxLength(100);

              builder.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(100);

              builder.Property(e => e.CreatedAt)
                  .IsRequired();

              builder.Property(e => e.Role)
                  .IsRequired()
                  .HasConversion<string>();
       }
}
