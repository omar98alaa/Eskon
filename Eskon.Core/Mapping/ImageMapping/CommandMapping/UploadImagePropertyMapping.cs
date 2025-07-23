using AutoMapper;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Domian.DTOs.ImageDTO;
using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Mapping.PropertyImageMapping
{
    partial class PropertyImageMapping : Profile
    {
        public void UploadPropertyImageMapping() {
            CreateMap<PropertyImageUploadDTO, Image>()
        .ForMember(dest => dest.Url, opt => opt.Ignore())
         .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.Property, opt => opt.Ignore())
        .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
