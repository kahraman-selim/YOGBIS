using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Newtonsoft.Json;
using YOGBIS.Common.VModels;

public class AyarlarController : Controller
{
    private readonly IConfiguration _configuration;

    public AyarlarController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        // TempData'dan tarih bilgisini al
        if (TempData["RegistrationEndDate"] != null)
        {
            ViewBag.RegistrationEndDate = TempData["RegistrationEndDate"].ToString();
        }
        else
        {
            // TempData boşsa, appsettings.json'dan oku
            ViewBag.RegistrationEndDate = _configuration["AppSettings:RegistrationEndDate"];
        }

        return View();
    }

    [HttpPost]
    public IActionResult UpdateRegistrationDate(string newDate)
    {
        if (!string.IsNullOrEmpty(newDate))
        {
            // appsettings.json dosyasının yolunu al
            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            // appsettings.json dosyasını oku
            var appSettingsJson = System.IO.File.ReadAllText(appSettingsPath);

            // JSON'u dinamik bir objeye çevir
            var config = JsonConvert.DeserializeObject<dynamic>(appSettingsJson);

            // Sadece RegistrationEndDate alanını güncelle
            config["AppSettings"]["RegistrationEndDate"] = newDate;

            // Değişiklikleri appsettings.json dosyasına yaz
            System.IO.File.WriteAllText(appSettingsPath, JsonConvert.SerializeObject(config, Formatting.Indented));

            // TempData'ya yeni değeri kaydet
            TempData["RegistrationEndDate"] = newDate;
        }

        TempData["Success"] = "Tarih güncellendi";

        return RedirectToAction("Index");
    }
}

public class AppSettings
{
    public string RegistrationEndDate { get; set; }
}

