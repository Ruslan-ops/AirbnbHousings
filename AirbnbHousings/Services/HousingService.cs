using Microsoft.AspNetCore.Mvc;
using Minio;
using System.Security.Cryptography.X509Certificates;

namespace AirbnbHousings.Services
{
    public class HousingService
    {
        private readonly MinioClient _minioClient;

        public HousingService(MinioClient minioClient)
        {
            _minioClient = minioClient;
        }
        

        public async Task<UploadResult> UploadImage(IFormFile image)
        {

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
            return new UploadResult { Bucket = bucketName, ObjectName = objectName, Url = url };
        }
    }

    public struct UploadResult
    {
        public string Url;
        public string ObjectName;
        public string Bucket;
    }

}
