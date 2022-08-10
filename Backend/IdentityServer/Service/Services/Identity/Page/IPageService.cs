using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Page;
using Domain.DTO.Identity.Page.Parameters;
using Service.Services.Base;

namespace Service.Services.Identity.Page
{
    public interface IPageService : IBaseService<Entities.Entities.Identity.Page, AddPageDto, PageDto , Guid?>
    {
        Task<IFinalResult> GetPagesCountAsync();
        Task<IFinalResult> GetByAppId(Guid appId);
        Task<DataPaging> GetAllPagedAsync(BaseParam<PageFilter> filter);
    }
}