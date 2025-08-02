using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IImageService
    {
        Task<Image?> GetImageByIdAsync(Guid Id);
        Task<Image?> GetImageByNameAsync(string imageName);
        Task<List<Image>> GetImagesByNameAsync(List<string> imagesNames);
        Task AddImageAsync(Image image);
        Task DeleteImageAsync(Image image);
    }
}
