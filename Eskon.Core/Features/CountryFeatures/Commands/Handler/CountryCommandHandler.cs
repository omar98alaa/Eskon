using AutoMapper;
using Eskon.Core.Features.CityFeatures.Commands.Commands;
using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Domian.DTOs.Country;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Domian.Models;
using Eskon.Service.Interfaces;
using Eskon.Service.Services;
using Eskon.Service.UnitOfWork;
using MediatR;


namespace Eskon.Core.Features.CountryFeatures.Commands.Handler
{
    public class CountryCommandHandler : ResponseHandler, IRequestHandler<AddCountryCommand, Response<AddCountryDTO>>,
        IRequestHandler<EditCountryCommand, Response<CountryUpdateDTO>>
    {

        #region Fields
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _unitofwork;
        #endregion

        public CountryCommandHandler(ICountryService countryService, IMapper mapper, IServiceUnitOfWork unitofwork)
        {
            _countryService = countryService;
            _mapper = mapper;
            _unitofwork = unitofwork;
        }

        public async Task<Response<AddCountryDTO>> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var country = new Country { Name = request.Name };

            var addedCountry = await _countryService.AddCountryAsync(country);

            var result = _mapper.Map<AddCountryDTO>(addedCountry);
            await _unitofwork.SaveChangesAsync();
            return Success(result, "Country added successfully");
        }

        public async Task<Response<CountryUpdateDTO>> Handle(EditCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _countryService.GetCountryByNameAsync(request.name);
            if (country == null)
                return NotFound<CountryUpdateDTO>("Country is not found");

            country.Name = request.name;
            country.UpdatedAt = DateTime.UtcNow;

            await _countryService.UpdateCountryAsync(country);
            await _unitofwork.SaveChangesAsync();

            var dto = _mapper.Map<CountryUpdateDTO>(country);
            return Success(dto, "City updated successfully");
        }

    }
}
