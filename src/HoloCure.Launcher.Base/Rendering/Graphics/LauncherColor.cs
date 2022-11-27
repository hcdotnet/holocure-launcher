// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE-MIT file in the repository root for full licence text.

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osuTK.Graphics;

namespace HoloCure.Launcher.Base.Rendering.Graphics;

public class LauncherColor
{
    public virtual Colour4 LinkIdleColor => Colour4.LightBlue;

    public virtual Color4 PanelColor => Gray(20);

    public virtual Color4 Gray0 => Color4Extensions.FromHex(@"000");

    public virtual Color4 Gray1 => Color4Extensions.FromHex(@"111");

    public virtual Color4 Gray2 => Color4Extensions.FromHex(@"222");

    public virtual Color4 Gray3 => Color4Extensions.FromHex(@"333");

    public virtual Color4 Gray4 => Color4Extensions.FromHex(@"444");

    public virtual Color4 Gray5 => Color4Extensions.FromHex(@"555");

    public virtual Color4 Gray6 => Color4Extensions.FromHex(@"666");

    public virtual Color4 Gray7 => Color4Extensions.FromHex(@"777");

    public virtual Color4 Gray8 => Color4Extensions.FromHex(@"888");

    public virtual Color4 Gray9 => Color4Extensions.FromHex(@"999");

    public virtual Color4 GrayA => Color4Extensions.FromHex(@"aaa");

    public virtual Color4 GrayB => Color4Extensions.FromHex(@"bbb");

    public virtual Color4 GrayC => Color4Extensions.FromHex(@"ccc");

    public virtual Color4 GrayD => Color4Extensions.FromHex(@"ddd");

    public virtual Color4 GrayE => Color4Extensions.FromHex(@"eee");

    public virtual Color4 GrayF => Color4Extensions.FromHex(@"fff");

    public virtual Color4 Gray(float amt) => new(amt, amt, amt, 1f);

    public virtual Color4 Gray(byte amt) => new(amt, amt, amt, 255);
}
