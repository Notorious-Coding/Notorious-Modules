using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NotoriousModules
{
    public static class SetupExtensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            var type = typeof(Module);
            var moduleTypes = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p != typeof(Module) && !p.IsAbstract);

            List<Module> modules = new List<Module>();  
            foreach (var moduleType in moduleTypes)
            {
                Module module = (Module)Activator.CreateInstance(moduleType)!;
                module.AddServices(services, configuration);
                services.AddSingleton(module);
            }

            return services;
        }

        public static WebApplication UseModules(this WebApplication app)
        {
            IEnumerable<Module> modules = app.Services.GetServices<Module>();
            foreach (var module in modules)
            {
                module.AddModuleEndpoints(app);
                module.AddAppConfiguration(app);
            }

            return app;
        }

        public static T? GetModuleConfiguration<T>(this IConfiguration configuration) where T : ModuleConfiguration
        {
            return configuration.GetSection(T.TAG).Get<T>();
        }
    }
}
