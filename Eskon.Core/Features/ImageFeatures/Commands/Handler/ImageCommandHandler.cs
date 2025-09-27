using Eskon.Core.Features.ImageFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Image;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;

namespace Eskon.Core.Features.ImageFeatures.Commands.Handler
{
    public class ImageCommandHandler : ResponseHandler, IImageCommandHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        #endregion

        #region Constructors
        public ImageCommandHandler(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }
        #endregion

        #region Handlers
        public async Task<Response<ImageReadDTO>> Handle(SaveImageCommand request, CancellationToken cancellationToken)
        {
            var file = request.image;

            if (file == null || file.Length == 0)
                return BadRequest<ImageReadDTO>("No files uploaded");

            // Check file extension
            var validExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!validExtensions.Contains(extension))
                return BadRequest<ImageReadDTO>("Invalid file format");

            // Note: Check image dimension

            var fileName = await _serviceUnitOfWork.FileService.SaveImageToFolderAsync(file);

            await _serviceUnitOfWork.ImageService.AddImageAsync(new Image() { Url = fileName });

            await _serviceUnitOfWork.SaveChangesAsync();

            return Created(new ImageReadDTO() { ImageName = fileName });
        }

        public async Task<Response<string>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _serviceUnitOfWork.ImageService.GetImageByNameAsync(request.imageName);

            if (image == null)
            {
                return NotFound<string>("Image not found");
            }

            await _serviceUnitOfWork.ImageService.DeleteImageAsync(image);

            if (!await _serviceUnitOfWork.FileService.DeleteImageFromFolderAsync(request.imageName))
            {
                return NotFound<string>("Image file not found");
            }

            await _serviceUnitOfWork.SaveChangesAsync();

            return Success($"Image {request.imageName} deleted successfully");
        }
        #endregion
    }
}
