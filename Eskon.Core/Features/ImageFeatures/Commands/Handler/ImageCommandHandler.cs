using Eskon.Core.Features.ImageFeatures.Commands.Command;
using Eskon.Core.Features.ImageFeatures.Commands.Commands;
using Eskon.Core.Response;
using Eskon.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Eskon.Core.Features.ImageFeatures.Commands.Handler
{
    public class ImageCommandHandler : ResponseHandler, IRequestHandler<UploadImageCommand, Response<string?>>,
        IRequestHandler<DeleteImageCommand, Response<string>>
    {
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _env;

        public ImageCommandHandler(IFileService fileService, IWebHostEnvironment env)
        {
            _fileService = fileService;
            _env = env;
        }

        public async Task<Response<string>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.FileName))
                return NotFound<string>("File name is required");

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "images");
            var filePath = Path.Combine(uploadsFolder, request.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Success("Image deleted successfully");
            }

            return NotFound<string>("Image not found");
        }



        async Task<Response<string?>> IRequestHandler<UploadImageCommand, Response<string?>>.Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
                return NotFound<string>("No file provided");


            var url = await _fileService.UploadImageAsync(request.File);
            return Success(url, "Image uploaded successfully");


        }


    }
}