using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
