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

        public static string HousingNotHasPhoto(int photoId) => $"The housing does't have the photo with id {photoId}";
    }
}
