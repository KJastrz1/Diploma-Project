namespace Shared.Models;

public class PatientDocument
{
    public Guid Id { get; private set; }
    public Patient Patient { get; set; }
    public Guid PatientId { get; set; }
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
    public DateTime UploadDate { get; set; }

    public PatientDocument(Patient patient, string fileName, byte[] fileContent, DateTime uploadDate)
    {
        Id = Guid.NewGuid();
        Patient = patient;
        PatientId = patient.Id;
        FileName = fileName;
        FileContent = fileContent;
        UploadDate = uploadDate;
    }
}
