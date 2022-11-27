using osu.Framework.IO.Network;

namespace HoloCure.Launcher.Base.Core.IO.Network;

public class LauncherJsonWebRequest<T> : JsonWebRequest<T>
{
    public LauncherJsonWebRequest(string url)
        : base(url)
    {
    }

    protected override string UserAgent => LauncherBase.GAME_NAME;
}
