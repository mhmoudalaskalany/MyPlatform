namespace Domain.Core
{
    public interface IDataPaging
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int TotalPage { get; set; }
        IFinalResult Result { get; set; }
    }
}