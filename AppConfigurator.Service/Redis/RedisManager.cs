using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppConfigurator.Library.Interfaces;
using AppConfigurator.Library.Utils;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AppConfigurator.Service.Redis
{
    public class RedisManager : IRedisManager
    {
        public IDatabase Database { get; private set; }
        public IConnectionMultiplexer Multiplexer { get; private set; }

        public RedisManager(string connection)
        {
            Multiplexer = ConnectionMultiplexer.Connect(connection);
            Database = Multiplexer.GetDatabase();
        }

        public async Task<IList<IApplicationModel>> GetAllAsync()
        {
            var storeData = new List<IApplicationModel>();
            var endpoints = Multiplexer.GetEndPoints();
            var tasks = new List<Task<IApplicationModel>>();
            foreach (var endpoint in endpoints)
            {
                var server = Multiplexer.GetServer(endpoint);
                var keys = server.Keys();
                foreach (var key in keys)
                {
                    tasks.Add(Database.StringGetAsync(key)
                        .ContinueWith(json => JsonConvert.DeserializeObject<IApplicationModel>(json.Result, JsonConfiguration.Settings)));
                }
            }

            return (await Task.WhenAll(tasks)).ToList();
        }

        public async Task<int> AddOrUpdateAsync(IList<IApplicationModel> models)
        {
            var redisItems = models.Select(
                model => new {
                    RedisKey = $"{model.ApplicationName}:{model.Name}",
                    Json = JsonConvert.SerializeObject(model)
                });

            var tasks = redisItems.Select(redisItem =>
                Database.StringSetAsync(redisItem.RedisKey, redisItem.Json));

            return (await Task.WhenAll(tasks))
                .Where(s => s).Count();
        }

        public async Task<bool> AddOrUpdateAsync(IApplicationModel model)
        {
            var redisItem = new
            {
                RedisKey = $"{model.ApplicationName}:{model.Name}",
                Json = JsonConvert.SerializeObject(model)
            };

            return await Database.StringSetAsync(redisItem.RedisKey, redisItem.Json);
        }

        public async Task<IApplicationModel> GetItemAsync(string applicationName, string configurationName)
        {
            var redisKey = $"{applicationName}:{configurationName}";
            var json = await Database.StringGetAsync(redisKey);
            return JsonConvert.DeserializeObject<IApplicationModel>(json, JsonConfiguration.Settings);
        }

        public Task<bool> DeleteAsync(string key)
        {
            return Database.KeyDeleteAsync(key);
        }
    }
}