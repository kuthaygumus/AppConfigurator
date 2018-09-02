using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using AppConfigurator.Library.Interfaces;
using AppConfigurator.Library.Ioc;

namespace AppConfigurator.Library
{
    public class ConfigManager : IConfiguration
    {
        public string ApplicationName { get; private set; }
        public int RefreshInterval { get; private set; }

        private ConcurrentDictionary<string, IApplicationModel> ConfigCache { get; set; }
        private Timer RefrestStoreTimer { get; set; }
        private bool isInitialized = false;
        private Dictionary<Enums.ApplicationValueType, Type> typeMap = new Dictionary<Enums.ApplicationValueType, Type>
        {
            {Enums.ApplicationValueType.Int, typeof(int) },
            {Enums.ApplicationValueType.String, typeof(String) },
            {Enums.ApplicationValueType.Bool, typeof(Boolean) }
        };

        private IApplication _application;

        public ConfigManager(string appName, int refreshIntervalMillisecond, IApplication application)
        {
            ConfigCache = new ConcurrentDictionary<string, IApplicationModel>();
            ApplicationName = appName;
            RefreshInterval = refreshIntervalMillisecond;
            RefrestStoreTimer = new Timer(RefreshConfigCache, ConfigCache, TimeSpan.Zero, TimeSpan.FromMilliseconds(RefreshInterval));
            _application = application;
        }

        public T GetValue<T>(string key)
        {
            if (!isInitialized)
                WaitUntilInitialized(TimeSpan.FromSeconds(10));

            if (ConfigCache.TryGetValue(key, out var value))
            {
                if (typeof(T) != typeMap[value.Type])
                    throw new InvalidCastException($"{value.Name} is not type of {value.Type.ToString()}");

                if (value.Type == Enums.ApplicationValueType.Int) //Invalid cast exception long to int düzenlemesi
                    value.Value = Convert.ToInt32(value.Value);
                return (T)value.Value;
            }

            return default(T);
        }

        #region Private Methods

        private void RefreshConfigCache(object state)
        {
            var cache = state as ConcurrentDictionary<string, IApplicationModel>;
            var store = Resolver.Resolve<IApplication>();

            if (!store.IsConnected())
                return;

            var appStore = store.GetAll(ApplicationName);
            foreach (var item in appStore)
            {
                if (item.IsActive)
                    cache.AddOrUpdate(item.Name, item, (key, prev) => item);
            }
            isInitialized = true;
        }

        private void WaitUntilInitialized(TimeSpan timeout)
        {
            var startTime = DateTime.UtcNow.Ticks;
            while (!isInitialized)
            {
                if (startTime + timeout.Ticks < DateTime.UtcNow.Ticks)
                    break;
            }
        }

        #endregion
    }
}