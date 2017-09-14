using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClaBot.Models
{
    class CacheItem
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("item")]
        public object Item { get; set; }
        [JsonProperty("ttl")]
        public int? TimeToLive { get; set; }
    }
}
