using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Consts
{
    internal class ErrorMessages
    {
        public const string UserNotOwnHousing = "The user doesn't own the housing";
        public const string InvalidAuthentication = "Invalid authentication, please login again";
        public const string EmailIsUsed = "User with the same email already exists";

        public static string HousingNotHasPhoto(int photoId) => $"The housing does't have the photo with id {photoId}";
        public static string UserNotHasPhoto(int photoId) => $"The user does't have the photo with id {photoId}";


    }
}
