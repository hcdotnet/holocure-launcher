// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

namespace HoloCure.Launcher.Base.Core.Updating;

/// <summary>
///     Determines whether an update is available.
/// </summary>
public enum UpdateAvailability
{
    /// <summary>
    ///     Checking for updates.
    /// </summary>
    Checking,

    /// <summary>
    ///     Current version is up-to-date.
    /// </summary>
    UpToDate,

    /// <summary>
    ///     An update is available.
    /// </summary>
    UpdateAvailable,

    /// <summary>
    ///     Checking for updates failed due to an error.
    /// </summary>
    UpdateUnavailableError,

    /// <summary>
    ///     Checking for updates failed due to expected behavior (i.e. installed with a package manager).
    /// </summary>
    UpdateUnavailableSuccess
}
