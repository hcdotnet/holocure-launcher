// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using HoloCure.Launcher.Resources;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.IO.Stores;

namespace HoloCure.Launcher.Base;

partial class LauncherBase
{
    protected virtual void InitializeStores()
    {
        Resources.AddStore(new DllResourceStore(LauncherResources.ResourceAssembly));

        /*
         * var localizationLoader = new LauncherLocalizationLoader();
         * componentLoader(localizationLoader);
         * dependencies.CacheAs<IFluentLocalizationLoader>(localizationLoader);
         * dependencies.CacheAs(localizationLoader.GetLanguages());
         */

        InitializeFonts();
    }

    protected virtual void InitializeFonts()
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
        }.ForEach(x => AddFont(Resources, x));
    }
}
