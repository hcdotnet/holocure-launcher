// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Games.HoloCure;
using HoloCure.Launcher.Base.Graphics.Containers;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace HoloCure.Launcher.Game.Tests.Visual.UserInterface;

[TestFixture]
public class TestSceneBasicGameListItemInList : LauncherTestScene
{
    [BackgroundDependencyLoader]
    private void load()
    {
        var flowContainer = new FillFlowContainer
        {
            Direction = FillDirection.Vertical,
            AutoSizeAxes = Axes.Y,
            RelativeSizeAxes = Axes.X,
            Spacing = new Vector2(0, 2f),
        };

        var scrollContainer = new LauncherScrollContainer
        {
            Width = 200f,
            Height = 600f,

            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,

            Child = flowContainer
        };

        Add(scrollContainer);

        AddRepeatStep("add holocuregame gamelistitem", () => flowContainer.Add(new HoloCureGame().MakeListItem()), 10);
    }
}
