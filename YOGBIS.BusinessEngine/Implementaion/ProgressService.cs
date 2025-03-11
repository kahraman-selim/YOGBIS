using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using YOGBIS.BusinessEngine.Contracts;

namespace YOGBIS.BusinessEngine.Implementation
{
    public class ProgressService : IProgressService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly object _lockObject = new object();
        private static Dictionary<string, ProgressData> _progressData = new Dictionary<string, ProgressData>();
        private readonly CultureInfo _trCulture = new CultureInfo("tr-TR");

        public ProgressService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void UpdateProgress(string sessionId, Action<ProgressData> updateAction)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentException("Session ID boş olamaz!", nameof(sessionId));
            }

            lock (_lockObject)
            {
                try
                {
                    if (!_progressData.ContainsKey(sessionId))
                    {
                        _progressData[sessionId] = new ProgressData
                        {
                            IslemAsamasi = "Baslatiliyor",
                            ToplamKayit = 0,
                            IslemYapilan = 0,
                            Yuzde = 0,
                            BasariliEklenen = 0,
                            Mesaj = "İşlem başlatılıyor..."
                        };
                    }

                    var progress = _progressData[sessionId];
                    updateAction(progress);

                    // Puan formatlaması
                    if (progress.MYSPuan.HasValue)
                    {
                        progress.MYSPuan = Math.Round(progress.MYSPuan.Value, 2, MidpointRounding.AwayFromZero);
                    }

                    // Tarih formatlaması
                    if (!string.IsNullOrEmpty(progress.MYSSTarih))
                    {
                        if (DateTime.TryParse(progress.MYSSTarih, out DateTime date))
                        {
                            progress.MYSSTarih = date.ToString("dd.MM.yyyy", _trCulture);
                        }
                    }

                    // Yüzde hesaplama
                    if (progress.ToplamKayit > 0)
                    {
                        progress.Yuzde = (int)(((double)progress.IslemYapilan / progress.ToplamKayit) * 100);
                    }

                    var progressJson = JsonConvert.SerializeObject(progress);
                    var httpContext = _httpContextAccessor.HttpContext;
                    if (httpContext != null)
                    {
                        httpContext.Session.SetString("CurrentProgress_" + sessionId, progressJson);
                    }
                }
                catch (Exception ex)
                {
                    _progressData[sessionId] = new ProgressData
                    {
                        IslemAsamasi = "Hata",
                        Error = "İşlem sırasında bir hata oluştu: " + ex.Message,
                        Mesaj = "Hata oluştu!"
                    };
                }
            }
        }

        public void ResetProgress(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                return;
            }

            lock (_lockObject)
            {
                try
                {
                    if (_progressData.ContainsKey(sessionId))
                    {
                        _progressData.Remove(sessionId);
                    }

                    var httpContext = _httpContextAccessor.HttpContext;
                    if (httpContext != null)
                    {
                        httpContext.Session.Remove("CurrentProgress_" + sessionId);
                    }
                }
                catch
                {
                    // Silme işlemi sırasındaki hataları yok sayabiliriz
                }
            }
        }

        public ProgressData GetProgress(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                return new ProgressData
                {
                    IslemAsamasi = "Hata",
                    Error = "Geçersiz oturum!",
                    Mesaj = "Geçersiz oturum!"
                };
            }

            lock (_lockObject)
            {
                try
                {
                    if (_progressData.TryGetValue(sessionId, out ProgressData progress))
                    {
                        return progress;
                    }

                    var httpContext = _httpContextAccessor.HttpContext;
                    if (httpContext != null)
                    {
                        var progressJson = httpContext.Session.GetString("CurrentProgress_" + sessionId);
                        if (!string.IsNullOrEmpty(progressJson))
                        {
                            progress = JsonConvert.DeserializeObject<ProgressData>(progressJson);
                            if (progress != null)
                            {
                                // Puan formatlaması
                                if (progress.MYSPuan.HasValue)
                                {
                                    progress.MYSPuan = Math.Round(progress.MYSPuan.Value, 2, MidpointRounding.AwayFromZero);
                                }

                                // Tarih formatlaması
                                if (!string.IsNullOrEmpty(progress.MYSSTarih))
                                {
                                    if (DateTime.TryParse(progress.MYSSTarih, out DateTime date))
                                    {
                                        progress.MYSSTarih = date.ToString("dd.MM.yyyy", _trCulture);
                                    }
                                }

                                _progressData[sessionId] = progress;
                                return progress;
                            }
                        }
                    }

                    progress = new ProgressData
                    {
                        IslemAsamasi = "Baslatiliyor",
                        ToplamKayit = 0,
                        IslemYapilan = 0,
                        Yuzde = 0,
                        BasariliEklenen = 0,
                        Mesaj = "İşlem başlatılıyor..."
                    };
                    _progressData[sessionId] = progress;

                    return progress;
                }
                catch (Exception ex)
                {
                    return new ProgressData
                    {
                        IslemAsamasi = "Hata",
                        Error = "İşlem bilgisi alınırken hata oluştu: " + ex.Message,
                        Mesaj = "Hata oluştu!"
                    };
                }
            }
        }
    }
}
