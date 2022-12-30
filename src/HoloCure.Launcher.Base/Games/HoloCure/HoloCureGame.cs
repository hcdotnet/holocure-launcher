// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HoloCure.Launcher.Base.Core.IO.Network;
using HoloCure.Launcher.Base.Core.IO.Network.Requests;
using osu.Framework.Localisation;
using osu.Framework.Platform;

namespace HoloCure.Launcher.Base.Games.HoloCure;

public class HoloCureGame : Game
{
    private const string api_key = "l7McEiuy5JD9OxqKfiO6gnlT2Obv9rBUpIKGBloH";
    private const string download_key = "91872433";
    private const string itch_url = "https://api.itch.io";
    private const string game_id = "1692002";

    public override LocalisableString GameTitle => "HoloCure";

    public override string GameTitlePath => "Games/HoloCure/Title";

    public override string GameIconPath => "Games/HoloCure/Icon";

    public override async Task InstallOrPlayGameAsync(Action<GameAlert> onAlert, Storage storage)
    {
        // Eventually make paths configurable - settings and profiles...

        onAlert(GameAlert.CheckingInstallation);

        string hcDir = Path.Combine("Games", "HoloCure");
        string gamDir = Path.Combine(hcDir, "game");

        // If directory does not exist or no executable.
        // Executable checking will need to change once other platforms are supported.
        if (!storage.ExistsDirectory(hcDir) || !storage.GetFiles(gamDir).Any(x => x.EndsWith(".exe")))
        {
            onAlert(GameAlert.InstallationNotFoundInstallingGame);
            await installGame(storage, hcDir);
        }

        onAlert(GameAlert.InstallationFoundStartingGame);

        // See earlier comments for executable checking.
        string executable = storage.GetFiles(gamDir).First(x => x.EndsWith(".exe"));
        var proc = Process.Start(new ProcessStartInfo
        {
            FileName = storage.GetFullPath(executable),
            ErrorDialog = true,
            UseShellExecute = false,
            // Don't redirect STD I/O, causes issues with freezing. Fixes GH-24.
            // https://github.com/steviegt6/holocure-launcher/issues/24
            // RedirectStandardOutput = true,
            // RedirectStandardError = true,
            WorkingDirectory = storage.GetFullPath(hcDir)
        });
        onAlert(GameAlert.GameStarted);
        proc?.WaitForExit();
        onAlert(GameAlert.GameExited);
    }

    public override async Task UpdateGameAsync(Action<GameAlert> onAlert, Storage storage)
    {
        onAlert(GameAlert.CheckingForUpdates);

        (string updatedAt, double id) = await getLatestDownloadId();
        string hcDir = Path.Combine("Games", "HoloCure");
        string versionPath = Path.Combine(hcDir, "uat");
        string currVersion = storage.Exists(versionPath) ? await File.ReadAllTextAsync(storage.GetFullPath(versionPath)) : "";

        if (updatedAt == currVersion.Trim())
        {
            onAlert(GameAlert.NoUpdatesFound);
            return;
        }

        onAlert(GameAlert.UpdatingGame);
        await installGame(storage, hcDir);
        onAlert(GameAlert.GameUpdated);
    }

    private async Task installGame(Storage storage, string directory)
    {
        if (!Directory.Exists(storage.GetFullPath(directory))) Directory.CreateDirectory(directory);

        var dsReq = new LauncherJsonWebRequest<ItchDownloadSessions>($"{itch_url}/games/{game_id}/download-sessions")
        {
            Method = HttpMethod.Post
        };
        dsReq.AddHeader("Authorization", $"Bearer {api_key}");
        await dsReq.PerformAsync().ConfigureAwait(false);
        string uuid = dsReq.ResponseObject.Uuid;

        string dir = storage.GetFullPath(directory);
        string zip = Path.Combine(dir, "HoloCure.zip");
        (string updatedAt, double id) = await getLatestDownloadId();
        await downloadGame(id, uuid, zip);
        extractZip(zip, Path.Combine(dir, "game"));
        await writeUpdatedAtFile(Path.Combine(dir, "uat"), updatedAt);
    }

    private async Task<(string updatedAt, double id)> getLatestDownloadId()
    {
        var request = new LauncherJsonWebRequest<ItchUploads>($"{itch_url}/games/{game_id}/uploads");
        request.AddHeader("Authorization", $"Bearer {api_key}");
        await request.PerformAsync().ConfigureAwait(false);
        var obj = request.ResponseObject.Uploads[0];
        return (obj.UpdatedAt, obj.Id);
    }

    private async Task downloadGame(double downloadId, string uuid, string path)
    {
        string downloadUrl = $"{itch_url}/uploads/{downloadId}/download?api_key={api_key}&uuid={uuid}&download_key_id={download_key}";
        var fileReq = new LauncherFileWebRequest(path, downloadUrl);
        await fileReq.PerformAsync().ConfigureAwait(false);
    }

    private void extractZip(string file, string directoryTo)
    {
        Directory.CreateDirectory(directoryTo);
        ZipFile.ExtractToDirectory(file, directoryTo, true);
        File.Delete(file);
    }

    private async Task writeUpdatedAtFile(string filePath, string updatedAt)
    {
        using StreamWriter w = File.CreateText(filePath);
        await w.WriteLineAsync(updatedAt);
    }
}
