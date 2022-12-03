using System;
using System.Threading.Tasks;
using HoloCure.Launcher.Base.Graphics.UserInterface.Games;
using HoloCure.Launcher.Base.Graphics.UserInterface.Screens;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osu.Framework.Platform;
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

    public abstract Task InstallOrPlayGameAsync(Action<GameAlert> onAlert, Storage storage);

    public abstract Task UpdateGameAsync(Action<GameAlert> onAlert, Storage storage);
}
