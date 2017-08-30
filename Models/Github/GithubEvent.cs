using Newtonsoft.Json;

namespace ClaBot.Models.Github
{
    public class GithubEvent
    {
        [JsonProperty("number")]
        public double Number { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("pull_request")]
        public PullRequest PullRequest { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }
    }
}
