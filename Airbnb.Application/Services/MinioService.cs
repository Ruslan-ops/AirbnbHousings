﻿using Airbnb.Application.Common.Options;
using Airbnb.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Airbnb.Application.Services
{
    public class MinioService : IS3Storage
    {
        private readonly MinioClient _minioClient;
        private readonly MinioOptions _options;
        //private const string _imagesBucket = "images";

        public MinioService(MinioClient minioClient, IOptions<MinioOptions> options)
        {
            _minioClient = minioClient;
            _options = options.Value;
        }


        public async Task<UploadResult> UploadPhotoAsync(IFormFile photo, S3PhotoDir photoDir)
        {
            var dir = System.Enum.GetName(photoDir);
            var path = $@"{dir}/{Guid.NewGuid()}.jpg" ;
            return await UploadPhotoAsync(photo, path);
        }

        public async Task<UploadResult> UploadPhotoAsync(IFormFile photo, string path)
        {
            var stream = photo.OpenReadStream();
            var contentType = photo.ContentType;
            var contentDisposition = photo.ContentDisposition;

            // Upload the image to the MinIO bucket.
            var bucketName = _options.BucketName;
            var objectName = path; // $@"/housing/{Guid.NewGuid()}.jpg";
            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithContentType(contentType)
                .WithObjectSize(photo.Length)
                .WithStreamData(stream);

            await _minioClient.PutObjectAsync(args);
            var url = $@"http://{_options.Endpoint}/{bucketName}/{objectName}";
            //await Console.Out.WriteLineAsync($"%%%% Url: {url}, ObjectName: {objectName}, Bucket: {bucketName}");
            var result = new UploadResult { Bucket = bucketName, ObjectName = objectName, Url = url };
            await Console.Out.WriteLineAsync($"%%%% Upload internal result: {JsonSerializer.Serialize(result)}");
            return result;
        }

        public async Task DeletePhotoAsync(string objectName)
        {
            var args = new RemoveObjectArgs().WithObject(objectName).WithBucket(_options.BucketName);
            await _minioClient.RemoveObjectAsync(args);
        }

    }

    public struct UploadResult
    {
        public string Url { get; set; }
        public string ObjectName { get; set; }
        public string Bucket { get; set; }
    }

    public enum S3PhotoDir
    {
        Housing = 1,
        User,
    }

}
