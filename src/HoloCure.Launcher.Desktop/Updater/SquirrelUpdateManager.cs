using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using HoloCure.Launcher.Game;
using osu.Framework.Allocation;
using osu.Framework.Logging;
using Squirrel;
using Squirrel.SimpleSplat;
using LogLevel = Squirrel.SimpleSplat.LogLevel;

namespace HoloCure.Launcher.Desktop.Updater
{
    /// <summary>
    ///     An <see cref="HoloCure.Launcher.Game.Updater.UpdateManager"/> implementation which uses <c>Clown.Squirrel</c> to handle updating.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class SquirrelUpdateManager : HoloCure.Launcher.Game.Updater.UpdateManager

    {
        private const string github_url = "https://github.com/steviegt6/holocure-launcher";
        private const string logger_name = "updater";
        private static readonly Logger logger = Logger.GetLogger(logger_name);

        /// <summary>
        ///     Prepares to perform an update.
        /// </summary>
        private static Task prepareUpdateAsync() => UpdateManager.RestartAppWhenExited();

        /// <summary>
        ///     The <see cref="UpdateManager"/> instance used to handle updating.
        /// </summary>
        private UpdateManager? updateManager;

        /// <summary>
        ///     Whether an update has been downloaded but not yet applied.
        /// </summary>
        private bool updatePending;

        /// <summary>
        ///     The <see cref="ILogger"/> instance used by <c>Clowd.Squirrel</c>.
        /// </summary>
        private readonly ILogger squirrelLogger = new SquirrelLogger();

        [Resolved]
        private LauncherGame game { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            SquirrelLocator.CurrentMutable.Register(() => squirrelLogger, typeof(ILogger));
        }

        public override async Task<bool> PerformUpdateCheck() => await checkForSquirrelUpdateAsync().ConfigureAwait(false);

        private async Task<bool> checkForSquirrelUpdateAsync(bool useDeltaPatching = true)
        {
            // Should we schedule a retry on completion of this check?
            bool scheduleRecheck = true;

            try
            {
                // TODO: do we need a GH access token? 60 reqs per hour is pretty generous (per-IP)
                // TODO: how to handle potential pre-releases? should we make those stable? make an opt-in cfg option? needs discussion.
                updateManager ??= new GithubUpdateManager(github_url, false, null, "");
                UpdateInfo? info = await updateManager.CheckForUpdate(!useDeltaPatching).ConfigureAwait(false);

                if (info.ReleasesToApply.Count == 0)
                {
                    if (updatePending)
                    {
                        // TODO: display notifcation again too
                        await prepareUpdateAsync().ContinueWith(_ => Schedule(() => game.Exit()));
                        return true;
                    }

                    // No updates; retry again later.
                    return false;
                }

                scheduleRecheck = false;

                // TODO: Display notification to user that an update is available.

                try
                {
                    await updateManager.DownloadReleases(info.ReleasesToApply, x => _ = x / 100f).ConfigureAwait(false);
                    await updateManager.ApplyReleases(info, x => _ = x / 100f).ConfigureAwait(false);
                    updatePending = true;
                }
                catch (Exception e)
                {
                    if (useDeltaPatching)
                    {
                        logger.Add("delta patching failed; will attempt full download!");

                        // Could fail if deltas are unavailable for full update path (https://github.com/Squirrel/Squirrel.Windows/issues/959), try again without deltas.
                        await checkForSquirrelUpdateAsync(false).ConfigureAwait(false);
                    }
                    else
                    {
                        // TODO: display notification on *real* (non-delta patch) failure.
                        Logger.Error(e, "update failed!");
                    }
                }
            }
            catch
            {
                // Ignore and retry later; can be triggered by no internet connection or thread abortion.
                scheduleRecheck = true;
            }
            finally
            {
                // Schedule a new check in 30 minutes time.
                if (scheduleRecheck) Scheduler.AddDelayed(() => Task.Run(async () => await CheckForUpdateAsync().ConfigureAwait(false)), 60 * 1000 * 30);
            }

            return true;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            updateManager?.Dispose();
        }

        private class SquirrelLogger : ILogger, IDisposable
        {
            public LogLevel Level { get; set; } = LogLevel.Info;

            void ILogger.Write(string message, LogLevel logLevel)
            {
                if (logLevel < Level) return;

                logger.Add(message);
            }

            void IDisposable.Dispose() { }
        }
    }
}
