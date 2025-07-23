using AutoMapper;
using Eskon.Domian.DTOs.ImageDTO;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.PropertyImageMapping
{
    partial class PropertyImageMapping : Profile
    {
        public void ReadPropertyImage()
        {

            CreateMap<Image, ImageReadDTO>();
        }

    }
}
