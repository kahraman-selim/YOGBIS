using AutoMapper;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<UlkeGruplari, UlkeGruplariVM>().ReverseMap();
            CreateMap<Kitalar, KitalarVM>().ReverseMap();
            CreateMap<Ulkeler, UlkelerVM>().ReverseMap();
            CreateMap<Eyaletler, EyaletlerVM>().ReverseMap();
            CreateMap<Sehirler, SehirlerVM>().ReverseMap();
            CreateMap<SoruKategoriler, SoruKategorilerVM>().ReverseMap();
            CreateMap<Mulakatlar, MulakatlarVM>().ReverseMap();
            CreateMap<MulakatSorulari, MulakatSorulariVM>().ReverseMap();
            CreateMap<SoruBankasi, SoruBankasiVM>().ReverseMap();            
            CreateMap<SoruKategori, SoruKategoriVM>().ReverseMap();
            CreateMap<Kullanici, KullaniciVM>().ReverseMap();
            CreateMap<Dereceler, DerecelerVM>().ReverseMap();
            CreateMap<UlkeGruplariKitalar, UlkeGruplariKitalarVM>().ReverseMap();
            CreateMap<Okullar, OkullarVM>().ReverseMap();
            CreateMap<OkulBilgi, OkulBilgiVM>().ReverseMap();
            CreateMap<Ogrenciler, OgrencilerVM>().ReverseMap();
            CreateMap<Notlar, NotlarVM>().ReverseMap();
            CreateMap<Adaylar, AdaylarVM>().ReverseMap();
        }
    }
}
