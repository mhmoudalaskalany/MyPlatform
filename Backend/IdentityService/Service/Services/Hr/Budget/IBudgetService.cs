using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Budget;
using Domain.DTO.Hr.Budget.Parameters;
using Service.Services.Base;

namespace Service.Services.Hr.Budget
{
    public interface IBudgetService : IBaseService<Entities.Entities.Hr.Budget, AddBudgetDto, BudgetDto , long?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<BudgetFilter> filter);
    }
}
