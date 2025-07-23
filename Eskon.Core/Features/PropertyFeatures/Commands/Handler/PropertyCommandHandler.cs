using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Eskon.Core.Response;
using Eskon.Service.Interfaces;
using MediatR;
using Eskon.Core.Features.PropertyFeatures.Commands.Command;
using Eskon.Domian.DTOs.Property;
using System.ComponentModel.DataAnnotations;
using Eskon.Domian.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using System.Net;
using Azure.Core;
using Eskon.Service.UnitOfWork;
using Eskon.Domian.Entities.Identity;
using Microsoft.AspNetCore.Identity;


namespace Eskon.Core.Features.PropertyFeatures.Commands.Handler
{
    public class PropertyCommandHandler: ResponseHandler, IPropertyCommandHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion
        public PropertyCommandHandler(IMapper mapper,IServiceUnitOfWork serviceUnitOfWork,UserManager<User> userManager) { 
            _serviceUnitOfWork=serviceUnitOfWork;
            _mapper=mapper;
            _userManager=userManager;
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
            List<User> AdminUsers = _userManager.GetUsersInRoleAsync("Admin").Result.ToList();
            Random random = new Random();
            User Admin=AdminUsers[random.Next(AdminUsers.Count)];
            Property property = _mapper.Map<Property>(request.PropertyWriteDTO);
            property.OwnerId=request.ownerId;
            property.AssignedAdminId =Admin.Id;
            await _serviceUnitOfWork.PropertyService.AddPropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();
            PropertyDetailsDTO propertyDetails=_mapper.Map<PropertyDetailsDTO>(property);
            return Created<PropertyDetailsDTO>(propertyDetails);
        }

        public async Task<Response<string>> Handle(SetPropertyAsAcceptedCommand request, CancellationToken cancellationToken)
        {
            Guid AdminId=request.adminId ;
            Property property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            if(property == null)
            {
                return BadRequest<string>(string.Empty);
            }
            if (AdminId != property.AssignedAdminId)
            {
                return Unauthorized<string>();
            }
      
            await _serviceUnitOfWork.PropertyService.SetIsAcceptedPropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();
            return Success<string>("PropertyAccepted");
        }
        
        public async Task<Response<string>> Handle(SetPropertyAsRejectedCommand request, CancellationToken cancellationToken)
        {
            Guid AdminId=request.adminId;
            Property property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            if (property == null)
            {
                return NotFound<string>(string.Empty);
            }

            if (AdminId != property.AssignedAdminId)
            {
                return Unauthorized<string>();
            }
            await _serviceUnitOfWork.PropertyService.SetRejectionMessageAsync(property, request.rejectionMessage);
            await _serviceUnitOfWork.SaveChangesAsync();
            return Success<string>("PropertyAccepted");
        }
       
        
        public async Task<Response<string>> Handle(SetIsSuspendedPropertyCommand request, CancellationToken cancellationToken)
        {
            Guid OwnerId=request.ownerId;
            Property property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            if (property == null)
            {
                return NotFound<string>(string.Empty);
            }
            
            if (OwnerId != property.OwnerId)
            { 
                return Unauthorized<string>();
            }
            await _serviceUnitOfWork.PropertyService.SetPropertySuspensionStateAsync(property, request.value);
            await _serviceUnitOfWork.SaveChangesAsync();
            return Success<string>($"PropertySuspended={request.value}");
        }

        public async Task<Response<PropertyDetailsDTO>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationContext(request.propertyWriteDTO);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.propertyWriteDTO,validation,results,true);
            if (!isValid) { 
                var internalErrorMessages = results.Select(r=>r.ErrorMessage).ToList();
                return BadRequest<PropertyDetailsDTO>(internalErrorMessages);
            }
            Property property =await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            Guid OwnerId =request.ownerId;
            if (property == null)
            {
                return BadRequest<PropertyDetailsDTO>(string.Empty);

            }
            if (OwnerId != property.OwnerId)
            { 
                return Unauthorized<PropertyDetailsDTO>();
            }
            Property UpdatedProperty = _mapper.Map(request.propertyWriteDTO, property);
            await _serviceUnitOfWork.PropertyService.UpdatePropertyAsync(UpdatedProperty);
            await _serviceUnitOfWork.SaveChangesAsync();
            PropertyDetailsDTO propertyDetailsDTO = _mapper.Map<PropertyDetailsDTO>(UpdatedProperty);  
            return Success<PropertyDetailsDTO>(propertyDetailsDTO);
        }
        
        public async Task<Response<PropertyDetailsDTO>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {

            Guid OwnerId=request.ownerId;
            Property property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.id);
            PropertyDetailsDTO propertyDetailsDTO=_mapper.Map<PropertyDetailsDTO>(property);
            if (property == null)
            {
                return BadRequest<PropertyDetailsDTO>(string.Empty);
            }
            if (OwnerId != property.OwnerId)
            {
                return Unauthorized<PropertyDetailsDTO>();
            }
            await _serviceUnitOfWork.PropertyService.RemovePropertyAsync(property);
            await _serviceUnitOfWork.SaveChangesAsync();
            return Success<PropertyDetailsDTO>(propertyDetailsDTO);

        }
    }
}
