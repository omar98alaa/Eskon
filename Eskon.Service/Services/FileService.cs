using Eskon.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Eskon.Service.Services
{
    public class FileService : IFileService
    {
        #region Field
        private readonly IWebHostEnvironment _env;
        private readonly string webRootPath;
        #endregion

        #region Constructor
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
            webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }
        #endregion


        public async Task<string> SaveImageToFolderAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(webRootPath, "uploads", "images");

            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<bool> DeleteImageFromFolderAsync(string fileName)
        {
            var uploadsFolder = Path.Combine(webRootPath, "uploads", "images");

            var filePath = Path.Combine(uploadsFolder, fileName);

            try
            {
                File.Delete(filePath);
            }catch
            {
                return false;
            }

            return true;
        }
    }
}



