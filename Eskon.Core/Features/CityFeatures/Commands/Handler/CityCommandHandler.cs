using AutoMapper;
using Eskon.Core.Features.CityFeatures.Commands.Commands;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTOs;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;
using MediatR;

namespace Eskon.Core.Features.CityFeatures.Commands.Handler
{
    public class CityCommandHandler : ResponseHandler, IRequestHandler<AddCityCommand, Response<CityDTO>>,
         IRequestHandler<DeleteCityCommand, Response<CityDTO>>,
        IRequestHandler<EditCityCommand, Response<CityUpdateDTO>>
    {
        #region fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _unitofwork;
        #endregion

        #region constructor
        public CityCommandHandler(IMapper mapper, IServiceUnitOfWork unitofwork)
        {

            _mapper = mapper;
            _unitofwork = unitofwork;
        }

        #endregion
        public async Task<Response<CityDTO>> Handle(AddCityCommand request, CancellationToken cancellationToken)
        {
            var country = await _unitofwork.CountryService.GetCountryByNameAsync(request.CityDTO.CountryName);

            if (country == null)
                return NotFound<CityDTO>("Country not found");

            var existingCity = await _unitofwork.CityService.GetCityByNameAsync(request.CityDTO.Name);

            if (existingCity != null && existingCity.CountryId == country.Id)
            {
                var existCity = _mapper.Map<CityDTO>(existingCity);
                return BadRequest<CityDTO>("City already exists in this country");
            }

            var city = new City
            {
                Name = request.CityDTO.Name,
                CountryId = country.Id
            };

            var addedCity = await _unitofwork.CityService.AddCityAsync(city);
            await _unitofwork.SaveChangesAsync();

            var result = _mapper.Map<CityDTO>(addedCity);
            return Success(result, "City added successfully");
        }

        public async Task<Response<CityDTO>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _unitofwork.CityService.GetCityByIdAsync(request.id);
            if (city == null)
                return NotFound<CityDTO>("City not found");

            city.DeletedAt = DateTime.UtcNow;
            await _unitofwork.CityService.DeleteCityAsync(city);
            await _unitofwork.SaveChangesAsync();

            var result = _mapper.Map<CityDTO>(city);
            return Success(result, "City is Deleted Successfully");

        }

        public async Task<Response<CityUpdateDTO>> Handle(EditCityCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CityUpdateDTO;

            var city = await _unitofwork.CityService.GetCityByNameAsync(request.name);
            if (city == null)
                return NotFound<CityUpdateDTO>("City not found");

            var country = await _unitofwork.CountryService.GetCountryByNameAsync(dto.CountryName);
            if (country == null)
                return NotFound<CityUpdateDTO>("Country not found");

            var existingCity = await _unitofwork.CityService.GetCityByNameAsync(dto.Name);

            if (existingCity != null && existingCity.Id != city.Id && existingCity.CountryId == country.Id)
                return BadRequest<CityUpdateDTO>(null, "This city already exists in this country");

            city.Name = dto.Name;
            city.CountryId = country.Id;
            city.UpdatedAt = DateTime.UtcNow;

            await _unitofwork.CityService.UpdateCityAsync(city);
            await _unitofwork.SaveChangesAsync();

            return Success(dto, "City updated");



        }
    }
}