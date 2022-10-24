// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using HoloCure.Launcher.Core;
using HoloCure.Launcher.Core.Fluent;
using HoloCure.Launcher.Game.Localization;
using HoloCure.Launcher.Resources;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.IO.Stores;

namespace HoloCure.Launcher.Game;

partial class LauncherGameBase
{
    protected override IStoreProvider StoreProvider { get; }
}

public class LauncherStoreProvider : IStoreProvider
{
    private readonly Action<Drawable> componentLoader;

    public LauncherStoreProvider(Action<Drawable> componentLoader)
    {
        this.componentLoader = componentLoader;
    }

    public virtual void InitializeStores(CoreGame game, DependencyContainer dependencies)
    {
        game.Resources.AddStore(new DllResourceStore(LauncherResources.ResourceAssembly));

        var localizationLoader = new LauncherLocalizationLoader();
        componentLoader(localizationLoader);
        dependencies.CacheAs<IFluentLocalizationLoader>(localizationLoader);
        dependencies.CacheAs(localizationLoader.GetLanguages());

        InitializeFonts(game);
    }

    protected virtual void InitializeFonts(CoreGame game)
    {
        new[]
        {
            // "Fonts/Best10DOT",

            "Fonts/Torus/Torus-Regular",
            "Fonts/Torus/Torus-Light",
            "Fonts/Torus/Torus-SemiBold",
            "Fonts/Torus/Torus-Bold",

            "Fonts/Torus-Alternate/Torus-Alternate-Regular",
            "Fonts/Torus-Alternate/Torus-Alternate-Light",
            "Fonts/Torus-Alternate/Torus-Alternate-SemiBold",
            "Fonts/Torus-Alternate/Torus-Alternate-Bold",

            "Fonts/Inter/Inter-Regular",
            "Fonts/Inter/Inter-RegularItalic",
            "Fonts/Inter/Inter-Light",
            "Fonts/Inter/Inter-LightItalic",
            "Fonts/Inter/Inter-SemiBold",
            "Fonts/Inter/Inter-SemiBoldItalic",
            "Fonts/Inter/Inter-Bold",
            "Fonts/Inter/Inter-BoldItalic",

            "Fonts/Noto/Noto-Basic",
            "Fonts/Noto/Noto-Hangul",
            "Fonts/Noto/Noto-CJK-Basic",
            "Fonts/Noto/Noto-CJK-Compatibility",
            "Fonts/Noto/Noto-Thai",

            "Fonts/Venera/Venera-Light",
            "Fonts/Venera/Venera-Bold",
            "Fonts/Venera/Venera-Black"
        }.ForEach(x => game.AddFont(game.Resources, x));
    }
}
