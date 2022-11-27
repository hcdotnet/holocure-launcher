// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;

namespace HoloCure.Launcher.Base.Rendering.Graphics.Containers;

public class LauncherHoverContainer : LauncherClickableContainer
{
    protected const float FADE_DURATION = 500f;

    protected Colour4 HoverColor { get; set; }

    protected Colour4 IdleColor { get; set; } = Colour4.White;

    protected virtual IEnumerable<Drawable> EffectTargets => new[] { Content };

    private bool isHovered;

    public LauncherHoverContainer()
    {
        Enabled.ValueChanged += e =>
        {
            if (!isHovered) return;

            if (e.NewValue)
                fadeIn();
            else
                fadeOut();
        };
    }

    protected override bool OnHover(HoverEvent e)
    {
        if (isHovered) return false;

        isHovered = true;

        if (!Enabled.Value) return false;

        fadeIn();

        return base.OnHover(e);
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        if (!isHovered) return;

        isHovered = false;
        fadeOut();

        base.OnHoverLost(e);
    }

    private void fadeIn() => EffectTargets.ForEach(d => d.FadeColour(HoverColor, FADE_DURATION, Easing.OutQuint));

    private void fadeOut() => EffectTargets.ForEach(d => d.FadeColour(IdleColor, FADE_DURATION, Easing.OutQuint));
}
