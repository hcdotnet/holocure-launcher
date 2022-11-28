// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using HoloCure.Launcher.Base.Rendering.Graphics.Containers;
using HoloCure.Launcher.Base.Rendering.Graphics.Screens;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;

namespace HoloCure.Launcher.Base.Rendering.Graphics.UserInterface.Screens;

public class AboutScreen : LauncherScreen
{
    private ScreenStack stack;

    public AboutScreen(ScreenStack stack)
    {
        this.stack = stack;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        var tempText = new LauncherTextFlowContainer
        {
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,

            TextAnchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Anchor = Anchor.Centre,
        };

        tempText.AddText("TEMP TEXT BUT ABOUT SCREEN EDITION ", st => st.Font = FontUsage.Default);
        tempText.AddIcon(FontAwesome.Solid.Skull, st => st.Size = new Vector2(20f));

        InternalChildren = new Drawable[]
        {
            tempText,
            new ReturnButton(stack)
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.BottomCentre,

                Position = new Vector2(0f, -60f)
            }
        };
    }

    private class ReturnButton : LauncherHoverContainer
    {
        [Resolved]
        private LauncherTheme theme { get; set; } = null!;

        protected override IEnumerable<Drawable> EffectTargets => box.Yield();

        private readonly ScreenStack stack;
        private Box box = null!;
        private bool clicked;

        public ReturnButton(ScreenStack stack)
        {
            this.stack = stack;
        }

        [BackgroundDependencyLoader]
        private void load(LauncherTheme theme)
        {
            AutoSizeAxes = Axes.Both;

            HoverColor = theme.ReturnButtonHoverColour;
            IdleColor = theme.ReturnButtonIdleColour;

            Masking = true;
            CornerRadius = 10f;

            var returnText = new LauncherTextFlowContainer
            {
                AutoSizeAxes = Axes.Both,

                TextAnchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,

                Margin = new MarginPadding(10f)
            };

            returnText.AddParagraph("Mhm, that's cool,", st => st.Font = FontUsage.Default);
            returnText.AddParagraph("now take me back.", st => st.Font = FontUsage.Default);

            InternalChildren = new Drawable[]
            {
                box = new Box
                {
                    Colour = IdleColor,
                    RelativeSizeAxes = Axes.Both
                },
                returnText
            };

            Enabled.Value = true;
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.ScaleTo(1.1f, 200D, Easing.Out);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ScaleTo(1f, 200D, Easing.In);

            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            // Prevent quickly clicking multiple times from causing multiple exits.
            if (clicked) return base.OnClick(e);

            clicked = true;
            stack.Exit();

            return base.OnClick(e);
        }
    }
}
