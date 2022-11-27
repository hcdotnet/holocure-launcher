// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HoloCure.Launcher.Base;
using HoloCure.Launcher.Base.Core.Updating;
using HoloCure.Launcher.Base.Core.Updating.UpdateManagers;
using osu.Framework.Localisation;

namespace HoloCure.Launcher.Desktop.Updater;

public class DesktopUpdateManager : UpdateManager
{
    private const string launcher_external_update_provider = "LAUNCHER_EXTERNAL_UPDATE_PROVIDER";

    private UpdateManager delegatedUpdateManager;

    public DesktopUpdateManager(UpdateManager delegatedUpdateManager)
    {
        this.delegatedUpdateManager = delegatedUpdateManager;
    }

    public override (bool canUpdate, UpdateAvailability availability, LocalisableString message) IsManagerAllowedToUpdate()
    {
        string? packageManaged = Environment.GetEnvironmentVariable(launcher_external_update_provider);

        return !string.IsNullOrEmpty(packageManaged) ? (false, UpdateAvailability.UpdateUnavailableSuccess, "Installed through package manager.") : base.IsManagerAllowedToUpdate();
    }

    public override async Task<(IEnumerable<UpdatePackage> packages, UpdateAvailability availability, LocalisableString message)> GetAvailableUpdatesAsync(LauncherBase.IBuildInfo buildInfo)
    {
        return await delegatedUpdateManager!.GetAvailableUpdatesAsync(buildInfo);
    }

    protected override Task<(bool successful, LocalisableString message)> UpdateAsync(UpdatePackage update)
    {
        throw new System.NotImplementedException();
    }
}
