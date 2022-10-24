using HoloCure.Launcher.Core;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;

namespace HoloCure.Launcher.Game;

public partial class LauncherGameBase : CoreGame
{
    /// <summary>
    ///     The <see cref="Edges"/> that the game should be drawn over at a top level. <br />
    ///     Defaults to <see cref="Edges.None"/>.
    /// </summary>
    public virtual Edges SafeAreaOverrideEdges => Edges.None;

    protected override Container<Drawable> Content => content;

    private Container content = null!;

    protected LauncherGameBase()
    {
        Name = GAME_NAME;
        StoreProvider = new LauncherStoreProvider(LoadComponent);
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        dependencies.CacheAs(this);

        base.Content.Add(new SafeAreaContainer
        {
            SafeAreaOverrideEdges = SafeAreaOverrideEdges,
            RelativeSizeAxes = Axes.Both,
            Child = CreateScalingContainer()
               .WithChildren(new Drawable[]
                {
                    content = new TooltipContainer
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                })
        });
    }

    protected virtual Container CreateScalingContainer() => new DrawSizePreservingFillContainer();
}
