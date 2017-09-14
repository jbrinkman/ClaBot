using Newtonsoft.Json;

namespace ClaBot.Models.Github
{
    public class Installation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

    }
}
