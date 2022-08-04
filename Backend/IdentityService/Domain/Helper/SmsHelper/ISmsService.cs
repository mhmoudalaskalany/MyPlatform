using System.Threading.Tasks;
using Domain.DTO.Common.Sms;

namespace Domain.Helper.SmsHelper
{
    public interface ISmsService
    {
        void SendSms(SmsDto dto);
        Task SendBulkSms(SmsDto dto);
        Task<string> SendBulkSmsWithResult(SmsDto dto);
        Task<dynamic> SendPost(SmsDto dto);
    }
}
