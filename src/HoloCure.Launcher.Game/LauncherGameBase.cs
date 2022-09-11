using System;
using System.Reflection;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using HoloCure.Launcher.Resources;
using osu.Framework.Development;

namespace HoloCure.Launcher.Game
{
    public class LauncherGameBase : osu.Framework.Game
    {
        public const string GAME_NAME = "HoloCure.Launcher";
        private const string build_suffix = "release";

        public virtual Version AssemblyVersion => Assembly.GetEntryAssembly()?.GetName().Version ?? new Version();

        public virtual bool IsDeployedBuild => AssemblyVersion.Major > 0; // Version is 0.0.0.0 on local builds.

        public virtual string BuildSuffix => build_suffix;

        public virtual string Version => !IsDeployedBuild ? $"local {(DebugUtils.IsDebugBuild ? "debug" : "release")}" : $"{AssemblyVersion}-{BuildSuffix}";

        protected override Container<Drawable> Content { get; }

        protected LauncherGameBase()
        {
            Name = GAME_NAME;

            base.Content.Add(Content = new Container
            {
                RelativeSizeAxes = Axes.Both
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(typeof(LauncherResources).Assembly));
        }
    }
}
