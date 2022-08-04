using Common.DTO.Base;

namespace Common.DTO.Identity.Page.Parameters
{
    public class PageFilter : MainFilter
    {
        public long? Id { get; set; }
        public string AppNameEn { get; set; }
        public string AppNameAr { get; set; }
        public string Url { get; set; }
    }
}