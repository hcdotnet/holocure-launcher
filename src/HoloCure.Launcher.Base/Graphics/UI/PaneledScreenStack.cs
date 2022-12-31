// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;

namespace HoloCure.Launcher.Base.Graphics.UI;

public class PaneledScreenStack : CompositeDrawable
{
    public ScreenStack Stack { get; protected set; }

    [BackgroundDependencyLoader]
    private void load(LauncherTheme theme)
    {
        Masking = true;
        CornerRadius = 10f;

        InternalChildren = new Drawable[]
        {
            new Box
            {
                RelativeSizeAxes = Axes.Both,

                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,

                Colour = theme.ScreenStackBackgroundColor
            },
            Stack = new ScreenStack
            {
                RelativeSizeAxes = Axes.Both,

                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            }
        };
    }

    public void SetPadding(MarginPadding padding)
    {
        Padding = padding;
    }
}
