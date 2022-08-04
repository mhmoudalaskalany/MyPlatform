using System.Threading.Tasks;
using Common.DTO.Common.Sms;

namespace Common.Helper.SmsHelper
{
    public interface ISmsService
    {
        void SendSms(SmsDto dto);
        Task SendBulkSms(SmsDto dto);
        Task<string> SendBulkSmsWithResult(SmsDto dto);
        Task<dynamic> SendPost(SmsDto dto);
    }
}
