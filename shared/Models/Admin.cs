namespace Shared.Models;

public class Admin : UserBase
{
    public DateTime DateAssigned { get; set; }
    public Admin? AssignedBy { get; set; }
    public Guid AssignedByAdminId { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; }
}

