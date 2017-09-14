using Newtonsoft.Json;

namespace ClaBot.Models.Github
{
    public class Status
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("creator")]
        public Person Creator { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
