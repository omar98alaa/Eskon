using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.ImageFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : BaseController
    {
        #region Fields
        #endregion

        #region Constructors
        public ImageController()
        {
        }
        #endregion

        #region Actions

        #region Upload image
        /// <summary>
        /// Uploads an image file to the server and stores its record in the database.
        /// </summary>
        /// <remarks>
        /// **Description:**
        /// This endpoint allows an authenticated user to upload an image file.  
        /// The image is validated for format, saved to the server's storage, and a record of the image is inserted into the database.  
        /// 
        /// **Validations:**
        /// - The request must contain a file in the `image` parameter.
        /// - Only `.jpg`, `.jpeg`, and `.png` file extensions are accepted.
        /// - The file must not be empty.
        /// 
        /// **Workflow:**
        /// 1. Validates that a file is provided and is non-empty.
        /// 2. Checks that the file extension is one of the allowed formats (`.jpg`, `.jpeg`, `.png`).
        /// 3. Saves the file to a storage folder using the `FileService`.
        /// 4. Adds an entry to the `ImageService` with the file's URL.
        /// 5. Persists changes to the database.
        /// 6. Returns the file name in the response.
        /// 
        /// **Example Request:**
        /// ```http
        /// POST /UploadImage
        /// Authorization: Bearer {jwt_token}
        /// Content-Type: multipart/form-data
        /// 
        /// Form-data:
        /// image: example.jpg
        /// ```
        /// 
        /// **Example Successful Response:**
        /// ```json
        /// {
        ///   "succeeded": true,
        ///   "message": null,
        ///   "errors": null,
        ///   "data": {
        ///     "imageName": "uploads/example_20250813.jpg"
        ///   }
        /// }
        /// ```
        /// 
        /// **Possible Error Responses:**
        /// - `400 Bad Request` — No file uploaded or invalid file format.
        /// - `401 Unauthorized` — Authentication required.
        /// - `500 Internal Server Error` — Failed to save the image.
        /// </remarks>
        /// <param name="image">The image file to be uploaded.</param>
        /// <returns>
        /// Returns a <see cref="Response{ImageReadDTO}"/> containing the uploaded image's file name.
        /// </returns>
        /// <response code="201">Image uploaded successfully.</response>
        /// <response code="400">Invalid request — file is missing or has an unsupported format.</response>
        /// <response code="401">Unauthorized — user must be logged in.</response>
        /// <response code="500">Internal server error while saving the file or writing to the database.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Response<ImageReadDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<ImageReadDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ImageReadDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ImageReadDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            var uploadedImageDTO = await Mediator.Send(new SaveImageCommand(image));
            return NewResult(uploadedImageDTO);
        }
        #endregion

        #region Delete Image
        /// <summary>
        /// Deletes an image from both the database and the server's file storage.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint removes an image by its file name.  
        /// It first checks if the image exists in the database, then deletes its record, and finally removes the physical file from storage.
        /// 
        /// **Workflow:**  
        /// 1. Retrieves the image record from the database using the provided `imageName`.
        /// 2. If the image record is not found, returns `404 Not Found`.
        /// 3. Deletes the image record from the database.
        /// 4. Attempts to remove the corresponding file from the storage folder.
        /// 5. If the file is not found in storage, returns `404 Not Found`.
        /// 6. Commits database changes.
        /// 7. Returns a success message confirming deletion.
        /// 
        /// **Example Request:**
        /// ```http
        /// DELETE /DeleteImage/sample.jpg
        /// Authorization: Bearer {jwt_token}
        /// ```
        /// 
        /// **Example Successful Response:**
        /// ```json
        /// {
        ///   "succeeded": true,
        ///   "message": null,
        ///   "errors": null,
        ///   "data": "Image sample.jpg deleted successfully"
        /// }
        /// ```
        /// 
        /// **Possible Error Responses:**
        /// - `404 Not Found` — Image record or file not found.
        /// - `401 Unauthorized` — User is not authenticated.
        /// 
        /// **Notes:**  
        /// - The `imageName` should include the file extension (e.g., `example.jpg`).
        /// - Only authenticated users can delete images.
        /// </remarks>
        /// <param name="imageName">The name of the image to delete, including its file extension.</param>
        /// <returns>
        /// Returns a <see cref="Response{string}"/> with a confirmation message if deletion is successful.
        /// </returns>
        /// <response code="200">Image deleted successfully.</response>
        /// <response code="401">Unauthorized — authentication required.</response>
        /// <response code="404">Image not found in the database or file storage.</response>
        [Authorize]
        [HttpDelete("{imageName}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteImage([FromRoute] string imageName)
        {
            var result = await Mediator.Send(new DeleteImageCommand(imageName));
            return NewResult(result);
        } 
        #endregion
        #endregion
    }
}
