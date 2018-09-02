using Newtonsoft.Json;

namespace AppConfigurator.Library.Utils
{
    public static class JsonConfiguration
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            Converters = new[]
            {
                new ApplicationModelConverter()
            }
        };
    }
}