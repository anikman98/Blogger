using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blogger.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private string _imagePath;
        
        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];
            
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try { 
            
                var save_path = Path.Combine(_imagePath);

                if(!Directory.Exists(_imagePath))
                {
                    Directory.CreateDirectory(_imagePath);
                }

                var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";

                using (var fileStream = new FileStream(Path.Combine(save_path, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return fileName;
            }catch(Exception ex) { 
                Console.WriteLine(ex.Message);
                return "Error";
            }
        }
    }
}