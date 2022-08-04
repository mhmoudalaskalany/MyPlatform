using Domain.Core;
namespace Domain.DTO.Hr.Budget
{
    public class AddBudgetDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
