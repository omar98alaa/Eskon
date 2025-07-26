using AutoMapper;
using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;
using MediatR;


namespace Eskon.Core.Features.CountryFeatures.Commands.Handler
{
    public class CountryCommandHandler : ResponseHandler, IRequestHandler<AddCountryCommand, Response<AddCountryDTO>>,
        IRequestHandler<EditCountryCommand, Response<CountryUpdateDTO>>
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _unitofwork;
        #endregion

        public CountryCommandHandler(IMapper mapper, IServiceUnitOfWork unitofwork)
        {
            _mapper = mapper;
            _unitofwork = unitofwork;
        }

        public async Task<Response<AddCountryDTO>> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var country = new Country { Name = request.AddCountryDTO.Name };

            var addedCountry = await _unitofwork.CountryService.AddCountryAsync(country);

            var result = _mapper.Map<AddCountryDTO>(addedCountry);
            await _unitofwork.SaveChangesAsync();
            return Success(result, "Country added successfully");
        }

        public async Task<Response<CountryUpdateDTO>> Handle(EditCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _unitofwork.CountryService.GetCountryByNameAsync(request.CountryUpdateDTO.Name);
            if (country == null)
                return NotFound<CountryUpdateDTO>("Country is not found");

            country.Name = request.CountryUpdateDTO.Name;
            country.UpdatedAt = DateTime.UtcNow;

            await _unitofwork.CountryService.UpdateCountryAsync(country);
            await _unitofwork.SaveChangesAsync();

            var dto = _mapper.Map<CountryUpdateDTO>(country);
            return Success(dto, "City updated successfully");
        }

    }
}
