// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Rendering.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace HoloCure.Launcher.Base.Rendering.Graphics.UserInterface;

public class LauncherLogo : CompositeDrawable
{
    private readonly Colour4 logoCyan = Colour4.FromHex("36C6FF");
    private readonly Colour4 logoWhite = Colour4.FromHex("FFFFFF");
    private readonly Colour4 logoPink = Colour4.FromHex("FB83B4");
    private readonly Colour4 logoYellow = Colour4.FromHex("FFC30E");

    private const int width = 160;
    private const int height = 160;

    public override bool IsPresent => true;

    private Sprite logoSprite = null!;
    private LauncherTextFlowContainer titleText = null!;
    private LauncherTextFlowContainer versionText = null!;

    [BackgroundDependencyLoader]
    private void load(TextureStore textures)
    {
        Height = height;
        Width = width;

        InternalChildren = new Drawable[]
        {
            logoSprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both,

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
                x.Colour = logoCyan;
            }
        );
        titleText.AddText(
            ".",
            x =>
            {
                x.Font = LauncherFont.Default.With(size: 20f);
                x.Colour = logoWhite;
            }
        );
        titleText.AddText(
            "Launcher",
            x =>
            {
                x.Font = LauncherFont.Default.With(size: 20f);
                x.Colour = logoPink;
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
                x.Colour = logoYellow;
            }
        );
        versionText.FadeIn(duration);
        versionText.MoveToY((height / 2f) + 24f, duration, Easing.OutQuint);
    }
}
