using AutoMapper;
using Eskon.Core.Features.PropertyFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
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

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetFilteredActivePropertiesPaginated request, CancellationToken cancellationToken)
        {
            var paginatedFilteredProperties = await _serviceUnitOfWork.PropertyService.GetFilteredActivePropertiesPaginatedAsync(request.pageNum, request.itemsPerPage, request.propertySearchFilters);

            var paginatedFilteredPropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(paginatedFilteredProperties.Data);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(
                paginatedFilteredPropertiesDTO,
                paginatedFilteredProperties.PageNumber,
                paginatedFilteredProperties.PageSize,
                paginatedFilteredProperties.TotalRecords
                );

            return Success(paginatedResponse);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetAssignedPendingPropertiesQuery request, CancellationToken cancellationToken)
        {
            var paginatedAssignedPendingProperties = await _serviceUnitOfWork.PropertyService.GetAssignedPendingPropertiesAsync(request.adminId, request.pageNum, request.itemsPerPage);

            var assignedPendingPropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(paginatedAssignedPendingProperties.Data);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(
                assignedPendingPropertiesDTO,
                paginatedAssignedPendingProperties.PageNumber,
                paginatedAssignedPendingProperties.PageSize,
                paginatedAssignedPendingProperties.TotalRecords
                );

            return Success(paginatedResponse);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetActivePropertiesPerOwnerQuery request, CancellationToken cancellationToken)
        {
            var paginatedActiveProperties = await _serviceUnitOfWork.PropertyService.GetActivePropertiesPerOwnerAsync(request.ownerId, request.pageNum, request.itemsPerPage);

            var activePropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(paginatedActiveProperties.Data);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(
                activePropertiesDTO,
                paginatedActiveProperties.PageNumber,
                paginatedActiveProperties.PageSize,
                paginatedActiveProperties.TotalRecords
                );

            return Success(paginatedResponse);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetPendingPropertiesPerOwnerQuery request, CancellationToken cancellationToken)
        {
            var paginatedPendingProperties = await _serviceUnitOfWork.PropertyService.GetPendingPropertiesPerOwnerAsync(request.ownerId, request.pageNum, request.itemsPerPage);

            var pendingPropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(paginatedPendingProperties.Data);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(
                pendingPropertiesDTO,
                paginatedPendingProperties.PageNumber,
                paginatedPendingProperties.PageSize,
                paginatedPendingProperties.TotalRecords
                );

            return Success(paginatedResponse);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetSuspendedPropertiesPerOwnerQuery request, CancellationToken cancellationToken)
        {
            var paginatedSuspendedProperties = await _serviceUnitOfWork.PropertyService.GetSuspendedPropertiesPerOwnerAsync(request.ownerId, request.pageNum, request.itemsPerPage);

            var suspendedPropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(paginatedSuspendedProperties.Data);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(
                suspendedPropertiesDTO,
                paginatedSuspendedProperties.PageNumber,
                paginatedSuspendedProperties.PageSize,
                paginatedSuspendedProperties.TotalRecords
                );

            return Success(paginatedResponse);
        }

        public async Task<Response<Paginated<PropertySummaryDTO>>> Handle(GetRejectedPropertiesPerOwnerQuery request, CancellationToken cancellationToken)
        {
            var paginatedRejectedProperties = await _serviceUnitOfWork.PropertyService.GetRejectedPropertiesPerOwnerAsync(request.ownerId, request.pageNum, request.itemsPerPage);

            var rejectedPropertiesDTO = _mapper.Map<List<PropertySummaryDTO>>(paginatedRejectedProperties.Data);

            var paginatedResponse = new Paginated<PropertySummaryDTO>(
                rejectedPropertiesDTO,
                paginatedRejectedProperties.PageNumber,
                paginatedRejectedProperties.PageSize,
                paginatedRejectedProperties.TotalRecords
                );

            return Success(paginatedResponse);
        }


        #endregion
    }
}
