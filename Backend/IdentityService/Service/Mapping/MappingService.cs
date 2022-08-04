using AutoMapper;

namespace Service.Mapping
{
    public partial class MappingService : Profile
    {
        public MappingService()
        {
            #region Hr Profiles
            MapFullUnit();
            MapMurasalatUnit();
            MapFullEmployee();
            MapMurasalatEmployee();
            MapUnit();
            MapBudget();
            MapEmployee();
            MapNationality();
            MapGradeProfile();
            MapTeam();
            MapCard();

            #endregion

            #region Identity Profiles

            MapUser();
            MapRole();
            MapApp();
            MapUserApp();
            MapUserRole();
            MapPermission();
            MapPage();
            MapPagePermission();
            MapRolePermission();

            #endregion

            #region Common Profiles

            MapAttachment();

            #endregion


        }
    }
}
