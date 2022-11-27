// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoloCure.Launcher.Base;
using HoloCure.Launcher.Base.Core.IO.Network;
using HoloCure.Launcher.Base.Core.IO.Network.Requests;
using HoloCure.Launcher.Base.Core.Updating;
using HoloCure.Launcher.Base.Core.Updating.UpdateManagers;
using osu.Framework.Localisation;

namespace HoloCure.Launcher.Desktop.Updater;

public class GitHubUpdateManager : UpdateManager
{
    private const string github_releases_endpoint = "https://api.github.com/repos/steviegt6/holocure-launcher/releases";

    public override async Task<(IEnumerable<UpdatePackage> packages, UpdateAvailability availability, LocalisableString message)> GetAvailableUpdatesAsync(LauncherBase.IBuildInfo buildInfo)
    {
        try
        {
            var releasesReq = new LauncherJsonWebRequest<GitHubRelease[]>(github_releases_endpoint);
            await releasesReq.PerformAsync().ConfigureAwait(false);

            var releases = releasesReq.ResponseObject.Select(release =>
            {
                var split = release.TagName.Split('-', 2);
                var version = split[0];
                var channel = split.Length > 1 ? split[1] : "";

                return new UpdatePackage(new Version(version), channel, release.HtmlUrl);
            });

            return (releases, UpdateAvailability.Checking, "Checking for updates...");
        }
        catch
        {
            // No failures should ever actually occur.
            return (new List<UpdatePackage>(), UpdateAvailability.UpdateUnavailableError, "Error occurred.");
        }
    }

    protected override async Task<(bool successful, LocalisableString message)> UpdateAsync(UpdatePackage update)
    {
        return await Task.FromResult((true, ""));
    }
}
