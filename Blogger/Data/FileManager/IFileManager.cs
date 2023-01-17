using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Blogger.Data.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveImage(IFormFile image);
        FileStream ImageStream(string image);
    }
}