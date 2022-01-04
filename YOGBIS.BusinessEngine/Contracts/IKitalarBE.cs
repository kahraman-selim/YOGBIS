using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.Common.ResultModels;
using YOGBIS.Common.VModels;

namespace YOGBIS.BusinessEngine.Contracts
{
    public interface IKitalarBE
    {
        Result<List<KitalarVM>> KitalariGetir();     
        Result<KitalarVM> KitaGetir(int id);
       
    }
}
