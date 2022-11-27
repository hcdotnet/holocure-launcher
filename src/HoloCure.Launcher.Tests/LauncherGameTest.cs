using System;
using HoloCure.Launcher.Base;

namespace HoloCure.Launcher.Game.Tests;

public class LauncherGameTest : LauncherBase
{
    protected override IBuildInfo BuildInfo { get; } = new TestBuildInfo();

    private class TestBuildInfo : IBuildInfo
    {
        public Version AssemblyVersion => typeof(LauncherTestBrowser).Assembly.GetName()?.Version ?? new Version();

        public bool IsDeployedBuild => false;

        public string ReleaseChannel => "test";
    }
}
