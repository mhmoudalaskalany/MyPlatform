using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.DTO.Common.Sms;
using Common.Helper.HttpClient.RestSharp;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Common.Helper.SmsHelper
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;
        private readonly IRestSharpContainer _restSharpContainer;
        private string _sender;
        private string _source;
        private string _user;
        private string _password;
        private string _url;
        public SmsService(IConfiguration configuration, IRestSharpContainer restSharpContainer)
        {
            _configuration = configuration;
            _restSharpContainer = restSharpContainer;
            InitializeParameters();
        }

        private void InitializeParameters()
        {
            _sender = _configuration["Sms:Sender"];
            _source = _configuration["Sms:Source"];
            _user = _configuration["Sms:User"];
            _password = _configuration["Sms:Password"];
            _url = _configuration["Sms:Url"];
        }
        public void SendSms(SmsDto dto)
        {
            try
            {
                string url = _url +
                             "?username=" + _user +
                             "&password=" + _password +
                             "&phoneno=" + dto.Phone +
                             "&message=" + dto.Message +
                             "&sender=" + _sender +
                             "&source=" + _source;
                WebClient client = new WebClient();
                client.OpenRead(url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }



        }

        public async Task SendBulkSms(SmsDto dto)
        {

            try
            {
                string url = $"{_url}UserId={_user}&Password={_password}&MobileNo={dto.Phone}&Message={dto.Message}&Lang=64&FlashSMS=N";
                WebClient client = new WebClient();
                var stream = await client.OpenReadTaskAsync(url);
                var streamReader = new StreamReader(stream);
                var str = await streamReader.ReadToEndAsync();



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<string> SendBulkSmsWithResult(SmsDto dto)
        {

            try
            {
                string url = $"{_url}UserId={_user}&Password={_password}&MobileNo={dto.Phone}&Message={dto.Message}&Lang=64&FlashSMS=N";
                WebClient client = new WebClient();
                var stream = await client.OpenReadTaskAsync(url);
                var streamReader = new StreamReader(stream);
                var str = await streamReader.ReadToEndAsync();
                return str;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<dynamic> SendPost(SmsDto dto)
        {
            var numbers = new List<string>();
            if (dto.Phone.Length == 8)
            {
                if (dto.Phone.All(char.IsDigit))
                {
                    numbers.Add($"968{dto.Phone}");
                }


            }
            else
            {
                if (dto.Phone.All(char.IsDigit))
                {
                    numbers.Add(dto.Phone);
                }


            }

            var arr = numbers.ToArray();
            var obj = new
            {
                UserID = _user,
                Password = _password,
                Message = dto.Message,
                Language = "64",
                ScheddateTime = DateTime.Now.ToString("MM/dd/yyyy"),
                MobileNo = arr,
                RecipientType = "1"
            };

            var http = new Dictionary<string, string>
            {
                {"Content-Type", "application/json"},
                {"Cache-Control", "no-cache"}
            };

            var result = await _restSharpContainer.SendRequest<dynamic>("https://ismartsms.net/RestApi/api/SMS/PostSMS", Method.Post,
                obj, headers: http);
            return result;


        }


    }
}
