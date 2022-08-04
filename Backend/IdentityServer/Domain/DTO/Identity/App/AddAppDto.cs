using Domain.Core;

namespace Domain.DTO.Identity.App
{
    public class AddAppDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string AppUrl { get; set; }
        public string AppIcon { get; set; }
        public string AppColor { get; set; }
        public string Code { get; set; }
        public bool IsPublic { get; set; }
    }
}