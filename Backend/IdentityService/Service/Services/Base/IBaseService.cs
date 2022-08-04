using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;

namespace Service.Services.Base
{
    public interface IBaseService<T, TDto , TGetDto , TKeyDto>
    {
        Task<IResult> GetAllAsync(bool disableTracking = false);
        Task<IResult> AddAsync(TDto model);
        Task<IResult> AddListAsync(List<TDto> model);
        Task<IResult> UpdateAsync(TDto model);
        Task<IResult> DeleteAsync(object id);
        Task<IResult> DeleteSoftAsync(object id);
        Task<IResult> GetByIdAsync(object id);
        Task<IResult> GetByIdForEditAsync(object id);
  



    }
}
