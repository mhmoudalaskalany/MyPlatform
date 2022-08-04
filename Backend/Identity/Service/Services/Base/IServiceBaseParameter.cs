using AutoMapper;
using Common.Abstraction.UnitOfWork;
using Common.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Service.Services.Base
{
    public interface IServiceBaseParameter<T> where T : class
    {
        IMapper Mapper { get; set; }
        IUnitOfWork<T> UnitOfWork { get; set; }
        IResponseResult ResponseResult { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
        IConfiguration Configuration { get; set; }
    }
}
