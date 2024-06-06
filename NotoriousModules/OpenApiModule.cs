
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NotoriousModules
{
    public abstract class OpenApiModule : Module
    {
        public OpenApiModule() : base()
        {
        }


        protected override void ConfigureEachEndpoint(IEndpointConventionBuilder routeHandlerBuilder)
        {
            base.ConfigureEachEndpoint(routeHandlerBuilder);

            routeHandlerBuilder
                .WithTags(ModuleName)
                .WithOpenApi();
        }
    }
}
