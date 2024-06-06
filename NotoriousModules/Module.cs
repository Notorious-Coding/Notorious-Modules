using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NotoriousModules
{
    public abstract class Module
    {
        public abstract string ModuleName { get;}
        private RouteGroupBuilder _builder { get; set; }

        public void AddModuleEndpoints(WebApplication app)
        {
            _builder = app.MapGroup(ModuleName.ToLower().Trim());
            ConfigureModuleEndpoints(_builder);
        }

        public void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDependencyInjection(services, configuration);
        }

        public void AddAppConfiguration(WebApplication app)
        {
            ConfigureApp(app);
        }

        protected virtual void ConfigureModuleEndpoints(RouteGroupBuilder routeGroupBuilder)
        {

        }

        protected virtual void ConfigureEachEndpoint(IEndpointConventionBuilder routeHandlerBuilder)
        {

        }

        protected virtual void ConfigureDependencyInjection(IServiceCollection services, IConfiguration configuration)
        {
            
        }

        protected virtual void ConfigureApp(WebApplication app)
        {

        }

        public IEndpointConventionBuilder AddEndpoint(Func<RouteGroupBuilder, IEndpointConventionBuilder> endpoint)
        {
            IEndpointConventionBuilder endpointBuilder = endpoint(_builder);
            ConfigureEachEndpoint(endpointBuilder);

            return endpointBuilder;
        }

        public IEndpointConventionBuilder AddGetEndpoint(string pattern, Delegate handler)
        {
            IEndpointConventionBuilder endpointBuilder = _builder.MapGet(pattern, handler);
            ConfigureEachEndpoint(endpointBuilder);
            return endpointBuilder;
        }

        public IEndpointConventionBuilder AddPostEndpoint(string pattern, Delegate handler)
        {
            IEndpointConventionBuilder endpointBuilder = _builder.MapPost(pattern, handler);
            ConfigureEachEndpoint(endpointBuilder);
            return endpointBuilder;
        }

        public IEndpointConventionBuilder AddPutEndpoint(string pattern, Delegate handler)
        {
            IEndpointConventionBuilder endpointBuilder = _builder.MapPut(pattern, handler);
            ConfigureEachEndpoint(endpointBuilder);
            return endpointBuilder;
        }

        public IEndpointConventionBuilder AddPatchEndpoint(string pattern, Delegate handler)
        {
            IEndpointConventionBuilder endpointBuilder = _builder.MapPatch(pattern, handler);
            ConfigureEachEndpoint(endpointBuilder);
            return endpointBuilder;
        }

        public IEndpointConventionBuilder AddDeleteEndpoint(string pattern, Delegate handler)
        {
            IEndpointConventionBuilder endpointBuilder = _builder.MapDelete(pattern, handler);
            ConfigureEachEndpoint(endpointBuilder);
            return endpointBuilder;
        }
    }
}
