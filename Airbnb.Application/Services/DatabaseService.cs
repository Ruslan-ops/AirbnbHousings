using Airbnb.Application.Common.Consts;
using Airbnb.Application.Common.Exceptions;
using Airbnb.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public async Task<Photo> DeleteHousingPhotoAsync(int photoId, int housingId, CancellationToken cancellationToken)
        {
            var photo = await _airbnbContext.Photos.FirstAsync(p => p.PhotoId == photoId, cancellationToken);
            _airbnbContext.Photos.Remove(photo);
            await _airbnbContext.SaveChangesAsync(cancellationToken);
            return photo;
        }

        public async Task<bool> UserOwnsHousing(int userId, int housingId, CancellationToken cancellationToken)
        {
            return await _airbnbContext.Housings.AsNoTracking().AnyAsync(h => h.LandlordId == userId, cancellationToken);
        }

        public async Task<bool> HousingHasPhoto(int housingId, int photoId, CancellationToken cancellationToken)
        {
            return await _airbnbContext.HousingPhotos.AsNoTracking().AnyAsync(hp => hp.HousingId == housingId && hp.PhotoId == photoId, cancellationToken);
        }
      
        public async Task AddUserPhotoAsync(int userId, Photo photo, CancellationToken cancellationToken)
        {
            var orderNumber = await GetLastOrderNumberForUserPhotoAsync(userId);
            photo.UsersPhotos.Add(new UsersPhoto { OrderNum = orderNumber, UserId = userId});
            var photoAddResult = _airbnbContext.Photos.Add(photo);
            await _airbnbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Photo> DeleteUserPhotoAsync(int photoId, CancellationToken cancellationToken)
        {
            var photo = await _airbnbContext.Photos.FirstAsync(p => p.PhotoId == photoId, cancellationToken);
            _airbnbContext.Photos.Remove(photo);
            await _airbnbContext.SaveChangesAsync(cancellationToken);
            return photo;
        }

        public async Task<bool> UserHasPhoto(int userId, int photoId, CancellationToken cancellationToken)
        {
            return await _airbnbContext.UsersPhotos.AsNoTracking().AnyAsync(up => up.UserId == userId && up.PhotoId == photoId, cancellationToken);
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

