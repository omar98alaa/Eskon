using Eskon.Core.Response;
using AutoMapper;
using Eskon.Service.UnitOfWork;
using Eskon.Domian.DTOs.Favourite;
using Eskon.Core.Features.FavouriteFeatures.Queries.Query;
using Eskon.Domain.Utilities;


namespace Eskon.Core.Features.FavouriteFeatures.Queries.Handler
{
    public class FavouriteQueryHandler : ResponseHandler, IFavouriteQueryHandler
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        #endregion

        #region Constructor
        public FavouriteQueryHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork)
        {
            _mapper = mapper;
            _serviceUnitOfWork = serviceUnitOfWork;
        }
        #endregion

        public async Task<Response<Paginated<FavouriteReadDTO>>> Handle(GetUserFavouritesQuery request, CancellationToken cancellationToken)
        {
            var favourites = await _serviceUnitOfWork.FavouriteService.GetPaginatedFavouritesPerCustomer(request.pageNumber, request.itemsPerPage, request.CustomerId); 
            var favouriteDTOs = _mapper.Map<Paginated<FavouriteReadDTO>>(favourites);
            return Success(favouriteDTOs);
        }


    }
}
