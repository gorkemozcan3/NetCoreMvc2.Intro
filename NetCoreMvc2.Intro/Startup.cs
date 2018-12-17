using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMvc2.Intro.Identity;
using NetCoreMvc2.Intro.Models;
using NetCoreMvc2.Intro.Services;

namespace NetCoreMvc2.Intro
{
    public class Startup
    {
        // configurasyon yapılandırması (appsettings.json)
        IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Entity framework configurasyonu  
            //services.AddDbContext<SchoolContext>(options => options.UseSqlServer(_configuration["DbConnection"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(_configuration["DbConnection"]));

            // Identity configurasyonu
            services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.SignIn.RequireConfirmedEmail = true;
                
            });

            // Cookie configurasyonu
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Security/Login";
                options.LogoutPath = "/Security/Logout";
                options.AccessDeniedPath = "/Security/AccessDenied";
                options.SlidingExpiration = true; // cookienin süresi dolmasına yakın giriş yapılırsa süre yenilenmesi için true
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".NetCoreDemo.Security.Cookie",
                    Path = "/", // roota atar
                    SameSite = SameSiteMode.Lax, // aynı domainden erişim sağlar, strict olursa uygulama harici erişim vermez
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
            });

            // Dependency Injection prensibine göre interface in çağıracağı class yapısı belirtiliyor.
            // interface in implemente edildiği yerlerde belirtilen class ın methodları çalışacaktır.
            // Loglama operasyonlarında 2 farklı tool için hangisinin kullanılacağı ya da DB erişimlerinde MSSQL/Oracle 2 farklı class vs için kolaylık sağlıyor
            // Loglama, caching, authorization vb işlemlerde, CRM uygulamalarında farklı müşteri tiplerie göre uygulamayı kolayca değiştirmede kullanılkır..
            services.AddScoped<ICalculator, Calculator18>();

            // Singletonda nesne 1 kere yaratıldığında sürekli bellekte tutularak her zaman o kullanılır.
            // Sıklıkla kullanılan classlar için uygun
            //services.AddSingleton<ICalculator, Calculator18>();

            // Scope ta ise nesne yaratılır ve işi bittiğinde bellekte silinir. Çok noktada çağırılan bir class için performans düşürür.

            //Session yapılandırması
            services.AddSession();
            services.AddDistributedMemoryCache();
            //Session yapılandırması
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enviroment prod olduğunda hata alındığında teknik detaylar görünmez...
            env.EnvironmentName = EnvironmentName.Production;
            if (env.IsDevelopment())
            {
                // Developer için teknik detayların yer aldığı hata gösterimi sağlıyor
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Prod ortamı için routing veriliyor...
                app.UseExceptionHandler("/error");
            }

            //Session yapılandırması
            app.UseSession();
            //Session yapılandırması

            app.UseAuthentication(); // identity işlemlerini aktif eder

            // app.UseMvcWithDefaultRoute();
            // with default route aşağıdaki Default ismindeki route aynı işlemi yapıyor(home/index/id?). Farklı routelar için değiştirildi.
            app.UseMvc(ConfigureRoutes);
            // Klasik routing dışında Attribute routing metodu da AdminController içinde yapıldı.

            app.UseStaticFiles(); // libman kütüphaneleerini aktif eder


        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Filter}/{action=Index}/{id?}");
            // Başında Gorkem ile gelenler bu routa göre çalısacak
            routeBuilder.MapRoute("GorkemRoute", "Gorkem/{controller=Home}/{action=Index3}/{id?}");
            routeBuilder.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
        }
    }
}
