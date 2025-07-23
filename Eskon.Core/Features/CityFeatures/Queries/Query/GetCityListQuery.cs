using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Domian.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.CityFeatures.Queries.Models
{
    public record GetCityListQuery : IRequest<Response<List<CityDTO>>>;
}
