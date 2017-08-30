using Newtonsoft.Json;

namespace ClaBot.Models.Github
{
    public class Person
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

    }

}
