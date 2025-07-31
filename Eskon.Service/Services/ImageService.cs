using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        public async Task AddImageAsync(Image image)
        {
            await _imageRepository.AddAsync(image);
        }

        public async Task DeleteImageAsync(Image image)
        {
            await _imageRepository.DeleteAsync(image);
        }

        public async Task<Image?> GetImageByIdAsync(Guid Id)
        {
            return await _imageRepository.GetByIdAsync(Id);
        }

        public async Task<Image?> GetImageByNameAsync(string imageName)
        {
            return (await _imageRepository.GetFilteredAsync(i => i.Url == imageName)).FirstOrDefault();
        }
    }
}
