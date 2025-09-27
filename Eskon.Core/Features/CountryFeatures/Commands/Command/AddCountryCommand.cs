using Eskon.Core.Response;
using Eskon.Domian.DTOs.CountryDTOs;
using MediatR;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Commands
{
    public record AddCountryCommand(AddCountryDTO AddCountryDTO) : IRequest<Response<AddCountryDTO>>;
}
