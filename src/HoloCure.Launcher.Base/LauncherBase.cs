using System;
using HoloCure.Launcher.Base.Rendering.Graphics;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;

namespace HoloCure.Launcher.Base;

public abstract partial class LauncherBase : Game
{
    public const string GAME_NAME = "HoloCure.Launcher";

    protected LauncherBase()
    {
        Name = GAME_NAME;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        if (dependencies is null) throw new InvalidOperationException("Dependencies have not been loaded yet.");

        dependencies.CacheAs<Game>(this);
        dependencies.CacheAs(this);
        dependencies.CacheAs(BuildInfo);
        dependencies.Cache(new LauncherTheme());

        InitializeStores();

        base.Content.Add(new Container
        {
            RelativeSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                content = new TooltipContainer
                {
                    RelativeSizeAxes = Axes.Both
                }
            }
        });
    }
}
