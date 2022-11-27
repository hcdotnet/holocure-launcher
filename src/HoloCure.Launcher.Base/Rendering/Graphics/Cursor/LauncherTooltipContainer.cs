// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Rendering.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Localisation;
using osuTK;
using osuTK.Graphics;

namespace HoloCure.Launcher.Base.Rendering.Graphics.Cursor;

public class LauncherTooltipContainer : TooltipContainer
{
    protected override ITooltip CreateTooltip() => new LauncherTooltip();

    public LauncherTooltipContainer(CursorContainer? cursor = null)
        : base(cursor)
    {
    }

    // Reduce appear delay if the tooltip is already partly visible.
    protected override double AppearDelay => (1 - CurrentTooltip.Alpha) * base.AppearDelay;

    public class LauncherTooltip : Tooltip
    {
        [Resolved]
        private LauncherColor colors { get; set; } = null!;

        private readonly Box background;
        private readonly LauncherSpriteText text;
        private bool instantMovement = true;

        public LauncherTooltip()
        {
            AutoSizeEasing = Easing.OutQuint;

            CornerRadius = 5;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Color4.Black.Opacity(40),
                Radius = 5,
            };
            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.9f,
                    Colour = colors.Gray3
                },
                text = new LauncherSpriteText
                {
                    Padding = new MarginPadding(5),
                    Font = LauncherFont.GetFont(weight: FontWeight.Regular)
                }
            };
        }

        public override void SetContent(LocalisableString content)
        {
            if (content == text.Text) return;

            text.Text = content;

            if (IsPresent)
            {
                AutoSizeDuration = 250;
                background.FlashColour(colors.Gray(0.4f), 1000, Easing.OutQuint);
            }
            else
                AutoSizeDuration = 0;
        }

        protected override void PopIn()
        {
            instantMovement |= !IsPresent;
            this.FadeIn(500, Easing.OutQuad);
        }

        protected override void PopOut() => this.Delay(150).FadeOut(500, Easing.OutQuint);

        public override void Move(Vector2 pos)
        {
            if (instantMovement)
            {
                Position = pos;
                instantMovement = false;
            }
            else
                this.MoveTo(pos, 200, Easing.OutQuad);
        }
    }
}
