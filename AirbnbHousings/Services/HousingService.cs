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


        

        public async Task UploadImage()
        {

            // Instantiate a Minio client with your endpoint, access key, and secret key.
            using(var client = new MinioClient("localhost:9000", "ROOTUSER", "ROOTUSER").WithSSL())
            {
                await client.PutObjectAsync(new PutObjectArgs());
                await client.PutObjectAsync("mybucket", "myimage.jpg", "path/to/image.jpg", "image/jpeg");
            }
            

            // Upload an image file to the MinIO bucket.
           
        }
    }
}
