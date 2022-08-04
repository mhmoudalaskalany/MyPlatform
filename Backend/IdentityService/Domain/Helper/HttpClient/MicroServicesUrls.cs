using Microsoft.Extensions.Configuration;

namespace Domain.Helper.HttpClient
{
    public class MicroServicesUrls
    {
        private readonly IConfiguration _configuration;
        private readonly string _fileManagerBaseUrl;
        public MicroServicesUrls(IConfiguration configuration)
        {
            _configuration = configuration;
            _fileManagerBaseUrl = _configuration["MicroServicesBaseUrl:FileManager"];
        }
        /* File Service Urls */
        public string DownloadFile => _fileManagerBaseUrl + _configuration["MicroServicesEndPoints:FileManager:Download"];
        public string DownloadFileWithAppCode => _fileManagerBaseUrl + _configuration["MicroServicesEndPoints:FileManager:DownloadWithAppCode"];
        public string DownloadWithId => _fileManagerBaseUrl + _configuration["MicroServicesEndPoints:FileManager:DownloadWithId"];
        public string GenerateToken => _fileManagerBaseUrl + _configuration["MicroServicesEndPoints:FileManager:GenerateToken"];
        public string GenerateTokenWithClaims => _fileManagerBaseUrl +
                                       _configuration["MicroServicesEndPoints:FileManager:GenerateTokenWithClaims"];
        public string Delete => _fileManagerBaseUrl +
                                                 _configuration["MicroServicesEndPoints:FileManager:Delete"];
        public string UploadBytes => _fileManagerBaseUrl +
                                _configuration["MicroServicesEndPoints:FileManager:UploadBytes"];
    }
}
