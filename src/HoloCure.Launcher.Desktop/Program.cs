using System;
using System.Collections.Generic;
using HoloCure.Launcher.Base;
using osu.Framework.Platform;
using osu.Framework;
using osu.Framework.Logging;

namespace HoloCure.Launcher.Desktop;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        // Collect and hold onto log entries printed prior to the creation of
        // our game object, as they will not be captured by SentryLogger. These
        // entries will then be forwarded to SentryLogger once it is created.
        // Forwarding is done by passing the collection through
        // LauncherGameDesktop, though this is subject to change.
        var entries = new List<LogEntry>();
        Logger.NewEntry += entries.Add;

        using GameHost host = Host.GetSuitableDesktopHost(LauncherBase.GAME_NAME);

        Logger.NewEntry -= entries.Add;
        using osu.Framework.Game game = new LauncherGameDesktop(entries);

        host.Run(game);
    }
}
