using Eskon.Domian.DTOs.ReviewDTOs;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.ReviewMapping
{
    public partial class ReviewMappings
    {
        public void ReviewWriteMapping()
        {
            CreateMap<Review, ReviewWriteDTO>().ReverseMap();
        }
    }
}
