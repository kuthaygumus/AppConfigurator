using System;
using AppConfigurator.Library.Application;
using AppConfigurator.Library.Interfaces;
using Newtonsoft.Json;

namespace AppConfigurator.Library.Utils
{
    public class ApplicationModelConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<ApplicationModel>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IApplicationModel);
        }
    }
}