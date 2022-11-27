using System.Collections.Generic;
using System.Drawing;
using System.IO;
using HoloCure.Launcher.Desktop.Components;
using HoloCure.Launcher.Game;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace HoloCure.Launcher.Desktop;

public class LauncherGameDesktop : LauncherGame
{
    private const string launcher_icon = "launcher.ico";

    // private const int minimum_width = 1000, minimum_height = 600;
    // private const int default_width = 1280, default_height = 720;

    private const int window_width = 1300;
    private const int window_height = 800;

    private DependencyContainer dependencies = null!;

    protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

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

    protected override void LoadComplete()
    {
        base.LoadComplete();

        LoadComponentAsync(new DRPComponent());
        LoadComponentAsync(new UpdaterComponent());
    }
}
