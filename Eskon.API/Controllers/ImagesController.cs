using Eskon.API.Base;
using Eskon.Core.Features.ImageFeatures.Commands.Command;
using Eskon.Core.Features.ImageFeatures.Commands.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : BaseController
    {
            private readonly IMediator _mediator;

     public ImagesController(IMediator mediator)
     {
         _mediator = mediator;
      }

        [HttpPost("upload/image")]
        public async Task<IActionResult> UploadImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return NotFound("No file uploaded.");

            var result = await _mediator.Send(new UploadImageCommand(file));

            return NewResult(result); 
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> DeleteImage(string fileName)
        {
            var command = new DeleteImageCommand(fileName);
            var result = await _mediator.Send(command);

            return NewResult(result); 
        }
    }

    }

