namespace Shared.Models;

    public class Clinic
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Appointment> Appointments { get; set; }

        public Clinic(string name, string address, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Doctors = new List<Doctor>();
            Appointments = new List<Appointment>();

        }

        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
        }

        public void AddAppointment(Appointment appointment)
        {
            Appointments.Add(appointment);
        }
    }
