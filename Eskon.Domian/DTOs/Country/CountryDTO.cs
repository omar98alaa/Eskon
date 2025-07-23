using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Domian.DTOs.City;

namespace Eskon.Domian.DTOs.Country_City
{
    public class CountryDTO 
    {
        public string Name { get; set; }
        public List<CityListDTO> Cities { get; set; }
    }
}
