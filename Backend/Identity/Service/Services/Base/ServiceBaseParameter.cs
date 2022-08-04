using AutoMapper;
using Common.Abstraction.UnitOfWork;
using Common.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Service.Services.Base
{
    public class ServiceBaseParameter<T> : IServiceBaseParameter<T> where T : class
    {

        #region Constructors

        public ServiceBaseParameter(IMapper mapper, IUnitOfWork<T> unitOfWork, IResponseResult responseResult, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            ResponseResult = responseResult;
            HttpContextAccessor = httpContextAccessor;
            Configuration = configuration;
        }

        #endregion


        #region Properties

        public IMapper Mapper { get; set; }
        public IUnitOfWork<T> UnitOfWork { get; set; }
        public IResponseResult ResponseResult { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public IConfiguration Configuration { get; set; }

        #endregion

    }
}
