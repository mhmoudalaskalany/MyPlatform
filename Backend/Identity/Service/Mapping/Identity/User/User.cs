using System.Linq;
using Common.DTO.Identity.User;
using Entities.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapUser()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Apps , opt =>
                    opt.MapFrom(src => src.UserApps.Select(x => x.App)))
                .ReverseMap();

            CreateMap<User, AddUserDto>()
                //.ForMember(dest => dest.Apps , 
                //    opt => opt.MapFrom(src => src.UserApps.Select(x => x.AppId)))
                //.ForMember(dest => dest.Employee.IsGovernmental , 
                //    opt => opt.MapFrom(src => src.Employee.EmployeeTypeId == 1))
                .ReverseMap();

            CreateMap<AddUserDto, User>()
                .ForMember(dest => dest.UserName , opt => opt.MapFrom(src => src.NationalId.TrimStart('0')))

                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.NationalId.TrimStart('0')))

                .ForMember(v => v.UserApps, opt => opt.Ignore())

                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.TrimStart('0').ToUpper()))

                .ForMember(dest => dest.NormalizedEmail , opt => opt.MapFrom(src => src.Email.ToUpper()))

                
               .AfterMap((vr, v) =>
                {
                    if (vr.Apps != null)
                    {
                        //user to list method to avoid exception (Collection was Modified During Iteration)
                        var removedApps = v.UserApps.Where(f => !vr.Apps.Contains(f.AppId)).ToList();
                        foreach (var f in removedApps)
                            v.UserApps.Remove(f);

                        var addedApps = vr.Apps.Where(id => v.UserApps.All(f => f.AppId != id))
                            .Select(id => new UserApp() { AppId = id });
                        foreach (var f in addedApps)
                            v.UserApps.Add(f);
                    }
                    
                });


            CreateMap<ActiveDirectoryUserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.LogonName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.LogonName))
                .ForMember(dest => dest.FullNameEn, opt => opt.MapFrom(src => src.DisplayName));
        }
    }
}
