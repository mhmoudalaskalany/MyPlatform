using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Core;

namespace Service.Services.Base
{
    public interface IBaseService<T, TDto , TGetDto , TKeyDto>
    {
        Task<IFinalResult> GetAllAsync(bool disableTracking = false);
        Task<IFinalResult> AddAsync(TDto model);
        Task<IFinalResult> AddListAsync(List<TDto> model);
        Task<IFinalResult> UpdateAsync(TDto model);
        Task<IFinalResult> DeleteAsync(object id);
        Task<IFinalResult> DeleteSoftAsync(object id);
        Task<IFinalResult> GetByIdAsync(object id);
        Task<IFinalResult> GetByIdForEditAsync(object id);
  



    }
}
