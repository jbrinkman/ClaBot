using Newtonsoft.Json;
using System;

namespace ClaBot.Models.Github
{
    public class PullRequest
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("head")]
        public Branch Head { get; set; }

        [JsonProperty("closed_at")]
        public DateTime? ClosedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("merged")]
        public bool Merged { get; set; }

        [JsonProperty("merge_commit_sha")]
        public string MergeCommitSha { get; set; }

        [JsonProperty("merged_at")]
        public DateTime? MergedAt { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("statuses_url")]
        public Uri StatusesUrl { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
