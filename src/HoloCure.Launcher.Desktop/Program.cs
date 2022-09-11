using System;
using System.Runtime.Versioning;
using osu.Framework.Platform;
using osu.Framework;
using HoloCure.Launcher.Game;
using Squirrel;

namespace HoloCure.Launcher.Desktop
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            if (OperatingSystem.IsWindows())
            {
                setupSquirrel();
            }
            /*else
            {
                // TODO: SDL_ShowSimpleMessageBox - warn about an unsupported platform?
            }*/

            using GameHost host = Host.GetSuitableDesktopHost(LauncherGameBase.GAME_NAME);
            using osu.Framework.Game game = new LauncherGameDesktop();
            host.Run(game);
        }

        [SupportedOSPlatform("windows")]
        private static void setupSquirrel()
        {
            SquirrelAwareApp.HandleEvents(
                onInitialInstall: (_, tools) =>
                {
                    tools.CreateShortcutForThisExe();
                    tools.CreateUninstallerRegistryEntry();
                },
                onAppUpdate: (_, tools) =>
                {
                    tools.CreateUninstallerRegistryEntry();
                },
                onAppUninstall: (_, tools) =>
                {
                    tools.RemoveShortcutForThisExe();
                    tools.RemoveUninstallerRegistryEntry();
                },
                onEveryRun: (_, _, _) =>
                {
                    // While setting the `ProcessAppUserModelId` fixes duplicate icons/shortcuts on the taskbar, it currently
                    // causes the right-click context menu to function incorrectly.
                    //
                    // This may turn out to be non-required after an alternative solution is implemented.
                    // see https://github.com/clowd/Clowd.Squirrel/issues/24
                    // tools.SetProcessAppUserModelId();
                }
            );
        }
    }
}
