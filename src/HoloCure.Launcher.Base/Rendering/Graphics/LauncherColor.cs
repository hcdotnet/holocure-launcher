// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE-MIT file in the repository root for full licence text.

using osu.Framework.Extensions.Color4Extensions;
using osuTK.Graphics;

namespace HoloCure.Launcher.Base.Rendering.Graphics;

public static class LauncherColor
{
    public static readonly Color4 PANEL_COLOR = Gray(20);

    public static readonly Color4 GRAY_0 = Color4Extensions.FromHex(@"000");
    public static readonly Color4 GRAY_1 = Color4Extensions.FromHex(@"111");
    public static readonly Color4 GRAY_2 = Color4Extensions.FromHex(@"222");
    public static readonly Color4 GRAY_3 = Color4Extensions.FromHex(@"333");
    public static readonly Color4 GRAY_4 = Color4Extensions.FromHex(@"444");
    public static readonly Color4 GRAY_5 = Color4Extensions.FromHex(@"555");
    public static readonly Color4 GRAY_6 = Color4Extensions.FromHex(@"666");
    public static readonly Color4 GRAY_7 = Color4Extensions.FromHex(@"777");
    public static readonly Color4 GRAY_8 = Color4Extensions.FromHex(@"888");
    public static readonly Color4 GRAY_9 = Color4Extensions.FromHex(@"999");
    public static readonly Color4 GRAY_A = Color4Extensions.FromHex(@"aaa");
    public static readonly Color4 GRAY_B = Color4Extensions.FromHex(@"bbb");
    public static readonly Color4 GRAY_C = Color4Extensions.FromHex(@"ccc");
    public static readonly Color4 GRAY_D = Color4Extensions.FromHex(@"ddd");
    public static readonly Color4 GRAY_E = Color4Extensions.FromHex(@"eee");
    public static readonly Color4 GRAY_F = Color4Extensions.FromHex(@"fff");

    public static Color4 Gray(float amt) => new(amt, amt, amt, 1f);

    public static Color4 Gray(byte amt) => new(amt, amt, amt, 255);
}
