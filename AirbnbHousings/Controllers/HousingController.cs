using AirbnbHousings.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Newtonsoft.Json;

namespace AirbnbHousings.Controllers
{
    [ApiController]
    [Route("housings")]
    public class HousingController : Controller
    {
        private readonly HousingService _housingService;

        public HousingController(HousingService housingService)
        {
            _housingService = housingService;
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            return Ok("successsss");
        }

        [HttpGet("list")]
        public List<HousingShort> GetList()
        {
            return null;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {

            if (image == null)
            {
                return BadRequest("No image file found");
            }

            // Get the image stream and metadata.
            var uploadResult = await _housingService.UploadImage(image);
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(uploadResult));
            return Ok();
            //return new Ok("Image uploaded successfully");
        }
    }
}
