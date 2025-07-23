using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTO;
using MediatR;


namespace Eskon.Core.Features.CityFeatures.Commands.Commands
{
    public record AddCityCommand(string name,string CountryName) : IRequest<Response<CityDTO>>;
}
