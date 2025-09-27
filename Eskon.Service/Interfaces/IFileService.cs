using Microsoft.AspNetCore.Http;

namespace Eskon.Service.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveImageToFolderAsync(IFormFile file);

        Task<bool> DeleteImageFromFolderAsync(string fileName);
    }
}
