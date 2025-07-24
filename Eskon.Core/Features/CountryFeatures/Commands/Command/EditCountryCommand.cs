using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country;
using MediatR;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Commands
{
    public record EditCountryCommand(int Id, CountryUpdateDTO CountryUpdateDTO)
        : IRequest<Response<CountryUpdateDTO>>;

}
