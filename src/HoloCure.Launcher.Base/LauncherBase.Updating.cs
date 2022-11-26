// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using HoloCure.Launcher.Base.Core.Updating;

namespace HoloCure.Launcher.Base;

partial class LauncherBase
{
    /// <summary>
    ///     Creates an <see cref="IUpdateManager"/> instance that will handle notifying the user of *launcher* updates, and applying them if possible.
    /// </summary>
    /// <remarks>
    ///     This has nothing to do with updating installations handled by this launcher.
    /// </remarks>
    protected abstract IUpdateManager? CreateUpdateManager();

    protected virtual void ScheduleUpdateManager()
    {
        // Schedule to be ran after LoadComplete finishes.
        Schedule(() =>
        {
            if (dependencies is null) throw new InvalidOperationException("Dependencies have not been loaded yet.");

            if (CreateUpdateManager()?.AsDrawable() is not { } updateManager) return;

            dependencies.CacheAs(updateManager);
            Add(updateManager);
        });
    }
}
