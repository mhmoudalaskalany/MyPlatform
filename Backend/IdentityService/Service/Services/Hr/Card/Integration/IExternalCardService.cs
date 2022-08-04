using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.Card;
using Service.Services.Base;

namespace Service.Services.Hr.Card.Integration
{
    public interface IExternalCardService : IBaseService<Entities.Entities.Hr.Card, AddCardDto, CardDto, long?>
    {
        /// <summary>
        /// Get Card By Employee Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IResult> GetCardDetailsByEmployeeIdAsync(Guid employeeId);

    }
}
