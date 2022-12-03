// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using HoloCure.Launcher.Base.Graphics.Sprites;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace HoloCure.Launcher.Base.Graphics.Containers;

public class LauncherTextFlowContainer : TextFlowContainer
{
    protected override SpriteText CreateSpriteText() => new LauncherSpriteText();

    public virtual ITextPart AddArbitraryDrawable(Drawable drawable) => AddPart(new TextPartManual(drawable.Yield()));

    public virtual ITextPart AddIcon(IconUsage icon, Action<SpriteText>? creationParameters = null) => AddText(icon.Icon.ToString(), creationParameters);
}
