using AutoMapper;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.Common.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<AdayDDK, AdayDDKVM>().ReverseMap();
            CreateMap<AdayGorevKaydi, AdayGorevKaydiVM>().ReverseMap();
            CreateMap<Adaylar, AdaylarVM>().ReverseMap();
            CreateMap<AdayNot, AdayNotVM>().ReverseMap();
            CreateMap<AutoHistory, AutoHistoryVM>().ReverseMap();
            CreateMap<Branslar, BranslarVM>().ReverseMap();
            CreateMap<EPostaAdresleri, EPostaAdresleriVM>().ReverseMap();
            CreateMap<Etkinlikler, EtkinliklerVM>().ReverseMap();
            CreateMap<Eyaletler, EyaletlerVM>().ReverseMap();
            CreateMap<FotoGaleri, FotoGaleriVM>().ReverseMap();
            CreateMap<GorevKararPdfGaleri, GorevKararPdfGaleriVM>().ReverseMap();
            CreateMap<IkametAdresleri, IkametAdresleriVM>().ReverseMap();
            CreateMap<Ilceler, IlcelerVM>().ReverseMap();
            CreateMap<Iller, IllerVM>().ReverseMap();
            CreateMap<IllerMdEPosta, IllerMdEPostaVM>().ReverseMap();
            CreateMap<Kitalar, KitalarVM>().ReverseMap();
            CreateMap<Kullanici, KullaniciVM>().ReverseMap();
            CreateMap<Mulakatlar, MulakatlarVM>().ReverseMap();
            CreateMap<MulakatSorulari, MulakatSorulariVM>().ReverseMap();
            CreateMap<Notlar, NotlarVM>().ReverseMap();
            CreateMap<Ogrenciler, OgrencilerVM>().ReverseMap();
            CreateMap<OkulBilgi, OkulBilgiVM>().ReverseMap();
            CreateMap<Okullar, OkullarVM>().ReverseMap();
            CreateMap<Personeller, PersonellerVM>().ReverseMap();
            CreateMap<Sehirler, SehirlerVM>().ReverseMap();
            CreateMap<Siniflar, SiniflarVM>().ReverseMap();
            CreateMap<SoruBankasi, SoruBankasiVM>().ReverseMap();
            CreateMap<SoruBankasiLog, SoruBankasiLogVM>().ReverseMap();
            CreateMap<SoruDereceler, SoruDerecelerVM>().ReverseMap();
            CreateMap<SoruDerece, SoruDereceVM>().ReverseMap();
            CreateMap<SoruKategori, SoruKategoriVM>().ReverseMap();
            CreateMap<SoruKategoriler, SoruKategorilerVM>().ReverseMap();
            CreateMap<SoruOnay, SoruOnayVM>().ReverseMap();
            CreateMap<Subeler, SubelerVM>().ReverseMap();
            CreateMap<UlkeGruplari, UlkeGruplariVM>().ReverseMap();
            CreateMap<Ulkeler, UlkelerVM>().ReverseMap();
            CreateMap<Universiteler, UniversitelerVM>().ReverseMap();

        }
    }
}
