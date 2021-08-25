using AutoMapper;
using Commercial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commercial.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        //public AutoMapperProfiles()
        //{
        //    CreateMap<Entity, EntityViewModel>();
        //    CreateMap<EntityViewModel, Entity>();
        //}
        public static IMapper Mapper { get; private set; }

        public static void Configure()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<tblMaster, MasterViewModel>()
                .ForMember(dest => dest.SupplierName,opt => opt.MapFrom(src => src.tblSupplier == null ? "" : src.tblSupplier.Name))
                .ForMember(dest => dest.BankName,opt => opt.MapFrom(src => src.tblBank == null ? "" : src.tblBank.BankName));

                cfg.CreateMap<MasterViewModel, tblMaster>();
                cfg.CreateMap<tblDetail, DetailViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.tblProduct == null ? "" : src.tblProduct.Name));
                cfg.CreateMap<DetailViewModel, tblDetail>();
                //cfg.CreateMap<Login, ProviderLoginVM>()
            });

            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}
