using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.ImageFeatures.Commands.Command;
using Eskon.Core.Features.ImageFeatures.Commands.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : BaseController
    {
        private readonly IMapper _mapper;

        public ImagesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("upload/image")]
        public async Task<IActionResult> UploadImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return NotFound("No file uploaded.");

            var result = await Mediator.Send(new UploadImageCommand(file));

            return NewResult(result);
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> DeleteImage(string fileName)
        {
            var command = new DeleteImageCommand(fileName);
            var result = await Mediator.Send(command);

            return NewResult(result);
        }
    }

}

