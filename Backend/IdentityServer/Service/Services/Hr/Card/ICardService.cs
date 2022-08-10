using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.Card;
using Service.Services.Base;
using System;

namespace Service.Services.Hr.Card
{
    public interface ICardService : IBaseService<Entities.Entities.Hr.Card, AddCardDto, CardDto, long?>
    {
        /// <summary>
        /// Get Cards By Employee Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetByEmployeeIdAsync(Guid employeeId);
        /// <summary>
        /// Add Card
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<byte[]> AddCardAsync(AddCardDto model);
    }
}
