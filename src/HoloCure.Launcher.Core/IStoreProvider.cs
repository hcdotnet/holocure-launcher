// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework.Allocation;

namespace HoloCure.Launcher.Core;

public interface IStoreProvider
{
    void InitializeStores(CoreGame game, DependencyContainer dependencies);
}
