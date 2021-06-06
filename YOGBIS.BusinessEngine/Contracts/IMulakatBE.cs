using System.Collections.Generic;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;
using YOGBIS.Data.DbModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IMulakatBE
    {
        Result<List<MulakatSorulariVM>> GetAllMulakatSorulari();
        /// <summary>
        /// Seçilen Mülakat Sorusu bilgilerini getiren method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<MulakatSorulariVM> GetAllMulakatSorulari(int id);

    }
}
