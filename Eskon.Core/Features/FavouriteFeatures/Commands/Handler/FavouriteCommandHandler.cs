using AutoMapper;
using Eskon.Core.Features.FavouriteFeatures.Commands.Command;
using Eskon.Core.Features.FavouriteFeatures.Commands.Handler;
using Eskon.Core.Response;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;


namespace Eskon.Core.Features.PropertyFeatures.Commands.Handler
{
    public class PropertyCommandHandler : ResponseHandler, IFavouriteCommandHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;
        #endregion
        public PropertyCommandHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork, UserManager<User> userManager)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddFavouriteCommand request, CancellationToken cancellationToken)
        {
            var favourite = new Favourite
            {
                UserId = request.UserId,
                PropertyId = request.PropertyId
            }; await _serviceUnitOfWork.FavouriteService.AddFavouriteAsync(favourite);
            await _serviceUnitOfWork.SaveChangesAsync();

           
            return Created("Added to Favourites Successfully");
        }
        public async Task<Response<string>> Handle(RemoveFavouriteCommand request, CancellationToken cancellationToken)
        {
            var favourite = new Favourite
            {
                UserId = request.UserId,
                PropertyId = request.PropertyId
            };
            await _serviceUnitOfWork.FavouriteService.RemoveFavouriteAsync(favourite);
            await _serviceUnitOfWork.SaveChangesAsync();

            return Success($"property with ID: {request.PropertyId} removed from Favourites", message: $"property with ID:  {request.PropertyId} removed");
        }


    }
}