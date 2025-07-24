// powered by 1986sec
using AutoMapper;
using IndustrialCampusAPI.DTOs;
using IndustrialCampusAPI.Models;

namespace IndustrialCampusAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Firma mappings
            CreateMap<Firma, FirmaDTO>();
            CreateMap<FirmaCreateDTO, Firma>();
            CreateMap<FirmaUpdateDTO, Firma>();

            // Kullanici mappings
            CreateMap<Kullanici, KullaniciDTO>();
            CreateMap<KullaniciRegisterDTO, Kullanici>();
            CreateMap<KullaniciUpdateDTO, Kullanici>();
            CreateMap<Kullanici, KullaniciUpdateDTO>();

            // Ziyaret mappings
            CreateMap<Ziyaret, ZiyaretDTO>()
                .ForMember(dest => dest.FirmaAdi, opt => opt.MapFrom(src => src.Firma.FirmaAdi))
                .ForMember(dest => dest.KullaniciAdi, opt => opt.MapFrom(src => src.Kullanici.AdSoyad));
            CreateMap<ZiyaretCreateDTO, Ziyaret>();
            CreateMap<ZiyaretUpdateDTO, Ziyaret>();

            // GelirGider mappings
            CreateMap<GelirGider, GelirGiderDTO>()
                .ForMember(dest => dest.FirmaAdi, opt => opt.MapFrom(src => src.Firma.FirmaAdi));
            CreateMap<GelirGiderCreateDTO, GelirGider>();
            CreateMap<GelirGiderUpdateDTO, GelirGider>();
        }
    }
}
// powered by 1986sec