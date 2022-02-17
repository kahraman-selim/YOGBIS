using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace YOGBIS.Common.ConstantsModels
{
    public class FotoYukle
    {
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public FotoYukle(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Obsolete]
        private async Task<string> FotoYukleConstant(string dosyaYolu, IFormFile dosya) 
        {
            dosyaYolu += Guid.NewGuid().ToString() + "_" + dosya.FileName;

            string dosyaKlasor = Path.Combine(_hostingEnvironment.WebRootPath, dosyaYolu);

            await dosya.CopyToAsync(new FileStream(dosyaKlasor, FileMode.Create));

            return "/" + dosyaYolu;
        }
    }
}
