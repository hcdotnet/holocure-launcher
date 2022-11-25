using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using HoloCure.Launcher.Base.Core.Updating;
using HoloCure.Launcher.Base.Core.Updating.UpdateManagers;
using HoloCure.Launcher.Desktop.AddOns;
using HoloCure.Launcher.Desktop.Updater;
using HoloCure.Launcher.Game;
using osu.Framework;
using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace HoloCure.Launcher.Desktop;

public class LauncherGameDesktop : LauncherGame
{
    private const string launcher_icon = "launcher.ico";
    private const string launcher_external_update_provider = "LAUNCHER_EXTERNAL_UPDATE_PROVIDER";

    // private const int minimum_width = 1000, minimum_height = 600;
    // private const int default_width = 1280, default_height = 720;

    private const int window_width = 1300;
    private const int window_height = 800;

    protected override IDictionary<FrameworkSetting, object> GetFrameworkConfigDefaults()
    {
        IDictionary<FrameworkSetting, object> defaults = base.GetFrameworkConfigDefaults() ?? new Dictionary<FrameworkSetting, object>();
        defaults[FrameworkSetting.WindowedSize] = new Size(window_width, window_height);
        return defaults;
    }

    public override void SetHost(GameHost host)
    {
        base.SetHost(host);

        if (host.Window is not SDL2DesktopWindow sdlWindow) return;

        sdlWindow.Title = Name;
        sdlWindow.MinSize = sdlWindow.MaxSize = new Size(window_width, window_height);

        // osu does this, likely an edge case? idk... works on my machine
        Stream icoStream = typeof(LauncherGameDesktop).Assembly.GetManifestResourceStream(typeof(LauncherGameDesktop), launcher_icon)!;
        sdlWindow.SetIconFromStream(icoStream);
    }

    protected override IUpdateManager? CreateUpdateManager()
    {
        string? packageManaged = Environment.GetEnvironmentVariable(launcher_external_update_provider);

        // Use NoActionUpdateManager if the launcher is installed through a package manager or other external installation tool.
        if (!string.IsNullOrEmpty(packageManaged)) return new NoActionUpdateManager();

        switch (RuntimeInfo.OS)
        {
            // Windows uses Clowd.Squirrel (Squirrel.Windows) fork for applying delta patches and other updating techniques.
            case RuntimeInfo.Platform.Windows:
                Debug.Assert(OperatingSystem.IsWindows());
                return new SquirrelUpdateManager();

            case RuntimeInfo.Platform.Linux:
            case RuntimeInfo.Platform.macOS:
            case RuntimeInfo.Platform.iOS:
            case RuntimeInfo.Platform.Android:
            default:
                // Only Windows currently features convenient update management.
                // Non-Windows platforms will have to live with a notification and nothing more, for now.
                return new SimpleUpdateManager();
        }
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        LoadComponentAsync(new DRPComponent());
    }
}
