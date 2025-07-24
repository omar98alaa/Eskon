using Microsoft.AspNetCore.Http;

namespace Eskon.Service.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
