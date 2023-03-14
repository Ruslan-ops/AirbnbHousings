using Airbnb.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Services
{
    public class DatabaseService
    {
        public AirbnbContext _airbnbContext;
        public DatabaseService(AirbnbContext airbnbContext)
        {
            _airbnbContext = airbnbContext;
        }


        private async Task<int> GetLastOrderNumberForHousingPhotoAsync(int housingId)
        {
            bool hasPhotos = await _airbnbContext.HousingPhotos.AsNoTracking().AnyAsync(hp => hp.HousingId == housingId);
            if (hasPhotos)
            {
                var maxOrderNumber = await _airbnbContext.HousingPhotos.AsNoTracking().Where(hp => hp.HousingId == housingId).MaxAsync(hp => hp.OrderNumber);
                return maxOrderNumber + 1;
            }
            return 1;
        }

        private async Task<int> GetLastOrderNumberForUserPhotoAsync(int userId)
        {
            bool hasPhotos = await _airbnbContext.UsersPhotos.AsNoTracking().AnyAsync(up => up.UserId == userId);
            if (hasPhotos)
            {
                var maxOrderNumber = await _airbnbContext.UsersPhotos.AsNoTracking().Where(up => up.UserId == userId).MaxAsync(up => up.OrderNum);
                return maxOrderNumber + 1;
            }
            return 1;
        }

        public async Task AddHousingPhotoAsync(int housingId, Photo photo)
        {
            var orderNumber = await GetLastOrderNumberForHousingPhotoAsync(housingId);
            photo.HousingPhotos.Add(new HousingPhoto { OrderNumber = orderNumber, HousingId = housingId });
            var photoAddResult = _airbnbContext.Photos.Add(photo);
            await _airbnbContext.SaveChangesAsync();
        }

        public async Task<Photo> DeleteHousingPhotoAsync(int photoId)
        {                
            var entities = await _airbnbContext.HousingPhotos.AsNoTracking().Where(hp => hp.PhotoId == photoId).ToListAsync();
            _airbnbContext.HousingPhotos.RemoveRange(entities);
            //await _airbnbContext.SaveChangesAsync();

            var image = await _airbnbContext.Photos.AsNoTracking().FirstAsync(p => p.PhotoId == photoId);
            _airbnbContext.Photos.Remove(image);
            await _airbnbContext.SaveChangesAsync();

            return image;
        }

        public async Task AddUserPhotoAsync(int userId, Photo photo)
        {
            var orderNumber = await GetLastOrderNumberForUserPhotoAsync(userId);
            photo.UsersPhotos.Add(new UsersPhoto { OrderNum = orderNumber, UserId = userId});
            var photoAddResult = _airbnbContext.Photos.Add(photo);
            await _airbnbContext.SaveChangesAsync();
        }



        //public async Task<Housing> AddHousingAsync(HousingCreateDto model)
        //{
        //    var housing = new Housing
        //    {
        //        Description = model.Description,
        //        Title = model.Title,
        //        CurrentNightPrice = model.NightPrice,
        //        FullAddress = model.Address,

        //        BathsAmount = 1,
        //        CalendarId = 1,
        //        CurrencyId = 1,
        //        HasSeparateBath = true,
        //        HousingPartId = 1,
        //        HousingSubtypeId = 1,
        //        IsHidden = false,
        //        IsCompletelyForGuests = true,
        //        LandlordId = 22,
        //        Latitude = 1.1111,
        //        Longitude = 2.324344,
        //        NightMaxPrice = model.NightPrice,
        //        NightMinPrice = model.NightPrice,
        //        NightBasePrice = model.NightPrice,
        //        LocalArrivalMaxTimeId = 3,
        //        LocalArrivalMinTimeId = 1,
        //        LocalDepartureTimeId = 10,
        //        MaxBookingDays = 10,
        //        MaxGuestsAmount = 6,
        //        MinBookingDays = 1,
        //        PostIndex = 3223242,
        //        StreetId = 50
        //    };

        //    var h = _airbnbContext.Housings.Add(housing);
        //    await _airbnbContext.SaveChangesAsync();
        //    return h.Entity;
        //}
    }
}

