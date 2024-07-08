using Domain.Entities;
using Shared.Enums;

namespace Domain.Entities;
public class Admin : UserBase
{
       public Admin AssignedBy { get; set; }
       public Guid AssignedByAdminId { get; set; }
       public DateTime? LastLogin { get; set; }
       public bool IsActive { get; set; }

       public Admin()
       {
              Role = UserRole.Admin;
       }
}
