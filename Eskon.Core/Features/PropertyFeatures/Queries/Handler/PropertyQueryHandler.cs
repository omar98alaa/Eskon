using AutoMapper;
using Eskon.Core.Features.PropertyFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Property;
using Eskon.Service.UnitOfWork;

namespace Eskon.Core.Features.PropertyFeatures.Queries.Handler
{
    public class PropertyQueryHandler : ResponseHandler, IPropertyQueryHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public PropertyQueryHandler(IServiceUnitOfWork serviceUnitOfWork, IMapper mapper)
        {
            this._serviceUnitOfWork = serviceUnitOfWork;
            this._mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<PropertyDetailsDTO>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.propertyId);

            if (property == null)
            {
                return NotFound<PropertyDetailsDTO>(message: $"Property with id: {request.propertyId} was not found");
            }

            var propertyDetailsDTO = _mapper.Map<PropertyDetailsDTO>(property);
            return Success(propertyDetailsDTO);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetAssignedPendingPropertiesQuery request, CancellationToken cancellationToken)
        {
            var assignedPendingProperties = await _serviceUnitOfWork.PropertyService.GetAssignedPendingPropertiesAsync(request.adminId, request.pageNum, request.itemsPerPage);

            var assignedPendingPropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(assignedPendingProperties);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(assignedPendingPropertiesDTO, 1, 10, 100);    // Needs fix

            return Success(paginatedResponse);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetActivePropertiesPerOwnerQuery request, CancellationToken cancellationToken)
        {
            var activeProperties = await _serviceUnitOfWork.PropertyService.GetActivePropertiesPerOwnerAsync(request.ownerId, request.pageNum, request.itemsPerPage);

            var activePropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(activeProperties);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(activePropertiesDTO, 1, 10, 100);    // Needs fix

            return Success(paginatedResponse);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetPendingPropertiesPerOwnerQuery request, CancellationToken cancellationToken)
        {
            var activeProperties = await _serviceUnitOfWork.PropertyService.GetPendingPropertiesPerOwnerAsync(request.ownerId, request.pageNum, request.itemsPerPage);

            var activePropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(activeProperties);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(activePropertiesDTO, 1, 10, 100);    // Needs fix

            return Success(paginatedResponse);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetSuspendedPropertiesPerOwnerQuery request, CancellationToken cancellationToken)
        {
            var activeProperties = await _serviceUnitOfWork.PropertyService.GetSuspendedPropertiesPerOwnerAsync(request.ownerId, request.pageNum, request.itemsPerPage);

            var activePropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(activeProperties);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(activePropertiesDTO, 1, 10, 100);    // Needs fix

            return Success(paginatedResponse);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetRejectedPropertiesPerOwnerQuery request, CancellationToken cancellationToken)
        {
            var activeProperties = await _serviceUnitOfWork.PropertyService.GetRejectedPropertiesPerOwnerAsync(request.ownerId, request.pageNum, request.itemsPerPage);

            var activePropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(activeProperties);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(activePropertiesDTO, 1, 10, 100);    // Needs fix

            return Success(paginatedResponse);
        }


        #endregion
    }
}
