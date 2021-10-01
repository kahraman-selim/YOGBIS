using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IMulakatSorulariBE
    {
        Result<List<MulakatSorulariVM>> MulakatSorulariGetir();
        /// <summary>
        /// Mülakat Sorusu Ekleme Methodu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<MulakatSorulariVM> MulakatSorusuEkle(MulakatSorulariVM model);        
        /// <summary>
        /// Seçilen Mülakat Sorusu bilgilerini getiren method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<MulakatSorulariVM> MulakatSorulariGetir(int id);

        Result<MulakatSorulariVM> MulakatSorusuGuncelle(MulakatSorulariVM model);
        
        Result<bool> MulakatSorusuSil(int id);

        Result<List<MulakatSorulariVM>> MulakatSorulariGetir(int id, string derece);
    }
}
