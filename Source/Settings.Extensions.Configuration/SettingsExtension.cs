using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Settings.Extensions.Configuration
{
    public static class SettingsExtension
    {
        public static (IServiceCollection, IConfiguration) AddSettings<T>(this IServiceCollection services, IConfiguration configuration, Func<OptionsBuilder<T>, OptionsBuilder<T>> validation = null, string name = null)
            where T : class, new()
        {
            var builder = services.AddOptions<T>().Bind(configuration.GetSection(name ?? typeof(T).Name));

            if (validation != null)
            {
                builder = validation(builder);
            }

            services.AddTransient(s => s.GetRequiredService<IOptions<T>>().Value);

            return (services, configuration);
        }

        public static (IServiceCollection, IConfiguration) AddSettings<T>(this (IServiceCollection services, IConfiguration configuration) pair, Func<OptionsBuilder<T>, OptionsBuilder<T>> validation = null, string name = null)
            where T : class, new()
        {
            var builder = pair.services.AddOptions<T>().Bind(pair.configuration.GetSection(name ?? typeof(T).Name));

            if (validation != null)
            {
                builder = validation(builder);
            }

            pair.services.AddTransient(s => s.GetRequiredService<IOptions<T>>().Value);

            return pair;
        }
    }
}