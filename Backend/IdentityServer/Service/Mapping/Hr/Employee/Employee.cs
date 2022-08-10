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
                .ReverseMap();

            CreateMap<Employee, AddEmployeeDto>().ReverseMap();


            CreateMap<Employee, EmployeeProfileDto>()
                .ReverseMap();

        }
    }
}