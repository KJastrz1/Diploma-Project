using AutoMapper;
using Shared.Models;
using Shared.Requests.Patient;
using Shared.Responses.Patient;
using Shared.Requests.Doctor;
using Shared.Responses.Doctor;
using Shared.Requests.Clinic;
using Shared.Responses.Clinic;
using Shared.Requests.DoctorSchedule;
using Shared.Responses.DoctorSchedule;

namespace Shared.Mappers;
public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<CreatePatientRequest, Patient>();
        CreateMap<UpdatePatientRequest, Patient>()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Patient, GetPatientResponse>();

        CreateMap<CreateDoctorRequest, Doctor>();
        CreateMap<UpdateDoctorRequest, Doctor>()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Doctor, GetDoctorResponse>();

        CreateMap<CreateClinicRequest, Clinic>();
        CreateMap<UpdateClinicRequest, Clinic>()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Clinic, GetClinicResponse>();

        CreateMap<CreateDoctorScheduleRequest, DoctorSchedule>();
        CreateMap<UpdateDoctorScheduleRequest, DoctorSchedule>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<DoctorSchedule, GetDoctorScheduleResponse>();


    }
}