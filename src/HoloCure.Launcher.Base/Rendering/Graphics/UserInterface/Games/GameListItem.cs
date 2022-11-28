// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Games;
using HoloCure.Launcher.Base.Rendering.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;

namespace HoloCure.Launcher.Base.Rendering.Graphics.UserInterface.Games;

public class GameListItem : CompositeDrawable
{
    [Resolved]
    private GameProvider gameProvider { get; set; } = null!;

    private readonly Game game;
    private Box box = null!;
    private LauncherTextFlowContainer titleText = null!;

    private bool isHovered;
    private bool isSelected;
    private Colour4 hoverColor;
    private Colour4 idleColor;
    private Colour4 selectedColor;

    public GameListItem(Game game)
    {
        this.game = game;
    }

    [BackgroundDependencyLoader]
    private void load(TextureStore textures, LauncherTheme themes)
    {
        gameProvider.SelectedGame.ValueChanged += e =>
        {
            isSelected = e.NewValue == game;

            if (isSelected)
                box.FadeColour(selectedColor, 250D, Easing.Out);

            else
            {
                if (isHovered)
                    box.FadeColour(hoverColor, 250D, Easing.Out);
                else
                    box.FadeColour(idleColor, 250D, Easing.Out);
            }
        };

        hoverColor = themes.Gray(0.2f);
        idleColor = themes.Gray(0.1f);
        selectedColor = themes.Gray(0.35f);

        Height = 40f;
        Width = 200f;

        Masking = true;
        CornerRadius = 4f;

        var icon = textures.Get(game.GameIconPath);

        InternalChildren = new Drawable[]
        {
            box = new Box
            {
                Width = 200f,
                Height = 40f,

                Colour = idleColor,
            },
            new Sprite
            {
                Height = 32f,
                Width = 32f,

                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,

                Texture = icon,

                Position = new Vector2(8f, 0f)
            },
            titleText = new LauncherTextFlowContainer
            {
                AutoSizeAxes = Axes.Both,

                TextAnchor = Anchor.Centre,
                Origin = Anchor.CentreLeft,
                Anchor = Anchor.CentreLeft,

                Position = new Vector2(48f, 0f)
            }
        };

        void titleStyle(SpriteText x)
        {
            x.Font = FontUsage.Default;
            x.Colour = themes.LogoWhiteColor;
        }

        titleText.AddText(game.GameTitle, titleStyle);
    }

    protected override bool OnHover(HoverEvent e)
    {
        isHovered = true;
        box.FadeColour(isSelected ? selectedColor : hoverColor, 250D, Easing.Out);
        return base.OnHover(e);
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        base.OnHoverLost(e);
        isHovered = false;
        box.FadeColour(isSelected ? selectedColor : idleColor, 250D, Easing.Out);
    }

    protected override bool OnClick(ClickEvent e)
    {
        gameProvider.SelectedGame.Value = game;
        return base.OnClick(e);
    }
}
