// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE-MIT file in the repository root for full licence text.

using osu.Framework.Graphics;
using osuTK.Graphics;

namespace HoloCure.Launcher.Base.Rendering.Graphics;

public class LauncherTheme
{
    public virtual Colour4 BackgroundColour => Color4.Black;

    public virtual Colour4 LinkIdleColor => Colour4.LightBlue;

    public virtual Colour4 TooltipBackgroundColor => Colour4.FromHex("333");

    public virtual Colour4 LogoCyanColor => Colour4.FromHex("36C6FF");

    public virtual Colour4 LogoWhiteColor => Colour4.FromHex("FFFFFF");

    public virtual Colour4 LogoPinkColor => Colour4.FromHex("FB83B4");

    public virtual Colour4 LogoYellowColor => Colour4.FromHex("FFC30E");

    public virtual Color4 Gray(float amt) => new(amt, amt, amt, 1f);

    public virtual Color4 Gray(byte amt) => new(amt, amt, amt, 255);
}
