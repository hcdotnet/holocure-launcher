using System.Linq;
using System.Threading.Tasks;
using HoloCure.Launcher.Base.Core.IO.Network;
using HoloCure.Launcher.Base.Core.IO.Network.Requests;
using osu.Framework.Allocation;

namespace HoloCure.Launcher.Base.Core.Updating.UpdateManagers;

/// <summary>
///     An <see cref="DrawableUpdateManager"/> implementation which pulls version information from GitHub.
/// </summary>
public abstract class GitHubUpdateManager : DrawableUpdateManager
{
    private const string github_release_endpoint = "https://api.github.com/repos/steviegt6/holocure-launcher/releases/latest";

    protected virtual string GitHubReleaseEndpoint => github_release_endpoint;

    [Resolved]
    private LauncherBase.IBuildInfo buildInfo { get; set; } = null!;

    public override async Task<bool> PerformUpdateCheck()
    {
        try
        {
            LauncherJsonWebRequest<GitHubRelease> releases = new LauncherJsonWebRequest<GitHubRelease>(GitHubReleaseEndpoint);
            await releases.PerformAsync().ConfigureAwait(false);

            GitHubRelease latest = releases.ResponseObject;

            // ignore builds suffixes; TODO: fixme? release streams...
            string version = buildInfo.Version.Split('-').First();
            string latestTagName = latest.TagName.Split('-').First();

            return HandleVersionUpdateCheck(latestTagName, version, latest);
        }
        catch
        {
            // No failures should ever actually occur.
            return true;
        }
    }

    /// <summary>
    ///     Handles determining whether an update is present, and applying it if necessary.
    /// </summary>
    /// <param name="latestVersion">The latest resolved version.</param>
    /// <param name="currentVersion">The current version being ran.</param>
    /// <param name="release">The release package.</param>
    protected abstract bool HandleVersionUpdateCheck(string latestVersion, string currentVersion, GitHubRelease release);
}
