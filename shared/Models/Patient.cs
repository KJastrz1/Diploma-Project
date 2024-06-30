namespace Shared.Models;

public class Patient : UserBase
{
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string PESEL { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<PatientDocument> Documents { get; set; }
    public List<Appointment> Appointments { get; set; }

    public void AddDocument(PatientDocument document)
    {
        Documents.Add(document);
    }
    public void AddAppointment(Appointment appointment)
    {
        Appointments.Add(appointment);
    }
}
