using Domain.Core;

namespace Domain.DTO.Identity.Page
{
    public class AddPageDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public long AppId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Code { get; set; }
    }
}
