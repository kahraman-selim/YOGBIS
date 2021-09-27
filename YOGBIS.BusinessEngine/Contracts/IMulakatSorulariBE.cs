using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IMulakatSorulariBE
    {
        Result<List<MulakatSorulariVM>> GetAllMulakatSorulari();
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
        Result<MulakatSorulariVM> GetAllMulakatSorulari(int id);

        Result<MulakatSorulariVM> MulakatSorusuGuncelle(MulakatSorulariVM model);
        
        Result<bool> MulakatSorusuSil(int id);

        Result<List<MulakatSorulariVM>> GetAllMulakatSorulariById(int id, string derece);
    }
}
