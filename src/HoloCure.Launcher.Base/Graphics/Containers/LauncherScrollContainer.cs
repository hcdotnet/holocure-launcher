// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;

namespace HoloCure.Launcher.Base.Graphics.Containers;

public class LauncherScrollContainer : LauncherScrollContainer<Drawable>
{
}

public class LauncherScrollContainer<T> : ScrollContainer<T>
    where T : Drawable
{
    private const float scroll_bar_height = 10f;
    private const float scroll_bar_padding = 3f;

    protected override ScrollbarContainer CreateScrollbar(Direction direction) => new LauncherScrollbar(direction);

    protected class LauncherScrollbar : ScrollbarContainer
    {
        private Colour4 hoverColor;
        private Colour4 defaultColor;
        private Colour4 highlightColor;

        private readonly Box box;

        public LauncherScrollbar(Direction direction)
            : base(direction)
        {
            Blending = BlendingParameters.Additive;

            CornerRadius = 5;

            // Needs to be set initially for the ResizeTo to respect minimum size.
            Size = new Vector2(scroll_bar_height);

            Margin = new MarginPadding
            {
                Left = direction == Direction.Vertical ? scroll_bar_padding : 0,
                Right = direction == Direction.Vertical ? scroll_bar_padding : 0,
                Top = direction == Direction.Horizontal ? scroll_bar_padding : 0,
                Bottom = direction == Direction.Horizontal ? scroll_bar_padding : 0,
            };

            Masking = true;
            Child = box = new Box { RelativeSizeAxes = Axes.Both };
        }

        [BackgroundDependencyLoader]
        private void load(LauncherTheme theme)
        {
            Colour = defaultColor = Colour4.FromHex("888");
            hoverColor = Colour4.FromHex("FFF");
            highlightColor = theme.LogoYellowColor;
        }

        public override void ResizeTo(float val, int duration = 0, Easing easing = Easing.None)
        {
            var size = new Vector2(scroll_bar_height)
            {
                [(int)ScrollDirection] = val
            };
            this.ResizeTo(size, duration, easing);
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.FadeColour(hoverColor, 100D);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.FadeColour(defaultColor, 100D);
            base.OnHoverLost(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (!base.OnMouseDown(e)) return false;

            // Note that we are changing the color of the box here as to not interfere with the hover effect.
            box.FadeColour(highlightColor, 100);
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if (e.Button != MouseButton.Left) return;

            box.FadeColour(Colour4.White, 100);
            base.OnMouseUp(e);
        }
    }
}
