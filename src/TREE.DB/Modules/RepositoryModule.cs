using Microsoft.Extensions.DependencyInjection;
using TREE.DB.Repositories.Abstract;
using TREE.DB.Repositories.Concrete;

namespace TREE.DB.Modules
{
    public static class RepositoryModule
    {
        public static void AddRepositoryModule(this IServiceCollection services)
        {
            services.AddScoped<INodeRepository, NodeRepository>();
        }
    }
}
