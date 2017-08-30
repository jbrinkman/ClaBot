using Newtonsoft.Json;

namespace ClaBot.Models.Github
{

    public static class Convert
    {
        // Serialize/deserialize helpers

        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }

        public static string ToJson<T>(this T o) where T: IModel
        {
            return JsonConvert.SerializeObject(o, Settings);
        }

        // JsonConverter stuff

        static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
