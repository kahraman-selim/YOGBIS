#region using
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Mvc;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.Mappings;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;
using YOGBIS.Data.Implementaion;
using MySql.Data.EntityFrameworkCore.Infrastructure.Internal;
using YOGBIS.BusinessEngine.Implementation;
#endregion

namespace YOGBIS.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        #region Startup(IConfiguration configuration)
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        #region ConfigureServices(IServiceCollection services)
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddDbContext<YOGBISContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("YOGBISConnection"),
                sqlOptions => sqlOptions.CommandTimeout(180)); // Zaman a��m�n� 180 saniyeye ��kar

                //services.AddDbContext<YOGBISContext>(options => options.UseSqlServer(Configuration.GetConnectionString("YOGBISConnection")));
            });



            #region Scopeds
            services.AddScoped<IAdaylarBE, AdaylarBE>();
            services.AddScoped<IBranslarBE, BranslarBE>();
            services.AddScoped<IDerecelerBE, DerecelerBE>();
            services.AddScoped<IEtkinliklerBE, EtkinliklerBE>();
            services.AddScoped<IDosyaGaleriBE, DosyaGaleriBE>();
            services.AddScoped<IDuyurularBE, DuyurularBE>();
            services.AddScoped<IEyaletlerBE, EyaletlerBE>();
            services.AddScoped<IFotoGaleriBE, FotoGaleriBE>();
            services.AddScoped<IKitalarBE, KitalarBE>();
            services.AddScoped<IKullaniciBE, KullaniciBE>();
            services.AddScoped<IKomisyonlarBE, KomisyonlarBE>();
            services.AddScoped<IMulakatOlusturBE, MulakatOlusturBE>();
            services.AddScoped<IMulakatSorulariBE, MulakatSorulariBE>();
            services.AddScoped<INotlarBE, NotlarBE>();
            services.AddScoped<IOgrencilerBE, OgrencilerBE>();
            services.AddScoped<IOkulBilgiBE, OkulBilgiBE>();
            services.AddScoped<IOkulBinaBolumBE, OkulBinaBolumBE>();
            services.AddScoped<IOkullarBE, OkullarBE>();
            services.AddScoped<ISSSBE, SSSBE>();
            services.AddScoped<ISehirlerBE, SehirlerBE>();
            services.AddScoped<ISiniflarBE, SiniflarBE>();
            services.AddScoped<ISoruBankasiBE, SoruBankasiBE>();
            services.AddScoped<ISoruKategorileriBE, SoruKategorileriBE>();
            services.AddScoped<ISubelerBE, SubelerBE>();
            services.AddScoped<IUlkeGruplariBE, UlkeGruplariBE>();
            services.AddScoped<IUlkelerBE, UlkelerBE>();
            services.AddScoped<IUlkeTercihleriBE, UlkeTercihleriBE>();
            services.AddScoped<IUlkeTercihBranslarBE, UlkeTercihBranslarBE>();

            services.AddScoped<IProgressService, ProgressService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Maps));
            
            #endregion


            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue;
                //options.AllowSynchronousIO = true; // E�er senkron IO gerekliyse
            });

            services.AddHttpClient("MyClient", client =>
            {
                client.Timeout = TimeSpan.FromMinutes(10); // Zaman a��m�n� 10 dakikaya ��kar
            });

            services.AddIdentity<Kullanici, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true; //kullan�c� email giri�i zorunlulu�u
                //options.User.AllowedUserNameCharacters="" izin verilen karakterler i�in
                //options.SignIn.RequireConfirmedEmail= email do�rulama zorunlulu�u,
                //options.Password.RequireDigit �ifrenin say� zorunlulu�u
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<YOGBISContext>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddMvc();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".YOGBIS.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                opt =>
                {
                    opt.Cookie.HttpOnly = true;
                    opt.Cookie.Name = "YOGBISCookies";
                    opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                    opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
                    opt.ExpireTimeSpan = TimeSpan.FromDays(5);
                });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "YOGBISCookies";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Identity/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.AddHostedService<SessionCheckBackgroundService>();
        }
        #endregion

        #region SessionCheckBackgroundService
        public class SessionCheckBackgroundService : BackgroundService
        {
            private readonly IServiceProvider _serviceProvider;

            public SessionCheckBackgroundService(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Kullanici>>();

                            var activeUsers = await userManager.Users
                                .Where(u => u.OturumDurumu == true)
                                .ToListAsync();

                            foreach (var user in activeUsers)
                            {
                                user.OturumDurumu = false;
                                await userManager.UpdateAsync(user);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // Hata loglamas� yap�labilir
                    }

                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
            }
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        #region Configure
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
        UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            SeedData.Seed(userManager, roleManager);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Detail",
                    pattern: "{controller}/{id:Guid}",
                    defaults: new { action = "Details", id = UrlParameter.Optional },
                    constraints: new { id = "[A-Z0-9]{8}-([A-Z0-9]{4}-){3}[A-Z0-9]{12}" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        #endregion
    }
}
