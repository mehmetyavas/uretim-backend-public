using AutoMapper;
using Business.Handlers.Giris.Commands;
using Business.Handlers.Production.Commands;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Entities.Concrete.Giris;
using Entities.Concrete.Netsis;
using Entities.Concrete.Uretim;
using Entities.Dtos.Giris;
using Entities.Dtos.Production;
using Entities.Dtos.Production.enjeksiyon;
using Entities.Dtos.WorkOrder;

namespace Business.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Tblisemri, WorkOrderDTO>().ReverseMap();
            CreateMap<RpUretimSeri, CreateProductionCommand>().ReverseMap();
            CreateMap<EryMakineTemizlik, HygieneDto>().ReverseMap();
            CreateMap<RpUretimSeriTakip, EnjeksiyonHammaddeDto>().ReverseMap();
            CreateMap<RpUretimSeriTakip, GetProducedMaterialsDto>()
                .ForMember(x => x.StokKodu, y => y.MapFrom(t => t.VsStokKodu))
                .ForMember(x => x.SeriNo, y => y.MapFrom(t => t.VsSeriNo))
                .ForMember(x => x.Miktar, y => y.MapFrom(t => t.Harcanan))
                .ForMember(x => x.DepoKod, y => y.MapFrom(t => t.DepoKodu)).ReverseMap();
            CreateMap<RpUretimSeriTakipMontaj, GetProducedMaterialsDto>()
                .ForMember(x => x.Miktar, y => y.MapFrom(t => t.Harcanan));
            CreateMap<RpUretimSeri, GetProducedDto>().ReverseMap();
        }
    }
}