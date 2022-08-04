using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Domain.Helper.MediaUploader
{
    public class UploaderConfiguration : IUploaderConfiguration
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        public UploaderConfiguration(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }
        public string SaveBase64(string fileBase64, string fileName, string folderName)
        {
            var path = $"{_hostingEnvironment.ContentRootPath}/{folderName}";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var (fileExtension, data) = fileBase64.GetBase64StringContents();
            var fileFullName = $"{fileName}-{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(path, fileFullName);
            var fileBytes = Convert.FromBase64String(data);
            File.WriteAllBytes(filePath, fileBytes);
            return fileFullName;
        }
        public async Task<string> ConvertToBase64String(string fileName, string folderName)
        {
            try
            {
                var path = $"{_hostingEnvironment.ContentRootPath}/{folderName}/{fileName}";
                var fileByte = await File.ReadAllBytesAsync(path);
                Stream stream = new MemoryStream(fileByte);
                var imgExtension = fileName.Split('.')[1];
                return $"data:{MimeTypeMap.GetMimeType(imgExtension)};base64,{Convert.ToBase64String(fileByte)}";
            }
            catch (Exception e)
            {
                var type = e.GetType();
                if (type.Name == "FileNotFoundException")
                {
                    return _configuration["DefaultImagePath"];
                }

                throw;
            }

        }
        public Stream ConvertToStream(string fileName, string folderName)
        {
            var path = $"{_hostingEnvironment.ContentRootPath}/{folderName}/{fileName}";
            var fileByte = File.ReadAllBytes(path);
            return new MemoryStream(fileByte);
        }
        public void RemoveFile(string fileName, string folderName)
        {
            var fullPath = $"{_hostingEnvironment.ContentRootPath}/{folderName}/{fileName}";
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }
    }
}
