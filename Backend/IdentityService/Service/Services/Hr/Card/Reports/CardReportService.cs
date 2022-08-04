using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Common.Template;
using Domain.DTO.Hr.Card;
using Domain.DTO.Hr.FullEmployee;
using Domain.Helper.HttpClient;
using Entities.Enum;
using Integration.FileRepository;
using Integration.FileRepository.Dtos;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;
using Service.Services.Common.Template;

namespace Service.Services.Hr.Card.Reports
{
    public class CardReportService : BaseService<Entities.Entities.Hr.Card, AddCardDto, CardDto, long?>, ICardReportService
    {
        private readonly MicroServicesUrls _urls;
        private readonly ITemplateService _templateService;
        private readonly IFileRepository _fileRepository;
        public CardReportService(IServiceBaseParameter<Entities.Entities.Hr.Card> parameters, ITemplateService templateService, IFileRepository fileRepository, MicroServicesUrls urls) : base(parameters)
        {
            _templateService = templateService;
            _fileRepository = fileRepository;
            _urls = urls;
        }

        #region Public Methods

        public async Task<IResult> GetGeneralReportAsync(GeneralReportFilter filter)
        {
            var predicate = GeneralPredicateBuilder(filter);
            var cards = await UnitOfWork.Repository.FindAsync(predicate, include: src =>

                src.Include(x => x.Employee)
                    

            );
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Card>, IEnumerable<CardDto>>(cards);
            return ResponseResult.PostResult(data, HttpStatusCode.OK);
        }

        #endregion

            #region Private Methods

            static Expression<Func<Entities.Entities.Hr.Card, bool>> GeneralPredicateBuilder(GeneralReportFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Card>(x => (bool)!x.IsDeleted);

            if (filter?.EmployeeId!= null)
            {
                predicate = predicate.And(x => x.EmployeeId == filter.EmployeeId);
            }
            if (!string.IsNullOrEmpty(filter?.JobTitle))
            {
                predicate = predicate.And(x => x.Employee.ArPositiontype.Equals(filter.JobTitle));
            }
            if (filter?.FromDate != null )
            {
                predicate = predicate.And(x => x.CreatedDate >= filter.FromDate);
            }

            if (filter?.ToDate != null)
            {
                predicate = predicate.And(x => x.CreatedDate <= filter.ToDate);
            }

            if (filter?.ToDate != null && filter?.FromDate != null)
            {
                predicate = predicate.And(x => x.CreatedDate >= filter.FromDate && x.CreatedDate <= filter.ToDate);
            }





            return predicate;
        }

        #endregion






    }

}
