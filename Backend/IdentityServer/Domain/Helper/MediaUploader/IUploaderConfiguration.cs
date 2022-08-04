using System.IO;
using System.Threading.Tasks;

namespace Domain.Helper.MediaUploader
{
    public interface IUploaderConfiguration
    {
        string SaveBase64(string fileBase64, string fileName, string folderName);
        Task<string> ConvertToBase64String(string fileName, string folderName);
        Stream ConvertToStream(string fileName, string folderName);
        void RemoveFile(string fileName, string folderName);
    }
}
