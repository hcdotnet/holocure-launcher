﻿// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Allocation;

namespace HoloCure.Launcher.Game;

partial class LauncherGame
{
    private DependencyContainer? dependencies;

    protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
}
