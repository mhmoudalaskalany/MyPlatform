using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Page;
using Domain.DTO.Identity.Page.Parameters;
using Service.Services.Base;

namespace Service.Services.Identity.Page
{
    public interface IPageService : IBaseService<Entities.Entities.Identity.Page, AddPageDto, PageDto , long?>
    {
        Task<IResult> GetPagesCountAsync();
        Task<IResult> GetByAppId(long appId);
        Task<DataPaging> GetAllPagedAsync(BaseParam<PageFilter> filter);
    }
}