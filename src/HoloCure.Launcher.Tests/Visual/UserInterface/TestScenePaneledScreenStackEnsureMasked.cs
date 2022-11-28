// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Rendering.Graphics.Screens;
using HoloCure.Launcher.Base.Rendering.Graphics.UserInterface;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace HoloCure.Launcher.Game.Tests.Visual.UserInterface;

[TestFixture]
public class TestScenePaneledScreenStackEnsureMasked : LauncherTestScene
{
    private class CornerBoxScreen : LauncherScreen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            static Box makeBox() =>
                new()
                {
                    Width = 80,
                    Height = 80,

                    Origin = Anchor.Centre,

                    Colour = Colour4.Red,
                };

            InternalChildren = new Drawable[]
            {
                makeBox().With(x => x.Anchor = Anchor.TopLeft),
                makeBox().With(x => x.Anchor = Anchor.TopRight),
                makeBox().With(x => x.Anchor = Anchor.BottomLeft),
                makeBox().With(x => x.Anchor = Anchor.BottomRight),
            };
        }
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        var panel = new PaneledScreenStack
        {
            RelativeSizeAxes = Axes.Both,

            Origin = Anchor.Centre,
            Anchor = Anchor.Centre
        };

        void setPadding(float padding)
        {
            panel.RelativeSizeAxes = Axes.None;
            panel.Width = panel.Parent.BoundingBox.Width - padding;
            panel.Height = panel.Parent.BoundingBox.Height - padding;
        }

        Add(panel);

        AddStep("push corner boxes screen", () => panel.Stack.Push(new CornerBoxScreen()));
        AddStep("set padding to 0 (removes relative sizing)", () => setPadding(0f));
        AddSliderStep("outer screen padding", 0f, 500f, 0f, setPadding);
    }
}
