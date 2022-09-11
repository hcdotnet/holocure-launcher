using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace HoloCure.Launcher.Game.Updater
{
    /// <summary>
    ///     The standard <see cref="IUpdateManager"/> implementation, which boilerplate for handling check validity, etc.
    /// </summary>
    public abstract class UpdateManager : CompositeDrawable, IUpdateManager
    {
        [Resolved]
        private LauncherGameBase game { get; set; } = null!;

        private readonly object updLock = new();
        private Task<bool>? updateCheckTask;

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Schedule(() => Task.Run(CheckForUpdateAsync));

            /* TODO: Version checking and notifications.
             * string version = someVersionHere;
             * string lastVersion = someConfig.Get(Version);
             * if (CanCheckForUpdates() && version != lastVersion) {
             *   showANotification();
             * }
             * someConfig.Set(Version, version);
             */
        }

        #region IUpdateManager Impl

        public virtual bool CanCheckForUpdates() => game.IsDeployedBuild;

        public virtual async Task<bool> CheckForUpdateAsync()
        {
            if (!CanCheckForUpdates()) return false;

            Task<bool> waitTask;
            lock (updLock) waitTask = updateCheckTask ??= PerformUpdateCheck();
            bool hasUpdates = await waitTask.ConfigureAwait(false);
            lock (updLock) updateCheckTask = null;
            return hasUpdates;
        }

        public abstract Task<bool> PerformUpdateCheck();

        public virtual Drawable AsDrawable() => this;

        #endregion
    }
}
