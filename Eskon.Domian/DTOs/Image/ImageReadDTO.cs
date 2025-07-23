using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Domian.DTOs.ImageDTO
{
    public class ImageReadDTO
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
