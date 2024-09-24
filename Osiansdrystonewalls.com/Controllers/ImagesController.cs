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
        private readonly iImageRepository imageRepository;

        public ImagesController(iImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadASync(IFormFile file)
        {
            var imageURL = await imageRepository.UploadASync(file);

            if (imageURL == null)
            {
                return Problem("This is not correct", null, (int)HttpStatusCode.InternalServerError);
            }
            else
                return
                    new JsonResult(new { link = imageURL });
        }
    }
}
