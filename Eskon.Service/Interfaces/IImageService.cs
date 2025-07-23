using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAndGetUrlAsync(IFormFile file);
    }
}
