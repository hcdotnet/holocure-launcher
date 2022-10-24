// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace HoloCure.Launcher.Game.Graphics.Containers;

/// <summary>
///     A <see cref="Container"/> with a background rendered using a <see cref="Box"/>.
/// </summary>
public class LauncherContainer : Container
{
    protected override Container<Drawable> Content => PanelContent;

    public Container<Drawable> PanelContent { get; private set; }

    public LauncherContainer()
    {
        Masking = true;
        CornerRadius = 3f;
        Margin = new MarginPadding(3f);

        AddRangeInternal(new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = LauncherColor.PANEL_COLOR
            },
            PanelContent = new Container
            {
                RelativeSizeAxes = Axes.Both
            }
        });
    }
}