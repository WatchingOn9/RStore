using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RStore {
    public class Startup {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:RStoreProducts:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options =>
                 options.UseSqlServer(
                 Configuration["Data:RStoreIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddTransient<IImageRepository, EFImageRepository>();
            services.AddTransient<ISlideShowRepository, EFSlideShowRepository>();
            services.AddTransient<IPageRepository, EFPageRepository>();
            services.AddTransient<IComponentRepository, EFComponentRepository>();
            services.AddTransient<ISettingRepository, EFSettingRepository>();
            services.AddTransient<ICategoryRepository, EFCategoryRepository>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddScoped<ValidateReCaptchaAttribute>();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            } else {
                app.UseExceptionHandler("/Error/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc(routes => {

                routes.MapRoute(
                    name: null,
                    template: "google0549c693d710c2b3.html",
                    defaults: new { controller = "Page", action = "Google" }
                );

                routes.MapRoute(
                    name: null,
                    template: "sitemap.xml",
                    defaults: new { controller = "Page", action = "Sitemap" }
                );

                routes.MapRoute(
                    name: null,
                    template: "robots.txt",
                    defaults: new { controller = "Page", action = "RobotTxt" }
                );

                routes.MapRoute(
                    name: null,
                    template: "Page/{Id:int}",
                    defaults: new { controller = "Page", action = "Index" }
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{page:int}",
                    defaults: new { controller = "Page", action = "Index", Id = 3 }
                );

                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}",
                    defaults: new { controller = "Page", action = "Index", Id = 3 }
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Page", action = "Index", Id = 3 });

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Page", action = "Index", Id = 1 });

                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");

            });
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }

    
    }
}
