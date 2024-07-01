using AutoMapper;
using Shared.Models;
using Shared.Requests.Patient;
namespace Shared.Mappers;
public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<CreatePatientRequest, Patient>();
          
    }
}