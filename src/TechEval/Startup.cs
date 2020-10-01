using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechEval.Core;
using TechEval.DataContext;

namespace TechEval
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
            services.AddRazorPages();
            services.AddDbContext<Db>(opt => opt
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddTransient<IODataDispatcher, ODataDispatcher>();
            services.RegisterCommandHandlers();


            services.AddControllers();

            services
                .AddQRest()
                .UseODataSemantics()
                .UseStandardCompiler(cpl =>
                {
                    cpl.UseCompilerCache = true;
                });

            services.MigrateDb();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler("/Error");


            app.UseRouting();
            app.UseStaticFiles();

          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(name: "default",
                     pattern: "api/{controller}/{action}");
            });
        }
    }
}
