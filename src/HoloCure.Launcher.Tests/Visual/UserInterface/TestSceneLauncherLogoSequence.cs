// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Base;
using HoloCure.Launcher.Base.Rendering.Graphics.UserInterface;
using NUnit.Framework;
using osu.Framework.Allocation;

namespace HoloCure.Launcher.Game.Tests.Visual.UserInterface;

[TestFixture]
public class TestSceneLauncherLogoSequence : LauncherTestScene
{
    [Resolved]
    private IBuildInfo buildInfo { get; set; } = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        var logo = new LauncherLogoOverlay();

        Add(logo);

        AddStep("run sequence", () => logo.RunIntroSequence('v' + buildInfo.AssemblyVersion.ToString() + '-' + buildInfo.ReleaseChannel));
    }
}
