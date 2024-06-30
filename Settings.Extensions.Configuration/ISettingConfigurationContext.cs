using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Settings.Extensions.Configuration
{
    public interface ISettingConfigurationContext
    {
        IServiceCollection Services { get; }
        IConfiguration Configuration { get; }

    }
}