// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Localisation;

namespace HoloCure.Launcher.Base.Core.Updating.UpdateManagers;

/// <summary>
///     Default implementation of <see cref="IUpdateManager{TUpdatePackage}"/> with default behaviors.
/// </summary>
public abstract class UpdateManager : Drawable, IUpdateManager
{
    [Resolved]
    private LauncherBase.IBuildInfo buildInfo { get; set; } = null!;

    private UpdatePackage? update;

    protected override void LoadComplete()
    {
        base.LoadComplete();

        // Check immediately and then continue checking every 30 minutes.
        Schedule(() => Task.Run(CheckForUpdatesAsync));
        Scheduler.AddDelayed(() => Task.Run(CheckForUpdatesAsync), 30 * 60 * 1000, true);
    }

    protected virtual async Task CheckForUpdatesAsync()
    {
        var canUpdate = setAndExtract(IsManagerAllowedToUpdate());
        if (!canUpdate) return;

        var info = setAndExtract(GetBuildInfo());
        var packages = setAndExtract(await GetAvailableUpdatesAsync(info));
        update = setAndExtract(SelectUpdate(packages, info));
    }

    private TValue setAndExtract<TValue>((TValue retVal, UpdateAvailability availability, LocalisableString message) val)
    {
        Availability.Value = (val.availability, val.message);
        return val.retVal;
    }

    #region IUpdateManager Impl

    public Bindable<(UpdateAvailability availability, LocalisableString message)> Availability { get; } = new();

    public virtual (bool canUpdate, UpdateAvailability availability, LocalisableString message) IsManagerAllowedToUpdate() =>
        !buildInfo.IsDeployedBuild ? (false, UpdateAvailability.UpdateUnavailableSuccess, "Undeployed build.") : (true, UpdateAvailability.Checking, "Resolving build information...");

    public (LauncherBase.IBuildInfo buildInfo, UpdateAvailability availability, LocalisableString message) GetBuildInfo() => new(buildInfo, UpdateAvailability.Checking, "Checking for updates...");

    public abstract Task<(IEnumerable<UpdatePackage> packages, UpdateAvailability availability, LocalisableString message)> GetAvailableUpdatesAsync(LauncherBase.IBuildInfo buildInfo);

    public virtual (UpdatePackage? package, UpdateAvailability availability, LocalisableString message) SelectUpdate(IEnumerable<UpdatePackage> updates, LauncherBase.IBuildInfo buildInfo)
    {
        var package = updates.FirstOrDefault(x => x.Version > buildInfo.AssemblyVersion);
        return (package, package is null ? UpdateAvailability.UpToDate : UpdateAvailability.UpdateAvailable, package is null ? "No updates available." : "Update available!");
    }

    public async Task<(bool successful, LocalisableString message)> DownloadAndApplyUpdate()
    {
        if (update is null) return (false, "Failed to update; no update available.");

        return await UpdateAsync(update);
    }

    protected abstract Task<(bool successful, LocalisableString message)> UpdateAsync(UpdatePackage update);

    public Drawable AsDrawable() => this;

    #endregion
}
