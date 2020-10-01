using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TechEval.DataContext
{
    public static class SetupDbExt
    {
        public static IServiceCollection MigrateDb(this IServiceCollection services) {

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var db = serviceProvider.GetService<Db>();
                db.Database.Migrate();
            }

            return services;
        }
    }
}
