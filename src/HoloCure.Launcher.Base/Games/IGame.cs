using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace HoloCure.Launcher.Base.Games;

/// <summary>
///     A game that may be launched by this launcher.
/// </summary>
public interface IGame
{
    SpriteText MakeMainWindowTitle();

    Drawable MakeSidebarIcon();

    Drawable MakeSidebarTitle();

    /// <summary>
    ///     Converts this object to a <see cref="Drawable"/>.
    /// </summary>
    Drawable AsDrawable();
}
