// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Input.Events;

namespace HoloCure.Launcher.Base.Rendering.Graphics.Containers;

public class LauncherHoverContainer : LauncherClickableContainer
{
    private const float FADE_DURATION = 500f;

    private ColourInfo HoverColor { get; set; }

    private ColourInfo IdleColor { get; set; }

    private bool internalIsHovered { get; set; }

    protected virtual IEnumerable<Drawable> EffectTargets => new[] { Content };

    public LauncherHoverContainer()
    {
        Enabled.ValueChanged += e =>
        {
            if (!internalIsHovered) return;

            if (e.NewValue)
                fadeIn();
            else
                fadeOut();
        };
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        EffectTargets.ForEach(x => x.FadeColour(IdleColor));
    }

    protected override bool OnHover(HoverEvent e)
    {
        if (internalIsHovered) return false;

        internalIsHovered = true;

        if (!Enabled.Value) return false;

        fadeIn();

        return base.OnHover(e);
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        if (!internalIsHovered) return;

        internalIsHovered = false;
        fadeOut();

        base.OnHoverLost(e);
    }

    private void fadeIn() => EffectTargets.ForEach(d => d.FadeColour(HoverColor, FADE_DURATION, Easing.OutQuint));

    private void fadeOut() => EffectTargets.ForEach(d => d.FadeColour(IdleColor, FADE_DURATION, Easing.OutQuint));
}
