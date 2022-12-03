// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using HoloCure.Launcher.Base.Graphics.UI.Screens;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace HoloCure.Launcher.Game.Tests.Visual.Screens;

[TestFixture]
public class StartUpScreenSequenceTest : LauncherTestScene
{
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

        //AddDurationWaitStep("wait for reveal", StartUpScreen.DEFAULT_REVEAL_DURATION);
        AddWaitStep("wait for reveal", (int)Math.Round(StartUpScreen.DEFAULT_REVEAL_DURATION / TimePerAction, MidpointRounding.AwayFromZero));
        AddAssert("assert sequence not completed", () => !sequenceCompleted);

        //AddDurationWaitStep("wait for hide", StartUpScreen.DEFAULT_HIDE_DURATION);
        AddWaitStep("wait for hide", (int)Math.Round((StartUpScreen.DEFAULT_HIDE_TIME_UNTIL_RUN - StartUpScreen.DEFAULT_REVEAL_DURATION) / TimePerAction, MidpointRounding.AwayFromZero) + 1);
        AddAssert("assert sequence completed", () => sequenceCompleted);
    }
}
