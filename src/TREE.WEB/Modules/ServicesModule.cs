using Microsoft.Extensions.DependencyInjection;
using TREE.WEB.Services.Abstract;
using TREE.WEB.Services.Concrete;

namespace TREE.WEB.Modules
{
    public static class ServicesModule
    {
        public static void AddServicesModule(this IServiceCollection services)
        {
            services.AddScoped<INodeService, NodeService>();
        }
    }
}
