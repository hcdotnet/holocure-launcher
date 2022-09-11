using HoloCure.Launcher.Game.Updater;

namespace HoloCure.Launcher.Game.Tests
{
    public class LauncherGameTest : LauncherGame
    {
        protected override IUpdateManager? CreateUpdateManager() => null;
    }
}
