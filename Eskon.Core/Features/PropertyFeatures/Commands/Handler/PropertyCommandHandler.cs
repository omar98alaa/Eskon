using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Eskon.Core.Features.NotificationFeatures.Commands.Command;
using Eskon.Core.Features.PropertyFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Property;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Eskon.Core.Features.PropertyFeatures.Commands.Handler
{
    public class PropertyCommandHandler : ResponseHandler, IPropertyCommandHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        #endregion
        public PropertyCommandHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork, UserManager<User> userManager, IMediator mediator)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _mediator = mediator;
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

            //var propertyType = await _serviceUnitOfWork.PropertyTypeService.GetPropertyTypesByIdAsync(request.PropertyWriteDTO.PropertyTypeId);
            //if (propertyType == null)
            //{
            //    return NotFound<PropertyDetailsDTO>("Property type Not Found");
            //}

            var defaultPropertyType = new PropertyType
            {
                Id = Guid.NewGuid(),
                Name = "type3"+ DateTime.Now.Ticks
            };
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
            property.PropertyType = defaultPropertyType;

            await _serviceUnitOfWork.PropertyService.AddPropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();

            PropertyDetailsDTO propertyDetails = _mapper.Map<PropertyDetailsDTO>(property);

            
            //
            // Property Created notification
            //
            await _mediator.Send(new SendNotificationCommand(
                ReceiverId: property.AssignedAdminId,
                Content: $"A new property '{property.Title}' has been assigned to you.",
                NotificationTypeName: "Property Created",
                RedirectionId: property.Id,
                RedirectionName: property.Title
            ), cancellationToken);

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

            // Property Accepted notification
            await _mediator.Send(new SendNotificationCommand(
                ReceiverId: property.OwnerId,
                Content: $"Your property '{property.Title}' has been accepted.",
                NotificationTypeName: "Property Accepted",
                RedirectionId: property.Id,
                RedirectionName: property.Title
            ), cancellationToken);

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

            //
            // Property Rejected notification
            //
            await _mediator.Send(new SendNotificationCommand(
                ReceiverId: property.OwnerId,
                Content: $"Your property '{property.Title}' has been rejected.",
                NotificationTypeName: "Property Rejected",
                RedirectionId: property.Id,
                RedirectionName: property.Title
            ), cancellationToken);
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
