using Microsoft.AspNetCore.Mvc;
using Minio;
using System.Security.Cryptography.X509Certificates;

namespace AirbnbHousings.Services
{
    public class MinioService
    {
        private readonly MinioClient _minioClient;
        private const string _imagesBucket = "images";

        public MinioService(MinioClient minioClient)
        {
            _minioClient = minioClient;
        }
        

        public async Task<UploadResult> UploadImageAsync(IFormFile image)
        {

            var stream = image.OpenReadStream();
            var contentType = image.ContentType;
            var contentDisposition = image.ContentDisposition;

            // Upload the image to the MinIO bucket.
            var bucketName = _imagesBucket;
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

        public async Task DeleteImageAsync(string objectName)
        {
            var args = new RemoveObjectArgs().WithObject(objectName).WithBucket(_imagesBucket);
            await _minioClient.RemoveObjectAsync(args);
        }
    }

    public struct UploadResult
    {
        public string Url;
        public string ObjectName;
        public string Bucket;
    }

}
