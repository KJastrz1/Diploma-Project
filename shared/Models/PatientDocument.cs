public class PatientDocument
{
    public Guid Id { get; private set; }
    public Patient Patient { get; set; }
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
    public DateTime UploadDate { get; set; }

    public PatientDocument(Patient patient, string fileName, byte[] fileContent)
    {
        Id = Guid.NewGuid();
        Patient = patient;
        FileName = fileName;
        FileContent = fileContent;
        UploadDate = DateTime.Now;
    }
}
