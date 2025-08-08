using AutoMapper;
using Eskon.Core.Features.PropertyTypeFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.PropertyType;
using Eskon.Service.UnitOfWork;


namespace Eskon.Core.Features.PropertyTypeFeatures.Queries.Handler
{
    public class PropertyTypeQueryHandler : ResponseHandler, IPropertyTypeQueryHandler
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _unitOfWork;

        #endregion

        public PropertyTypeQueryHandler(IMapper mapper, IServiceUnitOfWork unitOfWork)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<List<PropertyTypeDTO>>> Handle(GetAllPropertyTypesQuery request, CancellationToken cancellationToken)
        {
            var propertyTypes = await _unitOfWork.PropertyTypeService.GetPropertyTypesAsync();
            var propertyTypeDTOs = propertyTypes.Select(pt => new PropertyTypeDTO { Name = pt.Name }).ToList();

            return Success(propertyTypeDTOs);
        }
    }
}
