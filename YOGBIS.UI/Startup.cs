using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using YOGBIS.BusinessEngine.Contracts;
using YOGBIS.BusinessEngine.Implementaion;
using YOGBIS.Common.ConstantsModels;
using YOGBIS.Common.Mappings;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;
using YOGBIS.Data.DbModels;
using YOGBIS.Data.Implementaion;

namespace YOGBIS.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
       public void ConfigureServices(IServiceCollection services)
        {


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddDbContext<YOGBISContext>(options => options.UseMySQL(Configuration.GetConnectionString("YOGBISConnection")));
            //services.AddDbContext<YOGBISContext>(options => options.UseSqlServer(Configuration.GetConnectionString("YOGBISConnection")));

            #region Scopeds
            services.AddScoped<IDerecelerBE, DerecelerBE>();
            services.AddScoped<IEtkinliklerBE, EtkinliklerBE>();
            services.AddScoped<IEyaletlerBE, EyaletlerBE>();
            services.AddScoped<IKitalarBE, KitalarBE>();
            services.AddScoped<IKullaniciBE, KullaniciBE>();
            services.AddScoped<IMulakatOlusturBE, MulakatOlusturBE>();
            services.AddScoped<IMulakatSorulariBE, MulakatSorulariBE>();
            services.AddScoped<INotlarBE, NotlarBE>();
            services.AddScoped<IOgrencilerBE, OgrencilerBE>();
            services.AddScoped<IOkulBilgiBE, OkulBilgiBE>();
            services.AddScoped<IOkullarBE, OkullarBE>();
            services.AddScoped<ISehirlerBE, SehirlerBE>();
            services.AddScoped<ISoruBankasiBE, SoruBankasiBE>();
            services.AddScoped<ISoruKategorileriBE, SoruKategorileriBE>();
            services.AddScoped<IUlkeGruplariBE, UlkeGruplariBE>();
            services.AddScoped<IUlkelerBE, UlkelerBE>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Maps));
            #endregion


            services.AddIdentity<Kullanici, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true; //kullanýcý email giriþi zorunluluðu
                //options.User.AllowedUserNameCharacters="" izin verilen karakterler için
                //options.SignIn.RequireConfirmedEmail= email doðrulama zorunluluðu,
                //options.Password.RequireDigit þifrenin sayý zorunluluðu
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<YOGBISContext>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddMvc();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                opt=> {
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
                options.ExpireTimeSpan = TimeSpan.FromMinutes(3); 
                options.LoginPath = "/Identity/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
           UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                endpoints.MapControllerRoute(name:"default",pattern:"{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
