// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;

namespace HoloCure.Launcher.Base;

partial class LauncherBase
{
    protected abstract IBuildInfo BuildInfo { get; }
}

public interface IBuildInfo
{
    Version AssemblyVersion { get; }

    bool IsDeployedBuild { get; }

    string ReleaseChannel { get; }
}
