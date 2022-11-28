// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Collections.Generic;
using System.Linq;
using HoloCure.Launcher.Base.Games.HoloCure;
using osu.Framework.Bindables;

namespace HoloCure.Launcher.Base.Games;

public class GameProvider
{
    public virtual Lazy<List<Game>> Games { get; }

    public virtual Bindable<Game?> SelectedGame { get; } = new();

    public GameProvider()
    {
        Games = new Lazy<List<Game>>(() => GetGames().ToList());
    }

    protected virtual IEnumerable<Game> GetGames()
    {
        yield return new HoloCureGame();
    }
}
