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
        private readonly MinioClient _minioClient;

        public HousingController(MinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            Console.WriteLine(_minioClient.ToString());
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
            var cli = JsonConvert.SerializeObject(_minioClient);
            await Console.Out.WriteLineAsync((_minioClient is null).ToString());
            await Console.Out.WriteLineAsync(cli);

            if (image == null)
            {
                return BadRequest("No image file found");
            }

            // Get the image stream and metadata.
            var stream = image.OpenReadStream();
            var contentType = image.ContentType;
            var contentDisposition = image.ContentDisposition;
            
            // Upload the image to the MinIO bucket.
            var bucketName = "images";
            var objectName = $@"/housing/{Guid.NewGuid()}.jpg";
            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithContentType(contentType)
                .WithObjectSize(image.Length)
                .WithStreamData(stream);
            await _minioClient.PutObjectAsync(args);

            var urlArgs = new PresignedGetObjectArgs().WithObject(objectName).WithBucket(bucketName).WithExpiry(60 * 60 * 24 * 7);
            var url = await _minioClient.PresignedGetObjectAsync(urlArgs);
            await Console.Out.WriteLineAsync(url);
            return Ok();
            //return new Ok("Image uploaded successfully");
        }
    }
}
