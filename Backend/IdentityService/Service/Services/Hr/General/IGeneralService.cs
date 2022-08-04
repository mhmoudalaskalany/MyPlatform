using System.Threading.Tasks;

namespace Service.Services.Hr.General
{
    public interface IGeneralService
    {
        Task SendSmsFromExcel();
    }
}
