using System.Threading.Tasks;
using osu.Framework.Graphics;

namespace HoloCure.Launcher.Game.Updater
{
    /// <summary>
    ///     Manages checking for and installing launcher updates.
    /// </summary>
    public interface IUpdateManager
    {
        /// <summary>
        ///     Whether we can check for updates.
        /// </summary>
        bool CanCheckForUpdates();

        /// <summary>
        ///     Check whether an update is waiting to be installed.
        /// </summary>
        Task<bool> CheckForUpdateAsync();

        /// <summary>
        ///     Performs an asynchronous update check.
        /// </summary>
        Task<bool> PerformUpdateCheck();

        /// <summary>
        ///     Return this object as a <see cref="Drawable"/> to be added to a <see cref="Drawable"/> hierarchy.
        /// </summary>
        Drawable AsDrawable();
    }
}
