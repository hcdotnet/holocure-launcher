// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace HoloCure.Launcher.Base.Rendering.Graphics.Containers;

public class DrawableLinkCompiler : LauncherHoverContainer
{
    private List<Drawable> parts { get; }

    public DrawableLinkCompiler(ITextPart part)
        : this(part.Drawables.OfType<SpriteText>())
    {
    }

    public DrawableLinkCompiler(IEnumerable<Drawable> parts)
    {
        this.parts = parts.ToList();
    }

    public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => parts.Any(d => d.ReceivePositionalInputAt(screenSpacePos));

    protected override IEnumerable<Drawable> EffectTargets => parts;
}
