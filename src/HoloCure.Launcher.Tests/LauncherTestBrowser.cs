using System;
using HoloCure.Launcher.Base;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace HoloCure.Launcher.Game.Tests;

public class LauncherTestBrowser : LauncherBase
{
    protected override void LoadComplete()
    {
        base.LoadComplete();

        AddRange(new Drawable[]
        {
            new TestBrowser(GAME_NAME),
            new CursorContainer()
        });
    }

    public override void SetHost(GameHost host)
    {
        base.SetHost(host);
        host.Window.CursorState |= CursorState.Hidden;
    }

    public override IBuildInfo BuildInfo { get; } = new BrowserBuildInfo();

    private class BrowserBuildInfo : IBuildInfo
    {
        public Version AssemblyVersion => typeof(LauncherTestBrowser).Assembly.GetName()?.Version ?? new Version();

        public bool IsDeployedBuild => false;

        public string ReleaseChannel => "test-browser";

        public string Version => AssemblyVersion.ToString();
    }
}
