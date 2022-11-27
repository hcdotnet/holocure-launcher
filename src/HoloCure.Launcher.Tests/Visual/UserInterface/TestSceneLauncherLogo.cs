// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base.Rendering.Graphics.UserInterface;
using NUnit.Framework;
using osu.Framework.Allocation;

namespace HoloCure.Launcher.Game.Tests.Visual.UserInterface;

[TestFixture]
public class TestSceneLauncherLogo : LauncherTestScene
{
    [BackgroundDependencyLoader]
    private void load()
    {
        var logo = new LauncherLogoOverlay();

        Add(logo);

        AddStep("reveal logo (1000ms)", () => logo.ShowLogo(1000D));
        AddStep("reveal title (1000ms)", () => logo.ShowTitle(1000D));
        AddStep("reveal version (1000ms)", () => logo.ShowVersion(1000D, "v1.0.0.0"));
    }
}
