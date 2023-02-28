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
        private readonly MinioService _minioService;
        private readonly PostgresService _postgresService;

        public HousingController(MinioService minioService, PostgresService postgresService)
        {
            _minioService = minioService;
            _postgresService = postgresService;
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

        //[HttpPost("uploadN")]
        //public async Task<IActionResult> UploadImage(IFormCollection collection) 
        //{
        //    await Console.Out.WriteLineAsync(collection["housingId"]);
        //    return Ok();
        //}

        [HttpPost("image/new")]
        public async Task<IActionResult> UploadImage(IFormCollection collection)
        {
            IFormFile image = collection.Files.First();
            var housingIdStr = collection["housingId"];
            int housingId;
            try
            {
                housingId = Convert.ToInt32(housingIdStr);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (image == null)
            {
                return BadRequest("No image file found");
            }


            // Get the image stream and metadata.
            var uploadResult = await _minioService.UploadImageAsync(image);
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(uploadResult));
            await _postgresService.AddHousingImageAsync(housingId, uploadResult.ObjectName, uploadResult.Url);
            return Ok();
            //return new Ok("Image uploaded successfully");
        }

        [HttpDelete("image/delete")]
        public async Task<IActionResult> DeleteImage(string imageId)
        {
            var imageIdInt = Convert.ToInt32(imageId);
            var deletedImage = await _postgresService.DeleteHousingImageAsync(imageIdInt);
            await _minioService.DeleteImageAsync(deletedImage.Name);
            return Ok();
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddHousingAsync([FromBody]HousingCreateDto model)
        {
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(model));
            var h = await _postgresService.AddHousingAsync(model);
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(h));
            return Ok();
        }

        private IActionResult ValidateInt(string numberStr, out int number)
        {
            number = 0;
            try
            {
                number =  Convert.ToInt32(numberStr);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return null;
        }
    }

    public class HousingCreateDto
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public int NightPrice { get; set; }
        public string Address { get; set; }
    }

}
