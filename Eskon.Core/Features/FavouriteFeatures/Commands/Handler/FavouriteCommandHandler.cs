using AutoMapper;
using Eskon.Core.Features.FavouriteFeatures.Commands.Command;
using Eskon.Core.Features.FavouriteFeatures.Commands.Handler;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Favourite;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;


namespace Eskon.Core.Features.PropertyFeatures.Commands.Handler
{
    public class FavouriteCommandHandler : ResponseHandler, IFavouriteCommandHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;
        #endregion
        public FavouriteCommandHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<FavouriteReadDTO>> Handle(AddFavouriteCommand request, CancellationToken cancellationToken)
        {
            var property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.PropertyId);
            if (property == null)
                return NotFound<FavouriteReadDTO>("Property Not Found");

            var favourite = await _serviceUnitOfWork.FavouriteService.GetFavouriteForUserAndPropertyAsync(request.UserId, request.PropertyId);
            if (favourite != null)
                return BadRequest<FavouriteReadDTO>("This Property is already on your favourites list");

            favourite = new Favourite
            {
                UserId = request.UserId,
                PropertyId = request.PropertyId
            };

            await _serviceUnitOfWork.FavouriteService.AddFavouriteAsync(favourite);
            await _serviceUnitOfWork.SaveChangesAsync();
            
            var favouriteDTO = _mapper.Map<FavouriteReadDTO>(favourite); 

            return Created(favouriteDTO,"Property added to Favourites Succefully");
        }

        public async Task<Response<string>> Handle(RemoveFavouriteCommand request, CancellationToken cancellationToken)
        {
            var favourite = await _serviceUnitOfWork.FavouriteService.GetFavouriteByIdAsync(request.favouriteId);
            if (favourite == null)
            {
                return NotFound<string>($"Property Not Found in favourites");
            }

            if(favourite.UserId != request.UserId)
            {
                return Forbidden<string>();
            }

            await _serviceUnitOfWork.FavouriteService.RemoveFavouriteAsync(favourite);
            await _serviceUnitOfWork.SaveChangesAsync();

            return Success("Favourite successfully removed", message: "Favourite successfully removed");
        }


    }
}