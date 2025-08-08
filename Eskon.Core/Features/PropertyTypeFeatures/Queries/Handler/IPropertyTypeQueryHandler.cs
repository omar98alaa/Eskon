using Eskon.Core.Features.CountryFeatures.Queries.Models;
using Eskon.Core.Features.PropertyTypeFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Domian.DTOs.PropertyType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.PropertyTypeFeatures.Queries.Handler
{
    public interface IPropertyTypeQueryHandler : IRequestHandler<GetAllPropertyTypesQuery, Response<List<PropertyTypeDTO>>>;


}
