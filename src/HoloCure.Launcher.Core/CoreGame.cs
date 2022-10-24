// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using osu.Framework;
using osu.Framework.Allocation;

namespace HoloCure.Launcher.Core;

public abstract partial class CoreGame : Game
{
    protected abstract IStoreProvider StoreProvider { get; }

    [BackgroundDependencyLoader]
    private void load()
    {
        dependencies.CacheAs<Game>(this);
        dependencies.CacheAs(this);
        dependencies.CacheAs(StoreProvider);

        StoreProvider.InitializeStores(this, dependencies);
    }
}
