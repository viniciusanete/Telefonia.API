using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telefonia.Domain.Services;
using Telefonia.Infrastructure.Data.Repository;

namespace Telefonia.Host.Infrastructure.ExtensionMethods
{
    public static class DomainInjectDependence
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddTransient<Domain.Plano.IPlanoService, PlanoService>();
            services.AddTransient<Domain.Plano.IPlanoRepository, PlanoRepository>();

            services.AddScoped<Telefonia.Context.Context.IContext, Telefonia.Context.Context.Context>();

            /*Logger*/
            services.AddTransient<Microsoft.Extensions.Logging.ILogger, Middleware.Logger>();
            /*Logger*/

            return services;
        }
    }
}
