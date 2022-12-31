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
        // FIX: We apply padding to this CompositeDrawable, so Masking
        // (specifically CornerRadius) does not apply to the visible portion.
        // This is resolved by embedding the children within a Container that
        // instead has our Masking and CornerRadius values.
        // Thankfully, RelativeSizeAxes are confined within the bounds of the
        // drawable, excluding the padding, meaning we don't have this same
        // issue with a child.
        InternalChild = new Container
        {
            Masking = true,
            CornerRadius = 10f,

            RelativeSizeAxes = Axes.Both,

            Children = new Drawable[]
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
            }
        };
    }

    public void SetPadding(MarginPadding padding)
    {
        Padding = padding;
    }
}
