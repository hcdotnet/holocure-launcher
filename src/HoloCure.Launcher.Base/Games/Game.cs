using HoloCure.Launcher.Base.Rendering.Graphics.UserInterface.Games;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;

namespace HoloCure.Launcher.Base.Games;

/// <summary>
///     A game that may be launched by this launcher.
/// </summary>
public abstract class Game : CompositeDrawable
{
    public abstract LocalisableString GameTitle { get; }

    public abstract string GameTitlePath { get; }

    public abstract string GameIconPath { get; }

    public virtual GameListItem MakeListItem() => new(this);
}
