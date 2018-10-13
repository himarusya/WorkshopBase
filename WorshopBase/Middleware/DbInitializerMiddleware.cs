using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;
using WorshopBase.Data;

namespace WorkshopBase.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            // инициализация базы данных по университетам
            _next = next;

        }
        public Task Invoke(HttpContext context, IServiceProvider serviceProvider, WorkshopContext dbContext)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                //DbUserInitializer.Initialize(serviceProvider).Wait();
                DbInitializer.Initialize(dbContext);

                context.Session.SetString("starting", "Yes");
            }

            // Call the next delegate/middleware in the pipeline
            return _next.Invoke(context);
        }
    }
}
