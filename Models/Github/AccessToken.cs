using System;
using Newtonsoft.Json;

namespace ClaBot.Models.Github
{
    public class AccessToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expires_at")]
        public DateTime? Expiration { get; set; }

    }
}
