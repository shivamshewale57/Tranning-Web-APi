using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web_Api.Models.Domain;
using Web_Api.Models.DTO;
using Web_Api.Repositories;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        // acess injected repository through the constructor 
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]

        public async Task<IActionResult> Upload([FromForm]ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                // convert DTO to Domain MOdel 
                 var imageDomainMOdel = new Image
                 {  Id =request.Id,
                     File=request.File,
                     FileExtention=Path.GetExtension(request.File.FileName),
                     FileSizeInBytes=request.File.Length,
                     FileName=request.File.Name,
                     FileDescription= request.FileDescription
                 };

                // User repository to upload image

                await imageRepository.Upload(imageDomainMOdel);
                return Ok(imageDomainMOdel);
            }

            return BadRequest(ModelState);
        }

        //validate request is valid or not
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtenstions = new string[] { ".jpg", "jpeg", ".png" };
            if(allowedExtenstions.Contains(Path.GetExtension(request.File.FileName))==false)
            {
                ModelState.AddModelError("file", "Unsupported File extenstion");
            }

            //check size of the file 
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size More Than 10Mb,Please Upload Smaller Size File");
            }
        }
    }
}
