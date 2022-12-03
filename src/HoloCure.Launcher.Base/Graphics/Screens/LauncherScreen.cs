// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace HoloCure.Launcher.Base.Graphics.Screens;

public abstract class LauncherScreen : Screen
{
    protected LauncherScreen()
    {
        Alpha = 0f;
    }

    public override void OnResuming(ScreenTransitionEvent e)
    {
        base.OnResuming(e);

        this.FadeIn(500D, Easing.OutQuint);
    }

    public override void OnEntering(ScreenTransitionEvent e)
    {
        base.OnEntering(e);

        this.FadeIn(500D, Easing.OutQuint);
    }

    public override void OnSuspending(ScreenTransitionEvent e)
    {
        base.OnSuspending(e);

        this.FadeOut(500D, Easing.OutQuint);
    }

    public override bool OnExiting(ScreenExitEvent e)
    {
        this.FadeOut(500D, Easing.OutQuint);

        return base.OnExiting(e);
    }
}
