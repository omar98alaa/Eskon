using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTO;
using MediatR;

namespace Eskon.Core.Features.CityFeatures.Commands.Commands
{
 
   public record EditCityCommand(Guid id, CityUpdateDTO CityUpdateDTO) :IRequest<Response<CityUpdateDTO>>;


}
