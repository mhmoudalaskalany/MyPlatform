using Domain.DTO.Hr.Employee;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapEmployee()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.IsGovernmental,
                    opt => opt.MapFrom(src => src.EmployeeTypeId == 1))
                .ReverseMap();

            CreateMap<Employee, AddEmployeeDto>();


            CreateMap<Employee, EmployeeProfileDto>()
                .ForMember(dest => dest.IsGovernmental,
                    opt => opt.MapFrom(src => src.EmployeeTypeId == 1))
                .ReverseMap();

            CreateMap<AddEmployeeDto, Employee>()
                .ForMember(dest => dest.EmployeeTypeId,
                    opt => opt.MapFrom(src => src.IsGovernmental == true ? 1 : 2));
        }
    }
}