using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.Card;
using Domain.Extensions;
using Service.Services.Base;

namespace Service.Services.Hr.Card.Integration
{
    public class ExternalCardService : BaseService<Entities.Entities.Hr.Card, AddCardDto, CardDto, long?>, IExternalCardService
    {
      
        public ExternalCardService(IServiceBaseParameter<Entities.Entities.Hr.Card> parameters) : base(parameters)
        {
           
        }

        #region Public Methods
        public async Task<IResult> GetCardDetailsByEmployeeIdAsync(Guid employeeId)
        {
            var card = await UnitOfWork.Repository.LastOrDefaultAsync(x => x.EmployeeId == employeeId , orderByCriteria:Utilities.GetOrderByList("CreatedDate" , "asc"));
            var data = Mapper.Map<CardDto>(card);
            return ResponseResult.PostResult(data, HttpStatusCode.OK);
        }
       

        #endregion

        #region Private Methods

        
        

        #endregion






    }

}
