// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Graphics.Sprites;

namespace HoloCure.Launcher.Base.Graphics.Sprites;

public class LauncherSpriteText : SpriteText
{
    public LauncherSpriteText()
    {
        Shadow = true;
        Font = LauncherFont.Default;
    }
}
