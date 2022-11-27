using System;
using HoloCure.Launcher.Base;
using HoloCure.Launcher.Base.Rendering.Graphics.Screens;
using HoloCure.Launcher.Base.Rendering.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;

namespace HoloCure.Launcher.Game;

public abstract partial class LauncherGame : LauncherBase
{
    private ScreenStack? screenStack;

    [BackgroundDependencyLoader]
    private void load()
    {
        dependencies.CacheAs(this);

        var logo = new LauncherLogo();

        Children = new Drawable[]
        {
            // Background
            new Box
            {
                Colour = Colour4.Black,
                RelativeSizeAxes = Axes.Both,
            },
            screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both },
            logo
        };

        dependencies.CacheAs(logo);
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        if (screenStack is null) throw new InvalidOperationException("Attempted to complete load of LauncherGame before dependencies were loaded.");

        screenStack.Push(new StartUpScreen());
    }
}
