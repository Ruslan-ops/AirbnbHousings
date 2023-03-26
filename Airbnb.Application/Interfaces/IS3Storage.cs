using Airbnb.Application.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Interfaces
{
    public interface IS3Storage
    {
        Task<UploadResult> UploadPhotoAsync(IFormFile photo, S3PhotoDir photoDir);
        Task<UploadResult> UploadPhotoAsync(IFormFile photo, string path);
        Task DeletePhotoAsync(string objectName);

    }
}
