using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
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
        }
    }
}
