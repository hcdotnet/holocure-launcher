using osu.Framework.IO.Network;

namespace HoloCure.Launcher.Base.Core.IO.Network;

public class LauncherJsonWebRequest<T> : JsonWebRequest<T>
{
    protected override string UserAgent => LauncherBase.GAME_NAME;

    public LauncherJsonWebRequest(string url)
        : base(url)
    {
    }
}
