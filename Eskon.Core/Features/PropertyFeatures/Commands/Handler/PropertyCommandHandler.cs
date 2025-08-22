using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Eskon.Core.Features.PropertyFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Property;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Service.Interfaces;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;


namespace Eskon.Core.Features.PropertyFeatures.Commands.Handler
{
    public class PropertyCommandHandler : ResponseHandler, IPropertyCommandHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion
        public PropertyCommandHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork, UserManager<User> userManager, IEmailService emailService)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Response<PropertyDetailsDTO>> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
        {

            var validationContext = new ValidationContext(request.PropertyWriteDTO);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.PropertyWriteDTO, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<PropertyDetailsDTO?>(internalErrorMessages);
            }

            // Check city exists
            var city = await _serviceUnitOfWork.CityService.GetCityByIdAsync(request.PropertyWriteDTO.CityId);
            if (city == null) 
            {
                return NotFound<PropertyDetailsDTO>("City Not Found");
            }

            //Image Check
            var imagesUrls = request.PropertyWriteDTO.ImageUrls;
            var images = await _serviceUnitOfWork.ImageService.GetImagesByNameAsync(imagesUrls);
            if(images.Count != imagesUrls.Count)
            {
                return NotFound<PropertyDetailsDTO>("One or more images were not found");
            }

            var propertyType = await _serviceUnitOfWork.PropertyTypeService.GetPropertyTypesByIdAsync(request.PropertyWriteDTO.PropertyTypeId);
            if (propertyType == null)
            {
                return NotFound<PropertyDetailsDTO>("Property type Not Found");
            }


            // Assign an admin randomly
            List<User> AdminUsers = (await _userManager.GetUsersInRoleAsync("Admin")).ToList();
            Random random = new Random();
            var idx = random.Next(AdminUsers.Count);
            User Admin = AdminUsers[idx];

            // Populate new property object
            Property property = _mapper.Map<Property>(request.PropertyWriteDTO);
            property.OwnerId = request.ownerId;
            property.Owner = await _userManager.FindByIdAsync(request.ownerId.ToString());
            property.AssignedAdminId = Admin.Id;
            property.Images = images;
            property.PropertyType = propertyType;

            await _serviceUnitOfWork.PropertyService.AddPropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();

            PropertyDetailsDTO propertyDetails = _mapper.Map<PropertyDetailsDTO>(property);
            
            return Created(propertyDetails);
        }

        public async Task<Response<string>> Handle(SetPropertyAsAcceptedCommand request, CancellationToken cancellationToken)
        {
            // Check Property exists
            Property property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            if (property == null)
            {
                return NotFound<string>("Property Not Found");
            }
            // Check Property Admin
            Guid AdminId = request.adminId;
            if (AdminId != property.AssignedAdminId)
            {
                return Forbidden<string>();
            }
            // Set Property Accepted and SaveChanges
            await _serviceUnitOfWork.PropertyService.SetIsAcceptedPropertyAsync(property);

            await _serviceUnitOfWork.SaveChangesAsync();

            // Send Email to Owner
            _emailService.SendEmailAsync(
                property.Owner.Email,
                "ESKON: Property Status Update",
                $"Hello Mr.{property.Owner.LastName}\n\n" +
                $"We would like to inform you that your property: {property.Title} has been accepted and is now eligible to revceive booking requests."
            );

            return Success<string>($"Property With Id {property.Id} Accepted", $"{property.Id} Accepted");
        }

        public async Task<Response<string>> Handle(SetPropertyAsRejectedCommand request, CancellationToken cancellationToken)
        {
            // Check Property exists
            Property property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            if (property == null)
            {
                return NotFound<string>("Property Not Found");
            }
            // Check Property Admin
            Guid AdminId = request.adminId;
            if (AdminId != property.AssignedAdminId)
            {
                return Forbidden<string>();
            }
            // Set RejectionMessage and save chanes
            await _serviceUnitOfWork.PropertyService.SetRejectionMessageAsync(property, request.rejectionMessage);
            await _serviceUnitOfWork.SaveChangesAsync();

            // Send Email to Owner
            _emailService.SendEmailAsync(
                property.Owner.Email,
                "ESKON: Property Status Update",
                $"Hello Mr.{property.Owner.LastName}\n\n" +
                $"We regret to inform you that your property: {property.Title} has been rejected.\n" +
                $"Rejection reason:\n {property.RejectionMessage}\n\n" +
                $"You can either make any required changes and re-submit your property or remove it."
            );

            return Success<string>(string.Empty, request.rejectionMessage);
        }


        public async Task<Response<string>> Handle(SetIsSuspendedPropertyCommand request, CancellationToken cancellationToken)
        {
            Guid OwnerId = request.ownerId;
            Property property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            // Check Property exists
            if (property == null)
            {
                return NotFound<string>("Property Not Found");
            }
            // Check Property Owner
            if (OwnerId != property.OwnerId)
            {
                return Forbidden<string>();
            }
            // Set Suspension State
            await _serviceUnitOfWork.PropertyService.SetPropertySuspensionStateAsync(property, request.value);
            await _serviceUnitOfWork.SaveChangesAsync();
            return Success<string>(string.Empty, $"Property With Id {property.Id} Suspension State={request.value}");
        }

        public async Task<Response<PropertyDetailsDTO>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationContext(request.propertyWriteDTO);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.propertyWriteDTO, validation, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<PropertyDetailsDTO>(internalErrorMessages);
            }
            // Check Property exists
            Property property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            if (property == null)
            {
                return NotFound<PropertyDetailsDTO>("Property Not Found");

            }
            // Check Property Owner
            Guid OwnerId = request.ownerId;
            if (OwnerId != property.OwnerId)
            {
                return Forbidden<PropertyDetailsDTO>();
            }
            // Check city exists
            var city = await _serviceUnitOfWork.CityService.GetCityByIdAsync(request.propertyWriteDTO.CityId);
            if (city == null)
            {
                return NotFound<PropertyDetailsDTO>("City Not Found");
            }
            //Image Check
            var imagesUrls = request.propertyWriteDTO.ImageUrls;
            var images = await _serviceUnitOfWork.ImageService.GetImagesByNameAsync(imagesUrls);
            if (images.Count != imagesUrls.Count)
            {
                return NotFound<PropertyDetailsDTO>("One or more images were not found");
            }
           
            property = _mapper.Map(request.propertyWriteDTO, property);
            //Set Property As Pending
            await _serviceUnitOfWork.PropertyService.SetPropertyAsPendingAsync(property);
            //Add Images
            property.Images= images;
            //Update Property
            await _serviceUnitOfWork.PropertyService.UpdatePropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();
            PropertyDetailsDTO propertyDetailsDTO = _mapper.Map<PropertyDetailsDTO>(property);
            return Success(propertyDetailsDTO);
        }

        public async Task<Response<PropertyDetailsDTO>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {

            Guid OwnerId = request.ownerId;
            Property property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            PropertyDetailsDTO propertyDetailsDTO = _mapper.Map<PropertyDetailsDTO>(property);
            // Check Property exists
            if (property == null)
            {
                return NotFound<PropertyDetailsDTO>("Property Not Found");
            }
            // Check Property Owner
            if (OwnerId != property.OwnerId)
            {
                return Forbidden<PropertyDetailsDTO>();
            }
            // Soft Delete and SaveChanges
            await _serviceUnitOfWork.PropertyService.RemovePropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();
            return Success<PropertyDetailsDTO>(propertyDetailsDTO);

        }
    }
}
