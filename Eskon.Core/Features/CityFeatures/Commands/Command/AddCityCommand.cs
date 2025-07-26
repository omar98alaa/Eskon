using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTO;
using MediatR;


namespace Eskon.Core.Features.CityFeatures.Commands.Commands
{
    public record AddCityCommand(CityDTO CityDTO) : IRequest<Response<CityDTO>>;
}
