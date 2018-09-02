using AppConfigurator.Library;
using AppConfigurator.Library.Application;
using AppConfigurator.Library.Interfaces;
using AppConfigurator.Library.Ioc;
using AppConfigurator.Service.Redis;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AppConfigurator.Test
{
    public class AppConfigurationTest
    {
        private IApplication Application { get; set; }
        public IRedisManager RedisManager { get; set; }
        private IServiceCollection _serviceCollection;

        public AppConfigurationTest()
        {
            Init();
            RedisManager = Resolver.Resolve<IRedisManager>();
            Application = Resolver.Resolve<IApplication>();
        }

        #region Initializing Runtime Values

        private void Init()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddScoped<IApplicationModel, ApplicationModel>();
            _serviceCollection.AddSingleton<IRedisManager>(new RedisManager("localhost:6379"));
            _serviceCollection.AddSingleton<IApplication>(new RedisApplication("localhost:6379"));

            ServiceLocator.Instance = _serviceCollection.BuildServiceProvider();
        }

        #endregion

        [Theory]
        [InlineData("service-a", 1000)]
        public void MustInitializeConfiguration_WhenParametersGiven(string appName, int refreshInterval)
        {
            var configuration = new ConfigManager(appName, refreshInterval, Application);
            var siteName = configuration.GetValue<string>("SiteName");
            var maxItem = configuration.GetValue<int>("MaxItemCount");

            Assert.Equal("kuthaygumus.com", siteName);
            Assert.Equal(0, maxItem);
        }

        [Theory]
        [InlineData("service-a", 1000)]
        public void MustConfigurationWork_WithOldValues_WhenConnectionClosed(string appName, int refreshInterval)
        {
            var configuration = new ConfigManager(appName, refreshInterval, Application);
            var siteName = configuration.GetValue<string>("SiteName");
            var maxItem = configuration.GetValue<int>("MaxItemCount");

            Assert.NotNull(siteName);
            Assert.NotEqual(default(int), 50);

            var siteName2 = configuration.GetValue<string>("SiteName");
            var maxItem2 = configuration.GetValue<int>("MaxItemCount");

            Assert.Equal(siteName, siteName2);
            Assert.Equal(maxItem, maxItem2);
        }
    }
}