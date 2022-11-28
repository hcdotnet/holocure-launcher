// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Rendering.Graphics.Containers;
using HoloCure.Launcher.Base.Rendering.Graphics.Screens;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace HoloCure.Launcher.Base.Rendering.Graphics.UserInterface.Screens;

public class MainScreen : LauncherScreen
{
    [BackgroundDependencyLoader]
    private void load()
    {
        var tempText = new LauncherTextFlowContainer
        {
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,

            TextAnchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Anchor = Anchor.Centre,
        };

        tempText.AddText("Placeholder text to see that this screen is present ", st => st.Font = FontUsage.Default);
        tempText.AddIcon(FontAwesome.Solid.Skull, st => st.Size = new Vector2(20f));

        AddInternal(tempText);
    }
}
