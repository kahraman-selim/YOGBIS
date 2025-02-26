using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.Common.SessionOperations;
using YOGBIS.Common.VModels;

namespace YOGBIS.UI.ViewComponents
{
    public class UlkeTercihleriViewComponent : ViewComponent
    {
        private readonly IUlkeTercihleriBE _ulkeTercihleriBE;

        public UlkeTercihleriViewComponent(IUlkeTercihleriBE ulkeTercihleriBE)
        {
            _ulkeTercihleriBE = ulkeTercihleriBE;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? year = null)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }

            var data = _ulkeTercihleriBE.UlkeTercihleriGetir((int)year);

            ViewBag.Year = year;
            return View(data.Data ?? new List<UlkeTercihVM>()); // Boş liste döndür, null dönme
        }
    }
}
