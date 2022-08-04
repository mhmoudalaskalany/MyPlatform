using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Nationality;
using Domain.DTO.Hr.Nationality.Parameters;
using Service.Services.Base;

namespace Service.Services.Hr.Nationality
{
    public interface INationalityService : IBaseService<Entities.Entities.Hr.Nationality, AddNationalityDto, NationalityDto , long?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<NationalityFilter> filter);
    }
}
