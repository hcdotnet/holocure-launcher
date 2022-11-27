// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Linq;
using HoloCure.Launcher.Base.Rendering.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace HoloCure.Launcher.Base.Rendering.Graphics.UserInterface;

public class LauncherOverlay : CompositeDrawable
{
    [Resolved]
    private LauncherTheme theme { get; set; } = null!;

    private const int width = 160;
    private const int height = 160;

    public override bool IsPresent => true;

    public PaneledScreenStack Panel { get; private set; } = null!;

    private Sprite logoSprite = null!;
    private LauncherTextFlowContainer titleText = null!;
    private LauncherTextFlowContainer versionText = null!;
    private LinkFlowContainer discordText = null!;
    private LinkFlowContainer githubText = null!;

    [BackgroundDependencyLoader]
    private void load(TextureStore textures)
    {
        RelativeSizeAxes = Axes.Both;

        InternalChildren = new Drawable[]
        {
            Panel = new PaneledScreenStack
            {
                // 0 height and width until animations are done playing so we can resize according to padding.
                Height = 0,
                Width = 0,

                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            },
            logoSprite = new Sprite
            {
                Height = height,
                Width = width,

                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,

                Texture = textures.Get("LauncherLogo"),

                Scale = Vector2.Zero,
                Alpha = 0f,
                AlwaysPresent = true
            },
            titleText = new LauncherTextFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,

                TextAnchor = Anchor.TopCentre,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,

                Alpha = 0f,
                AlwaysPresent = true
            },
            versionText = new LauncherTextFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,

                TextAnchor = Anchor.TopCentre,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,

                Alpha = 0f,
                AlwaysPresent = true
            },
            discordText = new LinkFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,

                TextAnchor = Anchor.TopCentre,
                Origin = Anchor.Centre,
                Anchor = Anchor.BottomRight,

