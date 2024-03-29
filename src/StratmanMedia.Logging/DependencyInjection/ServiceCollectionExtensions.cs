﻿using Microsoft.Extensions.DependencyInjection;

namespace StratmanMedia.Logging.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStratmanMediaLogging(this IServiceCollection services, SMLoggerConfiguration configuration)
        {
            ServiceRegistrar.AddRequiredServices(services, configuration);

            return services;
        }
    }
}