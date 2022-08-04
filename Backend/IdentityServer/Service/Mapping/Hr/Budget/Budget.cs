using Domain.DTO.Hr.Budget;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapBudget()
        {
            CreateMap<Budget, BudgetDto>()
                .ReverseMap();
            CreateMap<Budget, AddBudgetDto>()
                .ReverseMap();
        }
    }
}