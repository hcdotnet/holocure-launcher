using System;
using HoloCure.Launcher.Base;
using HoloCure.Launcher.Base.Core.Updating;

namespace HoloCure.Launcher.Game.Tests;

public class LauncherGameTest : LauncherBase
{
    public override IBuildInfo BuildInfo { get; } = new TestBuildInfo();

    protected override void ScheduleUpdateManager()
    {
    }

    protected override IUpdateManager? CreateUpdateManager() => null;

    private class TestBuildInfo : IBuildInfo
    {
        public Version AssemblyVersion => typeof(LauncherTestBrowser).Assembly.GetName()?.Version ?? new Version();

        public bool IsDeployedBuild => false;

        public string ReleaseChannel => "test";

        public string Version => AssemblyVersion.ToString();
    }
}
