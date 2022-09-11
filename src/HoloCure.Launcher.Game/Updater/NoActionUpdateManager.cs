using HoloCure.Launcher.Game.IO.Network.Requests;

namespace HoloCure.Launcher.Game.Updater
{
    public class NoActionUpdateManager : GitHubUpdateManager
    {
        protected override bool HandleVersionUpdateCheck(string latestVersion, string currentVersion, GitHubRelease release)
        {
            if (latestVersion != currentVersion)
            {
                // TODO: notification or pop-up
                return true;
            }

            return false;
        }
    }
}
