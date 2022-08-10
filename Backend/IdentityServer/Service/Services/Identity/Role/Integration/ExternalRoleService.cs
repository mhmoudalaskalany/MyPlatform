using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.Role;
using Service.Services.Base;

namespace Service.Services.Identity.Role.Integration
{
    public class ExternalRoleService : BaseService<Entities.Entities.Identity.Role, AddRoleDto, RoleDto , long?>, IExternalRoleService
    {
        public ExternalRoleService(IServiceBaseParameter<Entities.Entities.Identity.Role> parameters) : base(parameters)
        {

        }
        public async Task<IFinalResult> GetByAppIdAsync(long appId)
        {

            var entities = await UnitOfWork.Repository.FindAsync(r => r.AppId == appId);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Role>, IEnumerable<RoleDto>>(entities);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
    }
}
