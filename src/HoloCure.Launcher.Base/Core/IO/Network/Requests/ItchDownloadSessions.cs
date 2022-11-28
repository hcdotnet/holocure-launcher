// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using Newtonsoft.Json;

namespace HoloCure.Launcher.Base.Core.IO.Network.Requests;

public class ItchDownloadSessions
{
    [JsonProperty("uuid")]
    public string Uuid { get; set; } = null!;
}
