using System.Collections.Generic;
using Newtonsoft.Json;

namespace HoloCure.Launcher.Game.IO.Network.Requests;

public class GitHubRelease
{
    public class GitHubAsset
    {
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("browser_download_url")]
        public string BrowserDownloadUrl { get; set; } = null!;
    }

    [JsonProperty("html_url")]
    public string HtmlUrl { get; set; } = null!;

    [JsonProperty("tag_name")]
    public string TagName { get; set; } = null!;

    [JsonProperty("assets")]
    public List<GitHubAsset> Assets { get; set; } = null!;
}