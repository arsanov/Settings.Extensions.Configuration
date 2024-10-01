using Microsoft.Extensions.Options;

namespace Settings.Extensions.Configuration.Observable
{
    public class OptionsObservable<T> : IObservable<T>
      where T : class, IEquatable<T>
    {
        private readonly IOptionsMonitor<T> optionsMonitor;

        public OptionsObservable(IOptionsMonitor<T> optionsMonitor)
        {
            this.optionsMonitor = optionsMonitor;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var actual = this.optionsMonitor.CurrentValue;
            observer.OnNext(actual);
            return this.optionsMonitor.OnChange((item, name) =>
            {
                observer.OnNext(item);
            });
        }
    }
}