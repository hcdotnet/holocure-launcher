// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Linq;
using HoloCure.Launcher.Base.Games;
using HoloCure.Launcher.Base.Rendering.Graphics.Containers;
using HoloCure.Launcher.Base.Rendering.Graphics.Screens;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace HoloCure.Launcher.Base.Rendering.Graphics.UserInterface.Screens;

public class MainScreen : LauncherScreen
{
    private LauncherScrollContainer scrollContainer = null!;
    private Container scrollBackground = null!;

    [BackgroundDependencyLoader]
    private void load(GameProvider gameProvider, LauncherTheme theme)
    {
        var flowContainer = new FillFlowContainer
        {
            Direction = FillDirection.Vertical,

            AutoSizeAxes = Axes.Y,
            RelativeSizeAxes = Axes.X,

            Spacing = new Vector2(0, 2f),
        };

        scrollContainer = new LauncherScrollContainer
        {
            // Height set in Update.
            Width = 200f,

            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,

            Position = new Vector2(0f, -8f),

            Child = flowContainer
        };

        InternalChildren = new Drawable[]
        {
            new Container
            {
                RelativeSizeAxes = Axes.Y,
                AutoSizeAxes = Axes.X,

                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,

                Position = new Vector2(8f),

                Children = new Drawable[]
                {
                    scrollBackground = new Container
                    {
                        Masking = true,
                        CornerRadius = 10f,

                        // Height set in Update.
                        Width = 216f,

                        Child = new Box
                        {
                            RelativeSizeAxes = Axes.Both,

                            Colour = theme.Gray(0.05f)
                        },
                    },
                    scrollContainer
                }
            }
        };

        gameProvider.Games.Value.ForEach(x => flowContainer.Add(x.MakeListItem()));
        gameProvider.SelectedGame.Value = gameProvider.Games.Value.First();
    }

    protected override void Update()
    {
        base.Update();

        scrollBackground.Height = scrollBackground.Parent.BoundingBox.Height - 16f;
        scrollContainer.Height = scrollBackground.BoundingBox.Height - 16f;
    }
}
