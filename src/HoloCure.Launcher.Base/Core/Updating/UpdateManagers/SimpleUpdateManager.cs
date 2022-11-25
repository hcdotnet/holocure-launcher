using HoloCure.Launcher.Base.Core.IO.Network.Requests;

namespace HoloCure.Launcher.Base.Core.Updating.UpdateManagers;

/// <summary>
///     An <see cref="DrawableUpdateManager"/> implementation which notifies the user if an update is available. <br />
///     The app may be updated at the user's discretion. <br />
///     Updates are not automatically applied, the user is just directed to an external URL for installation.
/// </summary>
public class SimpleUpdateManager : GitHubUpdateManager
{
    protected override bool HandleVersionUpdateCheck(string latestVersion, string currentVersion, GitHubRelease release)
    {
        if (latestVersion != currentVersion)
        {
            // TODO: notification or pop-up, should open an external url
            // TODO: osu's getBestUrl logic, which we may not bother with...: https://github.com/ppy/osu/blob/4bc26dbb487241e2bbae73751dbe9e93a4e427da/osu.Game/Updater/SimpleUpdateManager.cs#L77
            return true;
        }

        return false;
    }
}
