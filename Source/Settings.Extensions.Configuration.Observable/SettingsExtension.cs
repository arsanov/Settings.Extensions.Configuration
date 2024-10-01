using System.Reactive.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Settings.Extensions.Configuration.Observable
{
    public static class SettingsExtension
    {
        private static IObservable<T> FromMonitor<T>(IServiceProvider provider)
            where T : class, IEquatable<T>
        {
            var monitor = provider.GetRequiredService<IOptionsMonitor<T>>();
            return (new OptionsObservable<T>(monitor)).DistinctUntilChanged();
        }

        public static SettingConfigurationContext<T> AddObservableSettings<T>(this SettingConfigurationContext<T> context)
            where T : class, IEquatable<T>
        {
            context.Services.AddSingleton<IObservable<T>>(FromMonitor<T>);
            return context;
        }
    }
}