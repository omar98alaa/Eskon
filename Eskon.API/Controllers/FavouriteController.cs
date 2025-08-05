using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.FavouriteFeatures.Commands.Command;
using Eskon.Core.Features.FavouriteFeatures.Queries.Query;
using Eskon.Domian.DTOs.Favourite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public FavouriteController(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region GET
        [Authorize]
        [HttpGet("GetUserFavourites")]
        public async Task<IActionResult> GetUserFavourites()
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var query = new GetUserFavouritesQuery(userId);
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> AddToFavourites([FromRoute] Guid propertyId)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var command = new AddFavouriteCommand(userId,propertyId);
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        #endregion

        #region DELETE
        [Authorize]
        [HttpDelete("Remove")]
        public async Task<IActionResult> RemoveFromFavourites([FromRoute] Guid propertyId)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var command = new RemoveFavouriteCommand(userId, propertyId);
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        #endregion


    }
}
