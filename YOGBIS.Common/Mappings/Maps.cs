using AutoMapper;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Adaylar, AdaylarVM>().ReverseMap();
            CreateMap<Aktiviteler, AktivitelerVM>().ReverseMap();
            CreateMap<Dereceler, DerecelerVM>().ReverseMap();
            CreateMap<Eyaletler, EyaletlerVM>().ReverseMap();
            CreateMap<GorevKaydi, GorevKaydiVM>().ReverseMap();
            CreateMap<Kitalar, KitalarVM>().ReverseMap();
            CreateMap<Kullanici, KullaniciVM>().ReverseMap();
            CreateMap<Mulakatlar, MulakatlarVM>().ReverseMap();
            CreateMap<MulakatSorulari, MulakatSorulariVM>().ReverseMap();
            CreateMap<Notlar, NotlarVM>().ReverseMap();
            CreateMap<Ogrenciler, OgrencilerVM>().ReverseMap();
            CreateMap<Ogretmenler, OgretmenlerVM>().ReverseMap();
            CreateMap<OkulBilgi, OkulBilgiVM>().ReverseMap();
            CreateMap<Okullar, OkullarVM>().ReverseMap();
            CreateMap<Okutmanlar, OkutmanlarVM>().ReverseMap();
            CreateMap<Sehirler, SehirlerVM>().ReverseMap();
            CreateMap<Siniflar, SiniflarVM>().ReverseMap();
            CreateMap<SoruBankasi, SoruBankasiVM>().ReverseMap();
            CreateMap<SoruDerece, SoruDereceVM>().ReverseMap();
            CreateMap<SoruKategori, SoruKategoriVM>().ReverseMap();
            CreateMap<SoruKategoriler, SoruKategorilerVM>().ReverseMap();
            CreateMap<Subeler, SubelerVM>().ReverseMap();
            CreateMap<UlkeGruplari, UlkeGruplariVM>().ReverseMap();
            CreateMap<UlkeGruplariKitalar, UlkeGruplariKitalarVM>().ReverseMap();
            CreateMap<Ulkeler, UlkelerVM>().ReverseMap();
            CreateMap<Universiteler, UniversitelerVM>().ReverseMap();

        }
    }
}
