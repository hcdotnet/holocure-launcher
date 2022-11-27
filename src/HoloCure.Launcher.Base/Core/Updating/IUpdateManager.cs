using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HoloCure.Launcher.Base.Core.Updating.UpdateManagers;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Localisation;

namespace HoloCure.Launcher.Base.Core.Updating;

/// <summary>
///     Manages checking for and installing launcher updates.
/// </summary>
public interface IUpdateManager
{
    Bindable<(UpdateAvailability availability, LocalisableString message)> Availability { get; }

    /// <summary>
    ///     Determines whether this <see cref="IUpdateManager"/> may perform update checks and issue availability behavior.
    /// </summary>
    (bool canUpdate, UpdateAvailability availability, LocalisableString message) IsManagerAllowedToUpdate();

    /// <summary>
    ///     Gets the build info for this application; especially the version and release channel.
    /// </summary>
    (LauncherBase.IBuildInfo buildInfo, UpdateAvailability availability, LocalisableString message) GetBuildInfo();

    /// <summary>
    ///     Resolves available updates, filtered based on the release channel.
    /// </summary>
    Task<(IEnumerable<UpdatePackage> packages, UpdateAvailability availability, LocalisableString message)> GetAvailableUpdatesAsync(LauncherBase.IBuildInfo buildInfo);

    /// <summary>
    ///     Selects the latest update to use from the list of available updates.
    /// </summary>
    /// <returns></returns>
    (UpdatePackage? package, UpdateAvailability availability, LocalisableString message) SelectUpdate(IEnumerable<UpdatePackage> updates, LauncherBase.IBuildInfo buildInfo);

    /// <summary>
    ///     Downloads and applies the update.
    /// </summary>
    Task<(bool successful, LocalisableString message)> DownloadAndApplyUpdate();

    /// <summary>
    ///     Return this object as a <see cref="Drawable"/> to be added to a <see cref="Drawable"/> hierarchy.
    /// </summary>
    Drawable AsDrawable();
}

/// <summary>
///     Generic representation of an update package used with <see cref="UpdateManager"/>.
/// </summary>
/// <param name="Version">This update's version.</param>
/// <param name="ReleaseChannel">This update's release channel.</param>
/// <param name="DownloadUrl">The download URL for this update.</param>
public record UpdatePackage(Version Version, string ReleaseChannel, string DownloadUrl)
{
    public Version Version { get; } = Version;

    public string ReleaseChannel { get; } = ReleaseChannel;

    public string DownloadUrl { get; } = DownloadUrl;
}
