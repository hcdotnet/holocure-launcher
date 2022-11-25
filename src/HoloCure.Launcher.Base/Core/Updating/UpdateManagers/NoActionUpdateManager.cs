using HoloCure.Launcher.Base.Core.IO.Network.Requests;

namespace HoloCure.Launcher.Base.Core.Updating.UpdateManagers;

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
