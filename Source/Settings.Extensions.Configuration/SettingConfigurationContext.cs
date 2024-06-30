using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Settings.Extensions.Configuration
{
    public class SettingConfigurationContext<T> : ISettingConfigurationContext
    {

        public SettingConfigurationContext(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }

        public IServiceCollection Services { get; }
        public IConfiguration Configuration { get; }

        public static implicit operator (IServiceCollection services, IConfiguration configuration)(SettingConfigurationContext<T> context)
        {
            return (context.Services, context.Configuration);
        }

    }
}