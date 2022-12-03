// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Graphics.Containers;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace HoloCure.Launcher.Game.Tests.Visual.Containers;

[TestFixture]
public class TestSceneLauncherScrollContainer : LauncherTestScene
{
    [BackgroundDependencyLoader]
    private void load()
    {
        var scrollContainer = new LauncherScrollContainer
        {
            Width = 200,
            Height = 600,

            Origin = Anchor.Centre,
            Anchor = Anchor.Centre
        };

        scrollContainer.Add(new Box
        {
            Width = 50,
            Height = 50,
            Colour = Colour4.White
        });

        Add(new Box
        {
            RelativeSizeAxes = Axes.Both,
            Colour = Colour4.DarkGray
        });
        Add(scrollContainer);
    }
}
