// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Rendering.Graphics;
using osu.Framework.Graphics.Sprites;

namespace HoloCure.Launcher.Game.Rendering.Graphics.Sprites;

public class LauncherSpriteText : SpriteText
{
    public LauncherSpriteText()
    {
        Font = LauncherFont.Default;
    }
}
