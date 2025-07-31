using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.ImageFeatures.Commands.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ImageController(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region Actions

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            var uploadedImageDTO = await Mediator.Send(new SaveImageCommand(image));
            return NewResult(uploadedImageDTO);
        }

        [Authorize]
        [HttpDelete("{imageName}")]
        public async Task<IActionResult> DeleteImage([FromRoute] string imageName)
        {
            var result = await Mediator.Send(new DeleteImageCommand(imageName));
            return NewResult(result);
        }
        #endregion
    }
}
