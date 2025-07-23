using AutoMapper;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.ImageDTO;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.ImageFeatures.Queries.Models
{
    //public class GetAllImagesByPropertyIdHandler : ResponseHandler,IRequestHandler<GetAllImagesByPropertyIdQuery, Response<List<ImageReadDTO>>>
    //{
    //    private readonly IImageService _imageService;
    //    private readonly IMapper _mapper;

    //    public GetAllImagesByPropertyIdHandler(IImageService imageService, IMapper mapper)
    //    {
    //        _imageService = imageService;
    //        _mapper = mapper;
    //    }
    //    public async Task<Response<List<ImageReadDTO>>> Handle(GetAllImagesByPropertyIdQuery request, CancellationToken cancellationToken)
    //    {
    //        var images = await _imageService.GetAllImagesByPropertyIdAsync(request.PropertyId);
    //        var dtoList = _mapper.Map<List<ImageReadDTO>>(images);
    //        return Success(dtoList);
    //    }
    //}
}
