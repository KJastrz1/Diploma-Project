public class Clinic
{
    public Guid Id { get; private set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public List<Doctor> Doctors { get; set; }
    public List<Patient> Patients { get; set; }

    public Clinic(string name, string address, string phoneNumber)
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Doctors = new List<Doctor>();
        Patients = new List<Patient>();
    }

    public void AddDoctor(Doctor doctor)
    {
        Doctors.Add(doctor);
    }

    public void AddPatient(Patient patient)
    {
        Patients.Add(patient);
    }
}
