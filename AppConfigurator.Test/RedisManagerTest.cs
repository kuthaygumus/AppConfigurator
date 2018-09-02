using AppConfigurator.Library.Application;
using AppConfigurator.Library.Interfaces;
using AppConfigurator.Library.Ioc;
using AppConfigurator.Service.Redis;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AppConfigurator.Test
{
    public class RedisManagerTest
    {
        private IApplication Application { get; set; }
        public IRedisManager RedisManager { get; set; }
        private IServiceCollection _serviceCollection;

        public RedisManagerTest()
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

        [Fact]
        public async void RedisMustSet_WhenGivenRightModel()
        {
            var model = new ApplicationModel
            {
                Id = 1,
                Name = "SiteName",
                Type = Library.Enums.ApplicationValueType.String,
                IsActive = true,
                ApplicationName = "service-d",
                Value = "unittest.kuthaygumus.com"
            };

            Assert.True(await RedisManager.AddOrUpdateAsync(model));
            var recordFromDb = await RedisManager.GetItemAsync(model.ApplicationName, model.Name);
            Assert.Equal(model.Value, recordFromDb.Value);
        }
    }
}
