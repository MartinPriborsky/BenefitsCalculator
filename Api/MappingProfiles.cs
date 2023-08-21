using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api
{
    // AutoMapper profile class for defining mappings between entities and DTOs
    // why: to avoid manual mapping
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Map from Employee entity to GetEmployeeDto
            CreateMap<Employee, GetEmployeeDto>()
                .ForMember(dest => dest.Dependents, opt => opt.MapFrom(src => src.Dependents));

            // Map from Dependent entity to GetDependentDto
            CreateMap<Dependent, GetDependentDto>();
        }
    }
}
