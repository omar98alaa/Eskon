using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Mapping.CityMapping
{
    public partial class CityMapping : Profile
    {
        public CityMapping()
        {
            UpdateCityMapping();
            ListCityMapping();
            GetCityMapping();
        
        }

    }
}
