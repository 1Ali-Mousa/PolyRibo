using Microsoft.AspNetCore.Mvc;

namespace PolyRibo.API.Extension;

public static class ApiBehaviorConfigurationExtension
{
        public static IServiceCollection AddApiBehaviorConfigurationExtension(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            return services;
        }
}
