using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Bogus;


namespace Backend.DataSeeder;
public static class DataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var clinics = GenerateClinics();
        var doctors = GenerateDoctors(clinics);
        var patients = GeneratePatients();
        var appointments = GenerateAppointments(clinics, doctors, patients);
        var vacations = GenerateVacations(doctors);
        var doctorSchedules = GenerateDoctorSchedules(doctors);

        modelBuilder.Entity<Clinic>().HasData(clinics);
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Appointment>().HasData(appointments);
        modelBuilder.Entity<Vacation>().HasData(vacations);
        modelBuilder.Entity<DoctorSchedule>().HasData(doctorSchedules);
    }

    private static List<Clinic> GenerateClinics()
    {
        var clinicIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        var faker = new Faker<Clinic>()
            .RuleFor(c => c.Id, f => clinicIds[f.IndexFaker % clinicIds.Count])
            .RuleFor(c => c.Address, f => f.Address.StreetAddress())
            .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber());

        return faker.Generate(3);
    }

    private static List<Doctor> GenerateDoctors(List<Clinic> clinics)
    {
        var clinicIds = clinics.ConvertAll(c => c.Id);

        var faker = new Faker<Doctor>()
            .RuleFor(d => d.Id, f => Guid.NewGuid())
            .RuleFor(d => d.Name, f => f.Name.FirstName())
            .RuleFor(d => d.Surname, f => f.Name.LastName())
            .RuleFor(d => d.Email, f => f.Internet.Email())
            .RuleFor(d => d.MedicalLicenseNumber, f => f.Random.AlphaNumeric(10))
            .RuleFor(d => d.Specialty, f => f.Name.JobTitle())
            .RuleFor(d => d.OfficeNumber, f => f.Random.Number(1, 100).ToString())
            .RuleFor(d => d.ClinicId, f => f.PickRandom(clinicIds))
            .RuleFor(d => d.CreatedAt, f => f.Date.Past());

        return faker.Generate(15);
    }

    private static List<Patient> GeneratePatients()
    {
        var faker = new Faker<Patient>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Name.FirstName())
            .RuleFor(p => p.Surname, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(p => p.PESEL, f => f.Random.AlphaNumeric(11))
            .RuleFor(p => p.DateOfBirth, f => f.Date.Past(50, DateTime.Today.AddYears(-18)))
            .RuleFor(p => p.CreatedAt, f => f.Date.Past());

        return faker.Generate(10);
    }

    private static List<Appointment> GenerateAppointments(List<Clinic> clinics, List<Doctor> doctors, List<Patient> patients)
    {
        var clinicIds = clinics.ConvertAll(c => c.Id);
        var doctorIds = doctors.ConvertAll(d => d.Id);
        var patientIds = patients.ConvertAll(p => p.Id);

        var faker = new Faker<Appointment>()
            .RuleFor(a => a.Id, f => Guid.NewGuid())
            .RuleFor(a => a.ClinicId, f => f.PickRandom(clinicIds))
            .RuleFor(a => a.DoctorId, f => f.PickRandom(doctorIds))
            .RuleFor(a => a.PatientId, f => f.PickRandom(patientIds))
            .RuleFor(a => a.AppointmentDate, f => f.Date.Future())
            .RuleFor(a => a.EndDate, (f, a) => a.AppointmentDate.AddHours(1))
            .RuleFor(a => a.CreatedAt, f => f.Date.Past())
            .RuleFor(a => a.Notes, f => f.Lorem.Sentence());

        return faker.Generate(20);
    }

    private static List<Vacation> GenerateVacations(List<Doctor> doctors)
    {
        var doctorIds = doctors.ConvertAll(d => d.Id);

        var faker = new Faker<Vacation>()
            .RuleFor(v => v.Id, f => Guid.NewGuid())
            .RuleFor(v => v.DoctorId, f => f.PickRandom(doctorIds))
            .RuleFor(v => v.StartDate, f => f.Date.Future(1))
            .RuleFor(v => v.EndDate, (f, v) => v.StartDate.AddDays(7))
            .RuleFor(v => v.IsApproved, f => f.Random.Bool())
            .RuleFor(v => v.IsDenied, (f, v) => !v.IsApproved)
            .RuleFor(v => v.CreatedAt, f => f.Date.Past());

        return faker.Generate(10);
    }

    private static List<DoctorSchedule> GenerateDoctorSchedules(List<Doctor> doctors)
    {
        var schedules = new List<DoctorSchedule>();

        foreach (var doctor in doctors)
        {
            for (var day = DayOfWeek.Monday; day <= DayOfWeek.Friday; day++)
            {
                schedules.Add(new DoctorSchedule
                {
                    Id = Guid.NewGuid(),
                    DoctorId = doctor.Id,
                    Day = day,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    VisitDuration = new TimeSpan(0, 30, 0)
                });
            }
        }

        return schedules;
    }
}
