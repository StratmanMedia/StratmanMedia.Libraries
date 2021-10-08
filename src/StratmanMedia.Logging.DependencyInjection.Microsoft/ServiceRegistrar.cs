using Microsoft.Extensions.DependencyInjection;
using StratmanMedia.Logging.Contracts;

namespace StratmanMedia.Logging.DependencyInjection.Microsoft
{
    internal static class ServiceRegistrar
    {
        internal static void AddRequiredServices(IServiceCollection services, SMLoggerConfiguration configuration)
        {
            services.AddScoped<ISMLogger>(provider => new SMLogger(configuration));
        }
    }
}