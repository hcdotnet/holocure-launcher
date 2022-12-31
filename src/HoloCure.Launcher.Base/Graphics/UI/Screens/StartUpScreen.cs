// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using HoloCure.Launcher.Base.Graphics.Containers;
using HoloCure.Launcher.Base.Graphics.Screens;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace HoloCure.Launcher.Base.Graphics.UI.Screens;

public class StartUpScreen : LauncherScreen
{
    public const double DEFAULT_REVEAL_DURATION = 1000d;
    public const double DEFAULT_HIDE_DURATION = DEFAULT_REVEAL_DURATION / 2d;
    public const double DEFAULT_LOGO_TIME_UNTIL_RUN = 0d;
    public const double DEFAULT_TITLE_TIME_UNTIL_RUN = 300d;
    public const double DEFAULT_VERSION_TIME_UNTIL_RUN = DEFAULT_TITLE_TIME_UNTIL_RUN * 2d;
    public const double DEFAULT_HIDE_TIME_UNTIL_RUN = DEFAULT_REVEAL_DURATION * 3d;
    public const Easing DEFAULT_EASING = Easing.OutQuint;

    private const int logo_width = 160;
    private const int logo_height = 160;

    [Resolved]
    private LauncherTheme theme { get; set; } = null!;

    [Resolved]
    private IBuildInfo buildInfo { get; set; } = null!;

    private Sprite logoSprite = null!;
    private LauncherTextFlowContainer titleText = null!;
    private LauncherTextFlowContainer versionText = null!;

    [BackgroundDependencyLoader]
    private void load(TextureStore textures)
    {
        RelativeSizeAxes = Axes.Both;

        InternalChildren = new Drawable[]
        {
            logoSprite = new Sprite
            {
                Width = logo_width,
                Height = logo_height,

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
            }
        };
    }

    public virtual void RevealLogo(double duration = DEFAULT_REVEAL_DURATION, Easing easing = DEFAULT_EASING)
    {
        logoSprite.FadeIn(duration, easing);
        logoSprite.ScaleTo(1f, duration, easing);
    }

    public virtual void RevealTitle(double duration = DEFAULT_REVEAL_DURATION, Easing easing = DEFAULT_EASING)
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
        titleText.MoveToY((logoSprite.Height / 2f) + 4f, duration, Easing.OutQuint);
    }

    public virtual void RevealVersion(double duration = DEFAULT_REVEAL_DURATION, Easing easing = DEFAULT_EASING)
    {
        versionText.AddText(
            $"v{buildInfo.AssemblyVersion}-{buildInfo.ReleaseChannel}",
            x =>
            {
                x.Font = LauncherFont.Default.With(size: 16f);
                x.Colour = theme.LogoYellowColor;
            }
        );

        versionText.FadeIn(duration);
        versionText.MoveToY((logoSprite.Height / 2f) + 24f, duration, Easing.OutQuint);
    }

    public virtual void HideComponents(Action onHidden, double duration = DEFAULT_HIDE_DURATION, Easing easing = DEFAULT_EASING)
    {
        logoSprite.FadeOut(duration, easing);
        titleText.FadeOut(duration, easing);
        versionText.FadeOut(duration, easing);
        Scheduler.AddDelayed(onHidden, duration);
    }

    public virtual void RunDefaultIntroSequence(Action onHidden)
    {
        Scheduler.AddDelayed(() => RevealLogo(), DEFAULT_LOGO_TIME_UNTIL_RUN);
        Scheduler.AddDelayed(() => RevealTitle(), DEFAULT_TITLE_TIME_UNTIL_RUN);
        Scheduler.AddDelayed(() => RevealVersion(), DEFAULT_VERSION_TIME_UNTIL_RUN);
        Scheduler.AddDelayed(() => HideComponents(onHidden), DEFAULT_HIDE_TIME_UNTIL_RUN);
    }
}
