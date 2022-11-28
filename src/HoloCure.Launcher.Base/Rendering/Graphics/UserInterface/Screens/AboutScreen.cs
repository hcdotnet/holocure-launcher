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
    private void load(LauncherTheme theme, IBuildInfo buildInfo)
    {
        var aboutText = new LinkFlowContainer
        {
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,

            TextAnchor = Anchor.BottomCentre,
            Origin = Anchor.TopCentre,
            Anchor = Anchor.TopCentre,

            Position = new Vector2(0f, 160f)
        };

        initializeAboutText(aboutText, theme, buildInfo);

        InternalChildren = new Drawable[]
        {
            aboutText,
            new ReturnButton(stack)
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.BottomCentre,

                Position = new Vector2(0f, -60f)
            }
        };
    }

    private static void initializeAboutText(LinkFlowContainer aboutText, LauncherTheme theme, IBuildInfo buildInfo)
    {
        // God how is this going to be localized?

        void holocureStyle(SpriteText x)
        {
            x.Font = LauncherFont.Default.With(size: 36f);
            x.Colour = theme.LogoCyanColor;
        }

        void dotStyle(SpriteText x)
        {
            x.Font = LauncherFont.Default.With(size: 36f);
            x.Colour = theme.LogoWhiteColor;
        }

        void launcherStyle(SpriteText x)
        {
            x.Font = LauncherFont.Default.With(size: 36f);
            x.Colour = theme.LogoPinkColor;
        }

        void defaultStyle(SpriteText x)
        {
            x.Font = LauncherFont.Default.With(size: 20f);
            x.Colour = theme.FadedLinkColor;
        }

        void versionStyle(SpriteText x)
        {
            defaultStyle(x);
            x.Colour = theme.LogoYellowColor;
        }

        void heartStyle(SpriteText x)
        {
            defaultStyle(x);
            x.Colour = theme.HeartColor;
        }

        void linkStyle(SpriteText x)
        {
            defaultStyle(x);
            x.Colour = theme.LogoWhiteColor;
        }

        void finePrintLolStyle(SpriteText x)
        {
            defaultStyle(x);
            x.Font = x.Font.With(size: 16f);
        }

        aboutText.AddText("HoloCure", holocureStyle);
        aboutText.AddText(".", dotStyle);
        aboutText.AddText("Launcher ", launcherStyle);
        aboutText.AddText('v' + buildInfo.AssemblyVersion.ToString() + '-' + buildInfo.ReleaseChannel, versionStyle);
        aboutText.NewParagraph();

        aboutText.AddText("Kindly crafted with ", defaultStyle);
        aboutText.AddIcon(FontAwesome.Solid.Heart, heartStyle);
        aboutText.AddText(" by myself (", defaultStyle);
        aboutText.AddLink("Tomat", "https://github.com/steviegt6", theme.LogoWhiteColor, theme.LogoYellowColor, linkStyle);
        aboutText.AddText(") and ", defaultStyle);
        aboutText.AddLink("any contributions", "https://github.com/steviegt6/holocure-launcher/contributors", theme.LogoWhiteColor, theme.LogoYellowColor, linkStyle);
        aboutText.AddText(".", defaultStyle);
        aboutText.NewParagraph();

        aboutText.NewParagraph();
        aboutText.NewParagraph();

        aboutText.AddLink("HoloCure.Launcher", "https://github.com/steviegt6/holocure-launcher", theme.LogoWhiteColor, theme.LogoYellowColor, linkStyle);
        aboutText.AddText(" is a feature-rich, ", defaultStyle);
        aboutText.AddLink("free", "https://www.gnu.org/philosophy/free-sw.html", theme.LogoWhiteColor, theme.LogoYellowColor, linkStyle);
        aboutText.AddText(" (as in both \"", defaultStyle);
        aboutText.AddLink("free speech", "https://en.wikipedia.org/wiki/Gratis_versus_libre#Libre", theme.LogoWhiteColor, theme.LogoYellowColor, linkStyle);
        aboutText.AddText("\" and \"", defaultStyle);
        aboutText.AddLink("free beer", "https://en.wikipedia.org/wiki/Gratis_versus_libre#Gratis", theme.LogoWhiteColor, theme.LogoYellowColor, linkStyle);
        aboutText.AddText("\") alternative to the regular proprietary HoloCure Launcher.", defaultStyle);
        aboutText.NewParagraph();

        aboutText.NewParagraph();
        aboutText.NewParagraph();

        aboutText.AddText("This project is licensed under the ", defaultStyle);
        aboutText.AddLink("GNU General Public License", "https://www.gnu.org/licenses/gpl-3.0.en.html", theme.LogoWhiteColor, theme.LogoYellowColor, linkStyle);
        aboutText.AddText(", with bits of code sub-licensed under the MIT License.", defaultStyle);
        aboutText.NewParagraph();

        aboutText.AddText("Copies of these licenses may be found in the root directory of the ", defaultStyle);
        aboutText.AddLink("HoloCure.Launcher repository", "https://github.com/steviegt6/holocure-launcher", theme.LogoWhiteColor, theme.LogoYellowColor, linkStyle);
        aboutText.AddText(".", defaultStyle);
        aboutText.NewParagraph();

        aboutText.NewParagraph();
        aboutText.NewParagraph();

        aboutText.AddParagraph("HoloCure.Launcher  Copyright (C) 2022  Tomat and HoloCure.Launcher contributors", finePrintLolStyle);
        aboutText.AddParagraph("This program comes with ABSOLUTELY NO WARRANTY; for details see the aforementioned GNU General Public License.", finePrintLolStyle);
        aboutText.AddParagraph("This is free software, and you are welcome to redistribute it under certain conditions; for details see the aforementioned GNU General Public License.", finePrintLolStyle);
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
