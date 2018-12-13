using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMvc2.Intro.Services;

namespace NetCoreMvc2.Intro
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Dependency Injection prensibine göre interface in çağıracağı class yapısı belirtiliyor.
            // interface in implemente edildiği yerlerde belirtilen class ın methodları çalışacaktır.
            // Loglama operasyonlarında 2 farklı tool için hangisinin kullanılacağı ya da DB erişimlerinde MSSQL/Oracle 2 farklı class vs için kolaylık sağlıyor
            // Loglama, caching, authorization vb işlemlerde, CRM uygulamalarında farklı müşteri tiplerie göre uygulamayı kolayca değiştirmede kullanılkır..
            services.AddScoped<ICalculator, Calculator18>();

            // Singletonda nesne 1 kere yaratıldığında sürekli bellekte tutularak her zaman o kullanılır.
            // Sıklıkla kullanılan classlar için uygun
            //services.AddSingleton<ICalculator, Calculator18>();

            // Scope ta ise nesne yaratılır ve işi bittiğinde bellekte silinir. Çok noktada çağırılan bir class için performans düşürür.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseMvcWithDefaultRoute();
            // with default route aşağıdaki Default ismindeki route aynı işlemi yapıyor(home/index/id?). Farklı routelar için değiştirildi.
            app.UseMvc(ConfigureRoutes);
            // Klasik routing dışında Attribute routing metodu da AdminController içinde yapıldı.

            app.UseStaticFiles(); // libman kütüphaneleerini aktif eder
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index2}/{id?}");
            // Başında Gorkem ile gelenler bu routa göre çalısacak
            routeBuilder.MapRoute("GorkemRoute", "Gorkem/{controller=Home}/{action=Index3}/{id?}");
        }
    }
}
