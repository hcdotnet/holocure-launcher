// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace HoloCure.Launcher.Base.Graphics.Containers;

public class DrawableLinkCompiler : LauncherHoverContainer
{
    public List<Drawable> Parts { get; }

    public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => Parts.Any(d => d.ReceivePositionalInputAt(screenSpacePos));

    public DrawableLinkCompiler(ITextPart part)
        : this(part.Drawables.OfType<SpriteText>())
    {
    }

    public DrawableLinkCompiler(IEnumerable<Drawable> parts)
    {
        Parts = parts.ToList();
    }

    [BackgroundDependencyLoader]
    private void load(LauncherTheme theme)
    {
        if (IdleColor == default) IdleColor = theme.LinkIdleColor;
    }

    protected override IEnumerable<Drawable> EffectTargets => Parts;
}
