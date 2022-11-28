// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.IO.Network;

namespace HoloCure.Launcher.Base.Core.IO.Network;

public class LauncherFileWebRequest : FileWebRequest
{
    protected override string UserAgent => LauncherBase.GAME_NAME;

    public LauncherFileWebRequest(string filename, string url)
        : base(filename, url)
    {
    }
}
