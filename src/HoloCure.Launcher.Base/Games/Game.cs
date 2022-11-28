using HoloCure.Launcher.Base.Rendering.Graphics.UserInterface.Games;
using HoloCure.Launcher.Base.Rendering.Graphics.UserInterface.Screens;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osu.Framework.Screens;

namespace HoloCure.Launcher.Base.Games;

/// <summary>
///     A game that may be launched by this launcher.
/// </summary>
public abstract class Game : CompositeDrawable
{
    public abstract LocalisableString GameTitle { get; }

    public abstract string GameTitlePath { get; }

    public abstract string GameIconPath { get; }

    protected Screen? GameScreen { get; set; }

    public virtual GameListItem MakeListItem() => new(this);

    public virtual Screen GetOrCreateScreen() => GameScreen ??= new GameLauncherScreen(this);
}
