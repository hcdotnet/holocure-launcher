// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Game.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace HoloCure.Launcher.Game.Graphics.StartUp;

public class LoadingBox : CompositeDrawable
{
    private static readonly Colour4 background_color = new(0, 0, 0, 128);

    private Box background = null!;
    private SpriteText loadingSpriteText = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        TextFlowContainer test = new()
        {
            AutoSizeAxes = Axes.Both
        };

        test.AddParagraph("This is some text.", text => text.Font = LauncherFont.Default);

        InternalChildren = new Drawable[]
        {
            new LauncherContainer
            {
                Width = 300,
                Height = 200,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,

                Children = new Drawable[]
                {
                    test
                }
            }

            /*loadingSpriteText = new LauncherSpriteText
            {
                Text = "Test",
                UseFullGlyphHeight = false
            }*/
        };
    }
}