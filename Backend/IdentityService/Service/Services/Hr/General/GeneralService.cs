using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Domain.DTO.Common.General;
using Domain.DTO.Common.Sms;
using Domain.Helper.SmsHelper;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;

namespace Service.Services.Hr.General
{
    public class GeneralService : IGeneralService
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ISmsService _smsService;
        public GeneralService(IHostEnvironment hostEnvironment, ISmsService smsService)
        {
            _hostEnvironment = hostEnvironment;
            _smsService = smsService;
        }
        #region Public Methods


        public async Task SendSmsFromExcel()
        {
            try
            {
                var employees = ReadExcel();
                var successEmployees = new List<PhoneExcelDto>();
                var failedEmployees = new List<PhoneExcelDto>();
                foreach (var employee in employees)
                {
                    var sms = new StringBuilder();
                    sms.Append("لدخول منصة إجادة استخدم ");
                    sms.AppendLine();
                    sms.Append("اسم المستخدم : الرقم المدنى");
                    sms.AppendLine();
                    sms.Append("كلمة السر :");
                    sms.AppendLine();
                    sms.AppendFormat($"{employee.Password}");
                    sms.AppendLine();
                    sms.Append("الرابط: https://ejada.gov.om");
                    var smsDto = new SmsDto
                    {
                        Phone = employee.Phone,
                        Message = sms.ToString()
                    };
                    var result = await _smsService.SendPost(smsDto);
                    if (result.Code == 1)
                    {
                        employee.Status = "تم الارسال";
                        employee.Reason = result["Message"];
                        successEmployees.Add(employee);
                    }
                    else
                    {
                        employee.Reason = result["Message"];
                        employee.Status = "فشل الارسال";
                        failedEmployees.Add(employee);
                    }
                    //Thread.Sleep(3000);
                }

                await WriteSheetAsync(successEmployees, "SentSms");
                await WriteSheetAsync(failedEmployees, "FailedSms");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        #endregion


        #region Private Methods

        private List<PhoneExcelDto> ReadExcel()
        {
            var path = _hostEnvironment.ContentRootPath;
            var phonePath = Path.Combine(path, "Files\\Excel\\Phones.xlsx");
            var phoneFile = new FileInfo(phonePath);
            var employees = new List<PhoneExcelDto>();

            using var levelPackage = new ExcelPackage(phoneFile);
            var phonesSheet = levelPackage.Workbook.Worksheets.First();

            for (var i = 2; i <= 2998; i++)
            {
                employees.Add(new PhoneExcelDto
                {
                    CivilNumber = phonesSheet.Cells[i, 1].Value.ToString(),
                    JobId = phonesSheet.Cells[i, 2].Value.ToString(),
                    Password = phonesSheet.Cells[i, 3].Value.ToString(),
                    Phone = phonesSheet.Cells[i, 4].Value.ToString(),
                    Name = phonesSheet.Cells[i, 5].Value.ToString()
                });
            }

            return employees;
        }


        private async Task WriteSheetAsync(List<PhoneExcelDto> employees , string sheetName)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("المرسل");
            var currentRow = 1;
            var properties = employees.First().GetType().GetProperties();
            var columnNumber = 0;

            foreach (var prop in properties)
            {
                worksheet.Cell(currentRow, ++columnNumber).Value = prop.Name;
            }

            foreach (var record in employees)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = record.CivilNumber;
                worksheet.Cell(currentRow, 2).Value = record.JobId;
                worksheet.Cell(currentRow, 3).Value = record.Password;
                worksheet.Cell(currentRow, 4).Value = record.Phone;
                worksheet.Cell(currentRow, 5).Value = record.Name;
                worksheet.Cell(currentRow, 6).Value = record.Status;
                worksheet.Cell(currentRow, 7).Value = record.Reason;
            }

            await using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            var path = _hostEnvironment.ContentRootPath;
            var phonePath = Path.Combine(path, "Files\\Excel\\");
            var exists = Directory.Exists(phonePath);

            if (!exists)
                Directory.CreateDirectory(phonePath);
            await File.WriteAllBytesAsync($"{sheetName}.xlsx", content);
        }

        #endregion

    }
}
