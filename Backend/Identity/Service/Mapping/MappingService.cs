using AutoMapper;

namespace Service.Mapping
{
    public partial class MappingService : Profile
    {
        public MappingService()
        {
            MapUser();
            MapRole();
            MapApp();
            MapUserApp();
            MapUserRole();
            MapPermission();
            MapPage();
            MapPagePermission();
            MapRolePermission();
            MapAttachment();
        }
    }
}
