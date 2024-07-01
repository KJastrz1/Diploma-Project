using AutoMapper;
using Shared.Models;
using Shared.Requests.Patient;
using Shared.Responses.Patient;

namespace Shared.Mappers;
public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<CreatePatientRequest, Patient>();
        CreateMap<UpdatePatientRequest, Patient>()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Patient, GetPatientResponse>();

    }
}