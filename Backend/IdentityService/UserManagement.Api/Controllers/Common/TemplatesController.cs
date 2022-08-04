using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Domain.DTO.Common.Template;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Services.Common.Template;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Common
{
    /// <summary>
    /// Budgets Controller
    /// </summary>
    public class TemplatesController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ITemplateService _templateService;
        /// <summary>
        /// Constructor
        /// </summary>
        public TemplatesController(ITemplateService templateService, IConfiguration configuration)
        {
            _templateService = templateService;
            _configuration = configuration;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByCode")]
        [AllowAnonymous]
        public async Task<string> GetByCodeAsync()
        {
            var templateCode = _configuration["Templates:Card_Template"];
            var parameters = new Dictionary<string, dynamic>
            {
                {"Job", "مبرمج"},
                {"Name", "محمود رجب جعفر"},
                {"Date", DateTime.Now.ToString(CultureInfo.InvariantCulture)},
                {"NationalId", "123456"}
            };
            var dto = new TemplateDto
            {
                TemplateCode = templateCode,
                Parameters = parameters
            };
            var result = await _templateService.GenerateTemplateAsString(dto);
            return result;
        }

        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmptyByCode")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmptyByCodeAsync()
        {
            var templateCode = _configuration["Templates:Empty_Template"];
            var parameters = new Dictionary<string, dynamic>
            {
                {"Job", "مبرمج"},
                {"Name", "محمود رجب جعفر"},
                {"NameEn", "Mahmoud Ragab Gafar Omar"},
                {"Date", DateTime.Now.ToShortDateString()},
                {"NationalId", "121431703"},
                {"FileNumber", "29000022"},
                {"PhotoUrl" , "https://apis.omsgd.local:447/FileManager/api/Files/Download/00f77b21-5692-454d-a482-edb8458de883?appCode=HR"}
            };
            var dto = new TemplateDto
            {
                TemplateCode = templateCode,
                Parameters = parameters
            };
            var result = await _templateService.GetTemplateAsBytesByCode(dto);
            var file = new FileContentResult(result, "application/pdf")
            {
                FileDownloadName = "Test.pdf"
            };
            return file;
        }

        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmptyImageByCode")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmptyImageByCode()
        {
            var templateCode = _configuration["Templates:Empty_Template"];
            var parameters = new Dictionary<string, dynamic>
            {
                {"Job", "مبرمج"},
                {"Name", "محمود رجب جعفر"},
                {"NameEn", "Mahmoud Ragab Gafar Omar"},
                {"Date", DateTime.Now.ToShortDateString()},
                {"NationalId", "121431703"},
                {"FileNumber", "29000022"},
                {"PhotoUrl" , "https://apis.omsgd.local:447/FileManager/api/Files/Download/00f77b21-5692-454d-a482-edb8458de883?appCode=HR"}
            };
            var dto = new TemplateDto
            {
                TemplateCode = templateCode,
                Parameters = parameters
            };
            var result = await _templateService.GetTemplateAsImageBytesByCode(dto);
            
            var file = new FileContentResult(result, "image/jpg")
            {
                FileDownloadName = "Test.jpg"
            };

            return file;

        }


    }
}
