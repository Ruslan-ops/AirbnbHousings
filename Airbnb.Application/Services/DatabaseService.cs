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


        private async Task<int> GetLastOrderNumberForPhotoAsync(int housingId)
        {
            bool hasPhotos = await _airbnbContext.HousingPhotos.AnyAsync(hp => hp.HousingId == housingId);
            if (hasPhotos)
            {
                var maxOrderNumber = await _airbnbContext.HousingPhotos.AsQueryable().Where(hp => hp.HousingId == housingId).MaxAsync(hp => hp.OrderNumber);
                return maxOrderNumber + 1;
            }
            return 1;
        }

        public async Task AddHousingPhotoAsync(int housingId, string photoName, string url)
        {
            using var tx = _airbnbContext.Database.BeginTransaction();

            var orderNumber = await GetLastOrderNumberForPhotoAsync(housingId);
            var photoAddResult = _airbnbContext.Photos.Add(new Photo { CreatedDate = new DateTime(), Name = photoName, Url = url });
            await _airbnbContext.SaveChangesAsync();

            await _airbnbContext.HousingPhotos.AddAsync(new HousingPhoto { HousingId = housingId, PhotoId = photoAddResult.Entity.PhotoId, OrderNumber = orderNumber });
            await _airbnbContext.SaveChangesAsync();

            await tx.CommitAsync();
        }

        public async Task<Photo> DeleteHousingPhotoAsync(int imageId)
        {
            using var tx = await _airbnbContext.Database.BeginTransactionAsync();

            var entities = await _airbnbContext.HousingPhotos.Where(hp => hp.PhotoId == imageId).ToListAsync();
            _airbnbContext.HousingPhotos.RemoveRange(entities);
            await _airbnbContext.SaveChangesAsync();

            var image = await _airbnbContext.Photos.FirstAsync(p => p.PhotoId == imageId);
            _airbnbContext.Photos.Remove(image);
            await _airbnbContext.SaveChangesAsync();

            await tx.CommitAsync();

            return image;
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

