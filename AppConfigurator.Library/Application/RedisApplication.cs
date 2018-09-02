using System;
using System.Collections.Generic;
using System.Linq;
using AppConfigurator.Library.Interfaces;
using AppConfigurator.Library.Utils;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AppConfigurator.Library.Application
{
    public class RedisApplication : IApplication
    {
        public virtual IConnectionMultiplexer RedisMultiplexer { get; private set; }
        public IDatabase Database { get; }

        public RedisApplication(string connection)
        {
            RedisMultiplexer =  ConnectionMultiplexer.Connect(connection);
            Database =  RedisMultiplexer.GetDatabase();
        }

        public bool IsConnected()
        {
            return RedisMultiplexer.IsConnected;
        }

        public IList<IApplicationModel> GetAll(string applicationName)
        {
            try
            {
                var storeData = new List<IApplicationModel>();
                var endpoints = RedisMultiplexer.GetEndPoints();
                var results = new List<string>();
                foreach (var endpoint in endpoints)
                {
                    var server = RedisMultiplexer.GetServer(endpoint);
                    results.AddRange(server.Keys(pattern: $"{applicationName}:*").Select(key => Database.StringGet(key)).Select(dummy => (string) dummy));
                }
                if (results.Any())
                {
                    var json = $"[{results?.Aggregate((prev, next) => $"{prev}, {next}")}]";
                    return JsonConvert.DeserializeObject<IList<IApplicationModel>>(json, JsonConfiguration.Settings);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<IApplicationModel>();
        }
    }
}