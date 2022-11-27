// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Localisation;

namespace HoloCure.Launcher.Base.Rendering.Graphics.Containers;

public class LauncherClickableContainer : ClickableContainer, IHasTooltip
{
    private readonly Container content = new() { RelativeSizeAxes = Axes.Both };

    protected override Container<Drawable> Content => content;

    public virtual LocalisableString TooltipText { get; set; }

    [BackgroundDependencyLoader]
    private void load()
    {
        if (AutoSizeAxes != Axes.None)
        {
            content.RelativeSizeAxes = (Axes.Both & ~AutoSizeAxes);
            content.AutoSizeAxes = AutoSizeAxes;
        }

        InternalChild = content;
    }
}
