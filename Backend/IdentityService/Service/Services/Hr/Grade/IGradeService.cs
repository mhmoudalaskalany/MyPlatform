using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Grade;
using Domain.DTO.Hr.Grade.Parameters;
using Service.Services.Base;

namespace Service.Services.Hr.Grade
{
    public interface IGradeService : IBaseService<Entities.Entities.Hr.Grade, AddGradeDto, GradeDto , long?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<GradeFilter> filter);
    }
}
