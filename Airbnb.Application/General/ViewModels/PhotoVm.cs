using Airbnb.Application.Common.Mappings;
using Airbnb.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.General.ViewModels
{
    public class PhotoVm : IMapWith<UsersPhoto>, IMapWith<HousingPhoto>
    {
        public int PhotoId { get; set; }
        public string Url { get; set; }
        public int OrderNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UsersPhoto, PhotoVm>()
                .ForMember(vm => vm.OrderNumber, ops =>
                    ops.MapFrom(up => up.OrderNum))
                .ForMember(vm => vm.Url, ops =>
                    ops.MapFrom(up => up.Photo.Url));

            profile.CreateMap<HousingPhoto, PhotoVm>()
                .ForMember(vm => vm.OrderNumber, ops =>
                    ops.MapFrom(hp => hp.OrderNumber))
                .ForMember(vm => vm.Url, ops =>
                    ops.MapFrom(hp => hp.Photo.Url));
        }
    }
}
