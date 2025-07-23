using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Domian.DTOs.ImageDTO
{
    public class PropertyImageUploadDTO
    {
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        public Guid PropertyId { get; set; }
    }
}
