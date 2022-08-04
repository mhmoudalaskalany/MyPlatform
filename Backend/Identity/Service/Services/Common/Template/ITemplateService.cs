using System.Threading.Tasks;
using Common.DTO.Common.Template;

namespace Service.Services.Common.Template
{
    public interface ITemplateService
    {
        Task<string> GetTemplate(string templateCode);
        Task<string> GenerateTemplateAsString(TemplateDto templateDto);
        string GenerateTemplateAsString(string template, TemplateDto dto);
        Task<byte[]> GetTemplateAsBytesByCode(TemplateDto templateDto);
        Task<byte[]> GetTemplateAsImageBytesByCode(TemplateDto dto);
        /// <summary>
        /// Generate Card Template
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<byte[]> GetCardTemplateAsBytesByCode(TemplateDto dto);
    }
}
