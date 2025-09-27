using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTOs;
using MediatR;

namespace Eskon.Core.Features.CityFeatures.Commands.Commands
{
 
   public record EditCityCommand(string name,CityUpdateDTO CityUpdateDTO) :IRequest<Response<CityUpdateDTO>>;


}
