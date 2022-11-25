// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Game.Rendering.Graphics.StartUp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace HoloCure.Launcher.Game.Rendering.Screens;

public class StartUpScreen : Screen
{
    private StartUpBackground startUpBackground = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        InternalChildren = new Drawable[]
        {
            startUpBackground = new StartUpBackground
            {
                RelativeSizeAxes = Axes.Both
            }
        };
    }
}
