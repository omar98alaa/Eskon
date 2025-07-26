using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Infrastructure.Repositories;
using Eskon.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eskon.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly IFileService _fileService;

        public ImageService(IFileService fileService)
            {
                _fileService = fileService;
            }

        public async Task<string> UploadImageAndGetUrlAsync(IFormFile file)
        {
            var url = await _fileService.UploadImageAsync(file);
            return url;
        }

    }
}
