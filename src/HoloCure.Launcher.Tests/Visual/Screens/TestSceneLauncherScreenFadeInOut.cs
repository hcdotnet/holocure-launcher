// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Graphics.Screens;
using HoloCure.Launcher.Base.Graphics.UI;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace HoloCure.Launcher.Game.Tests.Visual.Screens;

[TestFixture]
public class TestScenePaneledLauncherScreenFadeInOut : LauncherTestScene
{
    private class BoxScreen : LauncherScreen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new Box
            {
                Width = 50,
                Height = 50,

                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,

                Colour = Colour4.White
            };
        }
    }

    private class CirceScreen : LauncherScreen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new Circle
            {
                Width = 50,
                Height = 50,

                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,

                Colour = Colour4.White
            };
        }
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        var panel = new PaneledScreenStack
        {
            RelativeSizeAxes = Axes.Both
        };

        Add(panel);

        AddStep("show box screen", () => panel.Stack.Push(new BoxScreen()));
        AddStep("show circle screen", () => panel.Stack.Push(new CirceScreen()));
        AddStep("return to prev. screen", () => panel.Stack.Exit());
    }
}
