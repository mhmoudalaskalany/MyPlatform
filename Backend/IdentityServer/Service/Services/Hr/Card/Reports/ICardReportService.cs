using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.Card;
using Service.Services.Base;

namespace Service.Services.Hr.Card.Reports
{
    public interface ICardReportService : IBaseService<Entities.Entities.Hr.Card, AddCardDto, CardDto, long?>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IFinalResult> GetGeneralReportAsync(GeneralReportFilter filter);


    }
}
