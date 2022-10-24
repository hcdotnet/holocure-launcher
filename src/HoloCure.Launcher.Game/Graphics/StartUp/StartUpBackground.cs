// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace HoloCure.Launcher.Game.Graphics.StartUp;

public class StartUpBackground : CompositeDrawable
{
    private static readonly Colour4 top_gradient_blue = new(74, 190, 249, 255);
    private static readonly Colour4 bottom_gradient_blue = new(41, 146, 218, 255);

    public static ColourInfo BackgroundGradient => ColourInfo.GradientVertical(top_gradient_blue, bottom_gradient_blue);

    private Box backgroundBox = null!;

    // private BackgroundBars backgroundBars = null!;
    private LoadingBox loadingBox = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        InternalChildren = new Drawable[]
        {
            backgroundBox = new Box
            {
                Colour = BackgroundGradient,
                RelativeSizeAxes = Axes.Both,
            },

            /*backgroundBars = new BackgroundBars
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
            },*/

            loadingBox = new LoadingBox
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre
            }
        };
    }
}