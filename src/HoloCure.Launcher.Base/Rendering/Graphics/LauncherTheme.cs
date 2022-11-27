// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE-MIT file in the repository root for full licence text.

using osu.Framework.Graphics;
using osuTK.Graphics;

namespace HoloCure.Launcher.Base.Rendering.Graphics;

public class LauncherTheme
{
    public virtual Colour4 LinkIdleColor => Colour4.LightBlue;

    public virtual Color4 Gray(float amt) => new(amt, amt, amt, 1f);

    public virtual Color4 Gray(byte amt) => new(amt, amt, amt, 255);
}
