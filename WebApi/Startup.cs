using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Endpoint.Extensions;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using WebApi.Providers;
using System.Linq;
using System;
using System.Web.Http.Controllers;
using Domain.Endpoint.Interfaces.Services;
using Domain.Endpoint.Services;
using Infrastructure.Endpoint.Data;
using Domain.Endpoint.Interfaces.Repositories;
using Infrastructure.Endpoint.Data.Repositories;

[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi
{
    public class Startup
    {
         public void Configuration(IAppBuilder app)
        {
            ServiceCollection services = new ServiceCollection();
            HttpConfiguration config = new HttpConfiguration();

            ConfigureServices(services);

            DefaultDependencyResolver resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            config.DependencyResolver = resolver;

            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // Map controllers to use as services (DPI)
            services.AddControllersAsServices(
                typeof(Startup).Assembly
                    .GetExportedTypes()
                    .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                    .Where(t => typeof(IHttpController).IsAssignableFrom(t) || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            );

            services.AddScoped<ISqlDbConnection, SqlDbConnection>();
            services.AddScoped<IProveedorService, ProveedorService>();
            services.AddScoped<Domain.Endpoint.Interfaces.Services.ICategoria, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddInfrastructureServices();
        }
    }
}
