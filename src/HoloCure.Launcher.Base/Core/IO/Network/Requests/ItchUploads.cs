// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HoloCure.Launcher.Base.Core.IO.Network.Requests;

public class ItchUploads
{
    public class ItchUpload
    {
        [JsonProperty("type")]
        public string Type { get; set; } = null!;

        [JsonProperty("filename")]
        public string FileName { get; set; } = null!;

        [JsonProperty("game_id")]
        public double GameId { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("storage")]
        public string Storage { get; set; } = null!;

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; } = null!;

        [JsonProperty("position")]
        public double Position { get; set; }

        [JsonProperty("size")]
        public double Size { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; } = null!;

        [JsonProperty("traits")]
        public Dictionary<string, JObject> Traits { get; set; } = null!;

        [JsonProperty("md5_hash")]
        public string Md5Hash { get; set; } = null!;
    }

    [JsonProperty("uploads")]
    public ItchUpload[] Uploads { get; set; } = null!;
}
