using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Settings.Extensions.Configuration
{
    public static class SettingsExtension
    {
        public static SettingConfigurationContext<T> AddSettings<T>(this IServiceCollection services, IConfiguration configuration, Func<OptionsBuilder<T>, OptionsBuilder<T>> validation = null, string name = null)
            where T : class, new()
        {
            var builder = services.AddOptions<T>().Bind(configuration.GetSection(name ?? typeof(T).Name));

            if (validation != null)
            {
                builder = validation(builder);
            }

            services.AddTransient(s => s.GetRequiredService<IOptions<T>>().Value);

            return new SettingConfigurationContext<T>(services, configuration);
        }

        public static SettingConfigurationContext<T> AddSettings<T>(this ISettingConfigurationContext context, Func<OptionsBuilder<T>, OptionsBuilder<T>> validation = null, string name = null)
            where T : class, new()
        {
            (IServiceCollection services, IConfiguration configuration) = (context.Services, context.Configuration);
            var builder = services.AddOptions<T>().Bind(configuration.GetSection(name ?? typeof(T).Name));

            if (validation != null)
            {
                builder = validation(builder);
            }

            services.AddTransient(s => s.GetRequiredService<IOptions<T>>().Value);

            return new SettingConfigurationContext<T>(context.Services, context.Configuration);
        }
    }
}