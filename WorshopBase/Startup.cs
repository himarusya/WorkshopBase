using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorshopBase.Models;
using WorshopBase.Data;
using WorshopBase.Middleware;
using WorshopBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace WorshopBase
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
            services.AddDbContext<WorkshopContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<WorkshopContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<WorkshopService>();

            services.AddMvc(options =>
            {
                // определение профилей кэширования
                options.CacheProfiles.Add("Caching",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
                options.CacheProfiles.Add("NoCaching",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            });
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, WorkshopContext context)
        {
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();
            app.UseWorkshopCache("Workshop");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });

            // инициализация базы данных
            DbInitializer.Initialize(context);
        }
    }
}
