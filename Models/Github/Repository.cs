using Newtonsoft.Json;

namespace ClaBot.Models.Github
{
    public class Repository
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public string Owner
        {
            get { return FullName.Split('/')[0]; }
        }

    }
}
