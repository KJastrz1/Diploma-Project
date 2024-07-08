using Shared.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;
//dodac odniesienie do metody SlotFinder.FindAvailableSlots

namespace Infrastructure.DataSeeder;
public static class DataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var clinics = GenerateClinics();
        var doctors = GenerateDoctors(clinics);
        var patients = GeneratePatients();
        var doctorSchedules = GenerateDoctorSchedules(doctors);
        var vacations = GenerateVacations(doctors);
        var appointments = GenerateAppointments(clinics, doctors, patients, doctorSchedules, vacations);

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
            .RuleFor(c => c.PhoneNumber, f => GeneratePolishPhoneNumber(f));

        return faker.Generate(3);
    }

    private static string GeneratePolishPhoneNumber(Faker faker)
    {
        return $"+48 {faker.Random.Number(100, 999)}-{faker.Random.Number(100, 999)}-{faker.Random.Number(100, 999)}";
    }

    private static string GeneratePesel(Faker faker)
    {
        return faker.Random.ReplaceNumbers("###########");
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
            .RuleFor(d => d.CreatedAt, f => f.Date.Past().ToUniversalTime());


        return faker.Generate(15);
    }

    private static List<Patient> GeneratePatients()
    {
        var faker = new Faker<Patient>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Name.FirstName())
            .RuleFor(p => p.Surname, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.PhoneNumber, f => GeneratePolishPhoneNumber(f))
            .RuleFor(p => p.PESEL, f => GeneratePesel(f))
            .RuleFor(p => p.DateOfBirth, f => f.Date.Past(50, DateTime.Today.AddYears(-18)).ToUniversalTime())
            .RuleFor(p => p.CreatedAt, f => f.Date.Past().ToUniversalTime());

        return faker.Generate(10);
    }

    private static List<Appointment> GenerateAppointments(List<Clinic> clinics, List<Doctor> doctors, List<Patient> patients, List<DoctorSchedule> doctorSchedules, List<Vacation> vacations)
    {
        var clinicIds = clinics.ConvertAll(c => c.Id);
        var doctorIds = doctors.ConvertAll(d => d.Id);
        var patientIds = patients.ConvertAll(p => p.Id);

        var appointments = new List<Appointment>();
        var faker = new Faker();

        foreach (var doctor in doctors)
        {
            var schedulesForDoctor = doctorSchedules.Where(ds => ds.DoctorId == doctor.Id).ToList();
            var vacationsForDoctor = vacations.Where(v => v.DoctorId == doctor.Id).ToList();
            var startDate = DateTime.UtcNow;
            var endDate = DateTime.UtcNow.AddMonths(1);

            var availableSlots = SlotFinder.FindAvailableSlots(schedulesForDoctor, new List<Appointment>(), vacationsForDoctor, startDate, endDate);

            foreach (var slot in availableSlots)
            {
                if (faker.Random.Bool(0.5f))
                {
                    var appointment = new Appointment
                    {
                        Id = Guid.NewGuid(),
                        ClinicId = faker.PickRandom(clinicIds),
                        DoctorId = doctor.Id,
                        PatientId = faker.PickRandom(patientIds),
                        AppointmentDate = slot.StartTime,
                        EndDate = slot.EndTime,
                        CreatedAt = faker.Date.Past().ToUniversalTime(),
                        Notes = faker.Lorem.Sentence()
                    };

                    appointments.Add(appointment);
                }
            }
        }

        return appointments;
    }

    private static List<Vacation> GenerateVacations(List<Doctor> doctors)
    {
        var doctorIds = doctors.ConvertAll(d => d.Id);

        var faker = new Faker<Vacation>()
            .RuleFor(v => v.Id, f => Guid.NewGuid())
            .RuleFor(v => v.DoctorId, f => f.PickRandom(doctorIds))
            .RuleFor(v => v.StartDate, f => f.Date.Future(1).ToUniversalTime())
            .RuleFor(v => v.EndDate, (f, v) => v.StartDate.AddDays(7).ToUniversalTime())
            .RuleFor(v => v.IsApproved, f => f.Random.Bool())
            .RuleFor(v => v.IsDenied, (f, v) => !v.IsApproved)
            .RuleFor(v => v.CreatedAt, f => f.Date.Past().ToUniversalTime());

        return faker.Generate(10);
    }

    private static List<DoctorSchedule> GenerateDoctorSchedules(List<Doctor> doctors)
    {
        var faker = new Faker();
        var schedules = new List<DoctorSchedule>();

        foreach (var doctor in doctors)
        {
            var daysOfWeek = faker.Random.EnumValues<DayOfWeek>(faker.Random.Int(1, 5)).ToList();
            foreach (var day in daysOfWeek)
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

