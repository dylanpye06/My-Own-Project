using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Osiansdrystonewalls.com.Repositories;
using System.Net;

namespace Osiansdrystonewalls.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository) 
        { 
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadASync(IFormFile file)
        {
            var imageURL = await imageRepository.UploadASync(file);

            if(imageURL == null)
            {
                return Problem("Image uplaod was not successful!", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new {link = imageURL});
        }
    }
}
