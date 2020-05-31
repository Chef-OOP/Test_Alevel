using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test_Alevel.ApiService.AuthService;
using Test_Alevel.ApiService.BasketService;
using Test_Alevel.ApiService.MasterCategoryService;

namespace Test_Alevel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<MasterCategoryApi>(opt=>
            {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpClient<AuthApi>(opt =>
            {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpClient<BasketApiService>(opt =>
            {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
