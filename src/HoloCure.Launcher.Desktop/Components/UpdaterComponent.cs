// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoloCure.Launcher.Base;
using HoloCure.Launcher.Base.Core.IO.Network;
using HoloCure.Launcher.Base.Core.IO.Network.Requests;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace HoloCure.Launcher.Desktop.Components;

internal class UpdaterComponent : Component
{
    private enum Buttons
    {
        Yes,
        No
    }

    private const string launcher_external_update_provider = "LAUNCHER_EXTERNAL_UPDATE_PROVIDER";
    private const string github_releases_endpoint = "https://api.github.com/repos/steviegt6/holocure-launcher/releases";

    [Resolved]
    private LauncherBase.IBuildInfo buildInfo { get; set; } = null!;

    protected override void LoadComplete()
    {
        base.LoadComplete();

        // Check for updates immediately.
        Schedule(() => Task.Run(checkForUpdatesAsync));
    }

    private async Task checkForUpdatesAsync()
    {
        var canUpdate = canCheckForUpdates(buildInfo);
        if (!canUpdate) return;

        var releases = await getReleases(buildInfo);
        if (releases.Count == 0) return;

        var latest = releases.FirstOrDefault(x => x.version > buildInfo.AssemblyVersion);
        if (latest == default) return;

        var btn = spawnUpdateNotification(buildInfo.AssemblyVersion.ToString(), latest.version.ToString(), latest.url);

        switch (btn)
        {
            case Buttons.Yes:
                break;

            case Buttons.No:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static bool canCheckForUpdates(LauncherBase.IBuildInfo buildInfo)
    {
        string? pkgMan = Environment.GetEnvironmentVariable(launcher_external_update_provider);

        if (pkgMan is not null)
        {
            Logger.Log($"{launcher_external_update_provider}: {pkgMan}", LoggingTarget.Runtime, LogLevel.Debug);
            return false;
        }

        Logger.Log($"{nameof(buildInfo.IsDeployedBuild)}: {buildInfo.IsDeployedBuild}.", LoggingTarget.Runtime, LogLevel.Debug);
        return buildInfo.IsDeployedBuild;
    }

    private static async Task<List<(Version version, string url)>> getReleases(LauncherBase.IBuildInfo buildInfo)
    {
        try
        {
            var request = new LauncherJsonWebRequest<GitHubRelease[]>(github_releases_endpoint);
            await request.PerformAsync().ConfigureAwait(false);

            var releases = request.ResponseObject.Select(release =>
            {
                var split = release.TagName.Split('-', '2');
                var version = split[0];
                var channel = split.Length > 1 ? split[1] : "";

                return (version: new Version(version), channel, url: release.HtmlUrl);
            });

            return releases.Where(x => x.channel == buildInfo.ReleaseChannel).Select(x => (x.version, x.url)).ToList();
        }
        catch
        {
            // Should never actually fail.
            return new List<(Version version, string url)>();
        }
    }

    private static Buttons spawnUpdateNotification(string currentVersion, string updateVersion, string downloadUrl)
    {
        // localization one day
        string message = $"An update for HoloCure.Launcher ({currentVersion} -> {updateVersion}) is available for download."
                         + "\nWould you like to be taken to the download page?";

        var yesButton = new SDL2.SDL.SDL_MessageBoxButtonData
        {
            flags = SDL2.SDL.SDL_MessageBoxButtonFlags.SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT,
            buttonid = (int)Buttons.Yes,
            text = "Yes"
        };

        var noButton = new SDL2.SDL.SDL_MessageBoxButtonData
        {
            buttonid = (int)Buttons.No,
            text = "No"
        };

        var box = new SDL2.SDL.SDL_MessageBoxData
        {
            flags = SDL2.SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION,
            message = message,
            title = "HoloCure.Launcher - Update Available",
            buttons = new[] { yesButton, noButton },
            numbuttons = 2
        };

        SDL2.SDL.SDL_ShowMessageBox(ref box, out int btn);
        return (Buttons)btn;
    }
}
