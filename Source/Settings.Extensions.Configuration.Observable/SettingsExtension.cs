using System.Reactive.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Settings.Extensions.Configuration.Observable
{
    public static class SettingsExtension
    {
        public static SettingConfigurationContext<T> AddObservableSettings<T>(this SettingConfigurationContext<T> context, Func<IObservable<T>, IObservable<T>> transform = null)
            where T : class, IEquatable<T>
        {
            var transformObservable = transform ?? (o => o.Throttle(TimeSpan.FromSeconds(10)).DistinctUntilChanged());
            context.Services.AddSingleton<OptionsObservable<T>>(p => new OptionsObservable<T>(p.GetRequiredService<IOptionsMonitor<T>>()));
            context.Services.AddSingleton<IObservable<T>>(p => transformObservable(p.GetRequiredService<OptionsObservable<T>>()));
            return context;
        }
    }
}