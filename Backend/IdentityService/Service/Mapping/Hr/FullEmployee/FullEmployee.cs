using Domain.DTO.Hr.Employee;
using Domain.DTO.Hr.FullEmployee;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapFullEmployee()
        {
            CreateMap<FullEmployee, MurasalatEmployeeDto>()
                .ForMember(dest => dest.FullNameAr, opt => opt.MapFrom(src => src.ArFullName))

                .ForMember(dest => dest.FullNameEn, opt => opt.MapFrom(src => src.EnFullName))

                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))

                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.ArPositiontype))

                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.DepartmentCode))

                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.ArDepartmentName))

                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.CivilNumber.TrimStart('0')))


                .ForMember(dest => dest.FileId, opt => opt.MapFrom(src => src.Attachment.FileId))
                
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Attachment.Extension))

                //.ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.ArDepartmentName + "-" + src.ArParentDepartmentName + "-" + src.ArGrandParentDepartmentName + "-"  + src.ArGrandDepartmentName))
                .ReverseMap();

            CreateMap<AddMurasalatEmployeeDto, FullEmployee>()
                .ForMember(dest => dest.ArFullName, opt => opt.MapFrom(src => src.FullNameAr))

                .ForMember(dest => dest.EnFullName, opt => opt.MapFrom(src => src.FullNameEn))

                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))

                .ForMember(dest => dest.ArPositiontype, opt => opt.MapFrom(src => src.Position))

                .ForMember(dest => dest.EnPositiontype, opt => opt.MapFrom(src => src.Position))

                .ForMember(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.UnitId))

                .ForMember(dest => dest.ArDepartmentName, opt => opt.MapFrom(src => src.UnitName))

                .ForMember(dest => dest.CivilNumber, opt => opt.MapFrom(src => src.NationalId.TrimStart('0')))

                .ReverseMap();


            CreateMap<FullEmployee, NewEmployeeDto>()
                .ForMember(dest => dest.FullNameAr, opt => opt.MapFrom(src => src.ArFullName))

                .ForMember(dest => dest.FullNameEn, opt => opt.MapFrom(src => src.EnFullName))

                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))

                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.ArPositiontype))

                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.DepartmentCode))

                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.CivilNumber.TrimStart('0')))

                .ForPath(dest => dest.Manager.Id, opt => opt.MapFrom(src => src.ManagerId))

                .ForPath(dest => dest.Manager.Position, opt => opt.MapFrom(src => src.ArDirectManagerPosition))

                .ForPath(dest => dest.Manager.FullNameAr, opt => opt.MapFrom(src => src.ArDirectManagerName))

                .ForPath(dest => dest.Manager.FullNameEn, opt => opt.MapFrom(src => src.EnDirectManagerName))

                .ForPath(dest => dest.Manager.NationalId, opt => opt.MapFrom(src => src.DirectManagerCivilNumber.TrimStart('0')))

                .ReverseMap();


            CreateMap<FullEmployee, EmployeeProfileDto>()
                .ForMember(dest => dest.FullNameAr,
                    opt => opt.MapFrom(src => src.ArFullName))

                .ForMember(dest => dest.FullNameEn,
                    opt => opt.MapFrom(src => src.EnFullName))

                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => src.ArPositiontype))

                .ForMember(dest => dest.NationalId,
                    opt => opt.MapFrom(src => src.CivilNumber.TrimStart('0')))

                .ReverseMap();

        }
    }
}