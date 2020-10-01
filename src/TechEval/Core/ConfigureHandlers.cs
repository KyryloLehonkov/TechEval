using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace TechEval.Core
{
    public static class ConfigureHandlers
    {
        public static IServiceCollection RegisterCommandHandlers(this IServiceCollection services)
        {
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();

            var handlerInterface = typeof(ICommandHandler<,>);
            var allHandlers =
               Assembly.GetExecutingAssembly().GetTypes()
               .Where(c =>
                 c.GetInterfaces().Any(x => x.IsGenericType && handlerInterface.IsAssignableFrom(x.GetGenericTypeDefinition())));

            foreach (var handler in allHandlers)
            {
                var commandHandlerInterface = handler.GetInterfaces()
                    .Where(c => c.IsGenericType && c.Name.Contains("ICommandHandler"))
                    .FirstOrDefault();
                if (commandHandlerInterface != null)
                {
                    services.AddTransient(commandHandlerInterface, handler);
                }
            }
            return services;
        }
    }

}
