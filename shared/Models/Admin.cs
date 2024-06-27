public class Admin : UserBase
{
    public DateTime DateAssigned { get; set; }
    public Guid AssignedByAdminId { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; }

    public Admin(string name, string surname, string email, UserRole role, DateTime dateAssigned, Guid assignedByAdminId)
        : base(name, surname, email, role)
    {
        DateAssigned = dateAssigned;
        AssignedByAdminId = assignedByAdminId;
        LastLogin = null;
        IsActive = true;
    }
}
