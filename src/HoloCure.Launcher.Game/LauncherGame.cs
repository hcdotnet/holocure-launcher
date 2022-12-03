using System;
using HoloCure.Launcher.Base;
using HoloCure.Launcher.Base.Graphics.UserInterface;
using osu.Framework.Allocation;

namespace HoloCure.Launcher.Game;

public abstract partial class LauncherGame : LauncherBase
{
    private LauncherOverlay? overlay = null;

    [BackgroundDependencyLoader]
    private void load()
    {
        if (dependencies is null) throw new InvalidOperationException("Dependencies have not been loaded yet.");

        dependencies.CacheAs(this);

        Child = overlay = new LauncherOverlay();
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        if (overlay is null) throw new InvalidOperationException("Attempted to complete load of LauncherGame before dependencies were loaded.");

        overlay.RunIntroSequence('v' + BuildInfo.AssemblyVersion.ToString() + '-' + BuildInfo.ReleaseChannel);

        // overlay.Panel.Stack.Push(new StartUpScreen());
    }
}
