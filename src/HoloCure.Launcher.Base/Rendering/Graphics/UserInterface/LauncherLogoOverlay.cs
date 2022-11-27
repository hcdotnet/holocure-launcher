// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Linq;
using HoloCure.Launcher.Base.Rendering.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace HoloCure.Launcher.Base.Rendering.Graphics.UserInterface;

public class LauncherLogoOverlay : CompositeDrawable
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
        RelativeSizeAxes = Axes.Both;

        InternalChildren = new Drawable[]
        {
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
    }

    public void RevealMovedComponents(double duration)
    {
        // Position to bottom-left corner.
        titleText.Anchor = Anchor.BottomLeft;
        titleText.Origin = Anchor.Centre;
        versionText.Anchor = Anchor.BottomLeft;
        versionText.Origin = Anchor.Centre;

        // Reset relative positions.
        titleText.Position = Vector2.Zero;
        versionText.Position = Vector2.Zero;

        // Move up half of the height to counteract centering.
        float titleHeight = titleText.Height;
        float versionHeight = versionText.Height;

        titleText.MoveToOffset(new Vector2(0f, -titleHeight / 2f));
        versionText.MoveToOffset(new Vector2(0f, -versionHeight / 2f));

        // Get widths to appropriately position title (move by half to counteract centering) and version (move to the side)
        float titleWidth = titleText.Children.Sum(x => x.Width);
        float versionWidth = versionText.Children.Sum(x => x.Width);

        titleText.MoveToOffset(new Vector2((titleWidth / 2f) + 4f, 0f)); // 4f for padding
        versionText.MoveToOffset(new Vector2(titleWidth + (versionWidth / 2f) + 12f, 0f)); // 12f for padding

        titleText.FadeIn(duration);
        versionText.FadeIn(duration);
    }
}
