public class Patient : UserBase
{
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required string PESEL { get; set; }
    public DateTime DateOfBirth { get; set; }

    public List<PatientDocument> Documents { get; set; }

    public Patient(string name, string surname, string email, string address, string phoneNumber, string pesel, DateTime dateOfBirth)
        : base(name, surname, email, UserRole.Patient)
    {
        Address = address;
        PhoneNumber = phoneNumber;
        PESEL = pesel;
        DateOfBirth = dateOfBirth;
        Documents = new List<PatientDocument>();
    }

    public void AddDocument(PatientDocument document)
    {
        Documents.Add(document);
    }
}
