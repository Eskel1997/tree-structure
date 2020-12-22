using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TREE.DB.DAL;
using TREE.DB.Modules;
using TREE.WEB.Modules;
using TREE.WEB.Profiles;
using TREE.WEB.Validation;

namespace TREE.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TreeContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(typeof(NodeProfile));
            services.AddRepositoryModule();
            services.AddServicesModule();
            services.AddControllersWithViews();

            services.AddMvc()
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<NodeEditViewModelValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Tree}/{action=Index}/{id?}");
            });
        }
    }
}
