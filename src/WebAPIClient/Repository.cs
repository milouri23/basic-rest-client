using System;
using System.Text.Json.Serialization;

namespace WebAPIClient
{
    public class Repository
    {
        public string Name { get; init; }

        public string Description { get; init; }

        [JsonPropertyName("html_url")]
        public Uri GitHubHomeUrl { get; init; }

        public Uri Homepage { get; init; }

        public int Watchers { get; init; }

        [JsonPropertyName("pushed_at")]
        public DateTime LastPushUtc { get; init; }

        public DateTime LastPush => LastPushUtc.ToLocalTime();
    }
}
