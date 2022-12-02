// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Reflection;
using HoloCure.Launcher.Base;

namespace HoloCure.Launcher.Game;

partial class LauncherGame
{
    public override IBuildInfo BuildInfo { get; } = new GameBuildInfo();

    public class GameBuildInfo : IBuildInfo
    {
        private static string[] split => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion.Split('|');

        public virtual Version AssemblyVersion => Assembly.GetEntryAssembly()?.GetName().Version ?? new Version();

        public virtual bool IsDeployedBuild => split.Length > 1 && bool.Parse(split[1]);

        public virtual string ReleaseChannel => split[0];
    }

    /*
     *
    private const string build_suffix = "release";

    public virtual Version AssemblyVersion => Assembly.GetEntryAssembly()?.GetName().Version ?? new Version();

    public virtual bool IsDeployedBuild => AssemblyVersion.Major > 0; // Version is 0.0.0.0 on development builds.

    public virtual string BuildSuffix => build_suffix;

    public virtual string Version => !IsDeployedBuild ? $"local {(DebugUtils.IsDebugBuild ? "debug" : "release")}" : $"{AssemblyVersion}-{BuildSuffix}";
     */
}
