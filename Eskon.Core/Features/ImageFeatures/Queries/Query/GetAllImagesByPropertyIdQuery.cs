using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Domian.DTOs.ImageDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.ImageFeatures.Queries.Models
{
  
    public record GetAllImagesByPropertyIdQuery(Guid PropertyId) : IRequest<Response<List<ImageReadDTO>>>;
}
