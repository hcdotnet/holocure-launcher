using osu.Framework.IO.Network;

namespace HoloCure.Launcher.Game.IO.Network
{
    public class LauncherJsonWebRequest<T> : JsonWebRequest<T>
    {
        public LauncherJsonWebRequest(string url)
            : base(url)
        {
        }

        protected override string UserAgent => LauncherGameBase.GAME_NAME;
    }
}
