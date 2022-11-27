using System;
using HoloCure.Launcher.Base;
using osu.Framework.Platform;
using osu.Framework;

namespace HoloCure.Launcher.Desktop;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        using GameHost host = Host.GetSuitableDesktopHost(LauncherBase.GAME_NAME);
        using osu.Framework.Game game = new LauncherGameDesktop();
        host.Run(game);
    }
}
