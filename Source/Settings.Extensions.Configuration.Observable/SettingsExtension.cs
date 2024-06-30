﻿using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Extensions.Configuration;
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

        public static (IServiceCollection, IConfiguration) AddObservableSettings<T>(this SettingConfigurationContext<T> context)
            where T : class, IEquatable<T>
        {
            services.AddSingleton<IObservable<T>>(FromMonitor<T>);
            return (services, configuration);
        }
    }
}