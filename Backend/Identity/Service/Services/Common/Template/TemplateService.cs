using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Abstraction.UnitOfWork;
using Common.DTO.Common.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SelectPdf;


namespace Service.Services.Common.Template
{
    public class TemplateService : ITemplateService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork<Entities.Entities.Common.Template> _templateUnitOfWork;
        public TemplateService(IUnitOfWork<Entities.Entities.Common.Template> templateUnitOfWork, IConfiguration configuration)
        {
            _templateUnitOfWork = templateUnitOfWork;
            _configuration = configuration;
        }
        /// <summary>
        /// Get By Code
        /// </summary>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        public async Task<string> GetTemplate(string templateCode)
        {
            var entity = await _templateUnitOfWork.Repository
                .FirstOrDefaultAsync(a => a.TemplateCode == templateCode,
                    include: src => src.Include(t => t.TemplateForms));
            return entity.TemplateForms.FirstOrDefault()?.Form;
        }
        /// <summary>
        /// Generate As String
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> GenerateTemplateAsString(TemplateDto dto)
        {

            var template = await GetTemplate(dto.TemplateCode);
            if (string.IsNullOrWhiteSpace(template))
            {
                throw new Exception($"Template {dto.TemplateCode} Not Found In Database");
            }
            var result = PrepareTemplate(dto, template);
            return result;

        }
        /// <summary>
        /// Generate As String
        /// </summary>
        /// <param name="template"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string GenerateTemplateAsString(string template, TemplateDto dto)
        {

            var result = PrepareTemplate(dto, template);
            return result;

        }
        /// <summary>
        /// Get As Bytes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> GetTemplateAsBytesByCode(TemplateDto dto)
        {

            var entity = await _templateUnitOfWork.Repository
                .FirstOrDefaultAsync(a => a.TemplateCode == dto.TemplateCode,
                    include: src => src.Include(t => t.TemplateForms));
            var template = entity?.TemplateForms?.FirstOrDefault()?.Form;

            if (string.IsNullOrWhiteSpace(template))
            {
                throw new Exception($"Template {dto.TemplateCode} Not Found In Database");
            }
            var result = PrepareTemplate(dto, template);
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(result);
            var pdfBytes = doc.Save();

            doc.Close();

            return pdfBytes;

        }

        /// <summary>
        /// Get Card As Bytes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> GetCardTemplateAsBytesByCode(TemplateDto dto)
        {

            try
            {
                var entity = await _templateUnitOfWork.Repository
                    .FirstOrDefaultAsync(a => a.TemplateCode == dto.TemplateCode,
                        include: src => src.Include(t => t.TemplateForms));
                var back = await _templateUnitOfWork.Repository
                    .FirstOrDefaultAsync(a => a.TemplateCode == "Back_Template",
                        include: src => src.Include(t => t.TemplateForms));
                var backForm = back.TemplateForms.First().Form;
                var frontForm = entity?.TemplateForms?.FirstOrDefault()?.Form;

                if (string.IsNullOrWhiteSpace(frontForm))
                {
                    throw new Exception($"Template {dto.TemplateCode} Not Found In Database");
                }
                var  result = PrepareTemplate(dto, frontForm);
                HtmlToPdf converter = new HtmlToPdf();
                var frontWebWidth = int.Parse(_configuration["CardConfig:Front:WebWidth"]);
                var frontWebHeight = int.Parse(_configuration["CardConfig:Front:WebHeight"]);
                var frontPdfWidth = float.Parse(_configuration["CardConfig:Front:PdfWidth"]);
                var frontPdfHeight = float.Parse(_configuration["CardConfig:Front:PdfHeight"]);
                converter.Options.PdfPageSize = PdfPageSize.Custom;
                converter.Options.PdfPageCustomSize = new SizeF(frontPdfWidth, frontPdfHeight);
                converter.Options.WebPageFixedSize = true;
                converter.Options.WebPageWidth = frontWebWidth;
                converter.Options.WebPageHeight = frontWebHeight;

                var doc = converter.ConvertHtmlString(result);

                HtmlToPdf converter2 = new HtmlToPdf();
                converter2.Options.PdfPageSize = PdfPageSize.Custom;
                converter2.Options.PdfPageCustomSize = new SizeF(frontPdfWidth, frontPdfHeight);
                converter2.Options.WebPageFixedSize = true;
                converter2.Options.WebPageWidth = frontWebWidth;
                converter2.Options.WebPageHeight = frontWebHeight;
                var doc2 = converter2.ConvertHtmlString(backForm);
                




                var finalDoc = new PdfDocument();
                foreach (PdfPage docPage in doc.Pages)
                {
                    finalDoc.Pages.Add(docPage);
                }
                foreach (PdfPage docPage in doc2.Pages)
                {
                    finalDoc.Pages.Add(docPage);
                }

                var finalBytes = finalDoc.Save();
                finalDoc.Close();
                return finalBytes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

        }

        /// <summary>
        /// Get As Bytes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<byte[]> GetTemplateAsImageBytesByCode(TemplateDto dto)
        {

            var entity = await _templateUnitOfWork.Repository
                .FirstOrDefaultAsync(a => a.TemplateCode == dto.TemplateCode,
                    include: src => src.Include(t => t.TemplateForms));
            var template = entity?.TemplateForms?.FirstOrDefault()?.Form;

            if (string.IsNullOrWhiteSpace(template))
            {
                throw new Exception($"Template {dto.TemplateCode} Not Found In Database");
            }
            var result = PrepareTemplate(dto, template);
            HtmlToImage converter = new HtmlToImage
            {
                WebPageFixedSize = true,
                WebPageWidth = 460, 
                WebPageHeight = 280,
            };
            System.Drawing.Image image = converter.ConvertHtmlString(result);
            using var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            var bytes = ms.ToArray();
            return bytes;


        }

        #region Private Methods
        /// <summary>
        /// Prepare Template
        /// </summary>
        /// <param name="templateDto"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        private string PrepareTemplate(TemplateDto templateDto, string template)
        {


            var filledTemplate = FillTemplate(templateDto, template);
            return filledTemplate;


        }
        /// <summary>
        /// Fill Template
        /// </summary>
        /// <param name="templateDto"></param>
        /// <param name="template"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        private string FillTemplate(TemplateDto templateDto, string template, char delimiter = ' ')
        {
            Antlr4.StringTemplate.Template stringTemplate;
            if (delimiter == ' ')
            {
                stringTemplate = new Antlr4.StringTemplate.Template(template, '$', '$');
            }
            else
            {
                stringTemplate = new Antlr4.StringTemplate.Template(template, delimiter, delimiter);
            }


            foreach (var parameter in templateDto.Parameters)
            {
                if (!(parameter.Value is string))
                {
                    var parameters = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(parameter.Value.ToString());

                    stringTemplate.Add(parameter.Key, parameters);
                }
                else
                {
                    string key = parameter.Key;
                    string value = parameter.Value;

                    if (parameter.Key.Contains("image"))
                    {
                        value = ConvertImageUrlToBase64(parameter.Value);
                    }

                    stringTemplate.Add(key, value);
                }
            }

            var renderedTemplate = stringTemplate.Render();

            return renderedTemplate;


        }

        /// <summary>
        /// Convert Image
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        private string ConvertImageUrlToBase64(string imageUrl)
        {
            string imageBase64 = "";
            using (WebClient client = new WebClient())
            {
                var imageBytes = client.DownloadData(imageUrl);

                imageBase64 = Convert.ToBase64String(imageBytes);
            }

            return "data:image/jpeg;base64," + imageBase64;
        }

        #endregion


    }
}
