// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Graphics.UI.Screens;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace HoloCure.Launcher.Game.Tests.Visual.Screens;

[TestFixture]
public class StartUpScreenSequenceTest : LauncherTestScene
{
    private double timePerAction = 250D;

    protected override double TimePerAction => timePerAction;

    [BackgroundDependencyLoader]
    private void load()
    {
        var stack = new ScreenStack
        {
            RelativeSizeAxes = Axes.Both
        };

        bool sequenceCompleted = false;
        var screen = new StartUpScreen();

        Add(stack);

        AddStep("push startup screen", () => stack.Push(screen));
        AddStep("play default sequence", () => screen.RunDefaultIntroSequence(() => sequenceCompleted = true));
        AddUntilStep("wait until sequence completed", () => sequenceCompleted);
    }
}
