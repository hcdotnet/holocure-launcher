// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace HoloCure.Launcher.Base;

partial class LauncherBase
{
    protected override Container<Drawable>? Content => content;

    private Container? content;
}
