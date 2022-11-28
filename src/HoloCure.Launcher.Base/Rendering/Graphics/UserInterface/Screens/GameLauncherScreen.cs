// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Games;
using HoloCure.Launcher.Base.Rendering.Graphics.Screens;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace HoloCure.Launcher.Base.Rendering.Graphics.UserInterface.Screens;

public class GameLauncherScreen : LauncherScreen
{
    private readonly Game game;

    public GameLauncherScreen(Game game)
    {
        this.game = game;
    }

    [BackgroundDependencyLoader]
    private void load(TextureStore textures)
    {
        InternalChildren = new Drawable[]
        {
            new Sprite
            {
                Origin = Anchor.TopCentre,
                Anchor = Anchor.TopCentre,

                Texture = textures.Get(game.GameTitlePath),

                Position = new Vector2(0f, 24f)
            }
        };
    }
}
