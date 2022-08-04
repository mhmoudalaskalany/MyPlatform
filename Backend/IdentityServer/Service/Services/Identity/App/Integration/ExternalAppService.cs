using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.App;
using Service.Services.Base;

namespace Service.Services.Identity.App.Integration
{
    public class ExternalAppService : BaseService<Entities.Entities.Identity.App, AddAppDto, AppDto, long?>, IExternalAppService
    {
       
        public ExternalAppService(IServiceBaseParameter<Entities.Entities.Identity.App> businessBaseParameter) : base(
            businessBaseParameter)
        {
          
        }

        #region Public Methods

        public async Task<IResult> GetLoggedUserAppsAsync()
        {
            var appIds = await UnitOfWork.GetRepository<Entities.Entities.Identity.UserApp>().FindSelectAsync(x => new
            {
                x.AppId
            }, x => x.UserId == long.Parse(ClaimData.UserId));
            var apps = await UnitOfWork.Repository.FindAsync(x => appIds.Select(a => a.AppId).Contains(x.Id));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.App>, List<AppDto>>(apps);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
       
        #endregion


        #region Private Methods

       

        #endregion


    }
}