                Alpha = 1f,
                AlwaysPresent = true
            },
            githubText = new LinkFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,

                TextAnchor = Anchor.TopCentre,
                Origin = Anchor.Centre,
                Anchor = Anchor.BottomRight,

                Alpha = 1f,
                AlwaysPresent = true
            }
        };

        void nonLinkStyle(SpriteText x)
        {
            x.Font = LauncherFont.Default.With(size: 16f);
            x.Colour = theme.FadedLinkColor;
        }

        void discordStyle(SpriteText x)
        {
            nonLinkStyle(x);
            x.Colour = theme.DiscordColor;
        }

        void githubStyle(SpriteText x)
        {
            nonLinkStyle(x);
            x.Colour = theme.GitHubColor;
        }

        discordText.AddText("Join our ", nonLinkStyle);
        discordText.AddLink("Discord", "https://discord.gg/Y8bvvqyFQw", theme.DiscordColor, theme.FadedLinkColor, discordStyle);
        discordText.AddText(" ", nonLinkStyle);
        discordText.AddIcon(FontAwesome.Brands.Discord, discordStyle);

        githubText.AddText("Fork me on ", nonLinkStyle);
        githubText.AddLink("GitHub", "https://github.com/steviegt6/holocure-launcher", theme.GitHubColor, theme.FadedLinkColor, githubStyle);
        githubText.AddText(" ", nonLinkStyle);
        githubText.AddIcon(FontAwesome.Brands.Github, githubStyle);

        // Let these sit off screen; putting this here because these need to render for width/height and I'm too lazy to make an elegant solution.
        discordText.MoveToOffset(new Vector2(1000f, 0f));
        githubText.MoveToOffset(new Vector2(1000f, 0f));
    }

    public void ShowLogo(double duration)
    {
        logoSprite.FadeIn(duration, Easing.OutQuint);
        logoSprite.ScaleTo(1f, duration, Easing.OutQuint);
    }

    public void ShowTitle(double duration)
    {
        titleText.AddText(
            "HoloCure",
            x =>
            {
                x.Font = LauncherFont.Default.With(size: 20f);
                x.Colour = theme.LogoCyanColor;
            }
        );
        titleText.AddText(
            ".",
            x =>
            {
                x.Font = LauncherFont.Default.With(size: 20f);
                x.Colour = theme.LogoWhiteColor;
            }
        );
        titleText.AddText(
            "Launcher",
            x =>
            {
                x.Font = LauncherFont.Default.With(size: 20f);
                x.Colour = theme.LogoPinkColor;
            }
        );
        titleText.FadeIn(duration);
        titleText.MoveToY((height / 2f) + 4f, duration, Easing.OutQuint);
    }

    public void ShowVersion(double duration, string version)
    {
        versionText.AddText(
            version,
            x =>
            {
                x.Font = LauncherFont.Default.With(size: 16f);
                x.Colour = theme.LogoYellowColor;
            }
        );
        versionText.FadeIn(duration);
        versionText.MoveToY((height / 2f) + 24f, duration, Easing.OutQuint);
    }

    public void RunIntroSequence(string version)
    {
        ShowLogo(1000D);
        Scheduler.AddDelayed(() => ShowTitle(1000D), 300D);
        Scheduler.AddDelayed(() => ShowVersion(1000D, version), 600D);
        Scheduler.AddDelayed(() => HideComponents(500D), 3000D);
        Scheduler.AddDelayed(() => RevealMovedComponents(500D), 3500D);
    }

    public void HideComponents(double duration)
    {
        logoSprite.FadeOut(duration);
        titleText.FadeOut(duration);
        versionText.FadeOut(duration);
        discordText.FadeOut(duration);
        githubText.FadeOut(duration);
    }

    public void RevealMovedComponents(double duration)
    {
        // Position to bottom-left corner.
        titleText.Anchor = Anchor.BottomLeft;
        versionText.Anchor = Anchor.BottomLeft;

        discordText.Anchor = Anchor.BottomRight;
        githubText.Anchor = Anchor.BottomRight;

        // Reset relative positions.
        titleText.Position = Vector2.Zero;
        versionText.Position = Vector2.Zero;
        discordText.Position = Vector2.Zero;
        githubText.Position = Vector2.Zero;

        // Move up half of the height to counteract centering.
        float titleHeight = titleText.Height;
        float versionHeight = versionText.Height;

        titleText.MoveToOffset(new Vector2(0f, -titleHeight / 2f));
        versionText.MoveToOffset(new Vector2(0f, -versionHeight / 2f));

        float discordHeight = discordText.Height;
        float githubHeight = githubText.Height;

        discordText.MoveToOffset(new Vector2(0f, -discordHeight / 2f));
        githubText.MoveToOffset(new Vector2(0f, -githubHeight / 2f));

        // Get widths to appropriately position title (move by half to counteract centering) and version (move to the side)
        float titleWidth = titleText.Children.Sum(x => x.Width);
        float versionWidth = versionText.Children.Sum(x => x.Width);

        titleText.MoveToOffset(new Vector2((titleWidth / 2f) + 4f, 0f)); // 4f for padding
        versionText.MoveToOffset(new Vector2(titleWidth + (versionWidth / 2f) + 12f, 0f)); // 12f for padding

        float discordWidth = discordText.Children.Sum(x => x.Width);
        float githubWidth = githubText.Children.Sum(x => x.Width);

        discordText.MoveToOffset(new Vector2((-discordWidth / 2f) - 4f, 0f)); // 4f for padding
        githubText.MoveToOffset(new Vector2(-discordWidth + (-githubWidth / 2f) - 12f, 0f)); // 12f for padding

        titleText.FadeIn(duration);
        versionText.FadeIn(duration);
        discordText.FadeIn(duration);
        githubText.FadeIn(duration);

        const float y_padding = 16f;
        const float x_padding = 16f;
        float yOffset = Math.Max(titleHeight, Math.Max(versionHeight, Math.Max(discordHeight, githubHeight)));

        // Using BoundingBox here probably isn't the best idea, but it works.
        Panel.MoveToOffset(new Vector2(0f, -yOffset / 2f));
        Panel.Height = Parent.BoundingBox.Height - yOffset - y_padding;
        Panel.Width = Parent.BoundingBox.Width - x_padding;

        Panel.FadeIn(duration);
    }
}
