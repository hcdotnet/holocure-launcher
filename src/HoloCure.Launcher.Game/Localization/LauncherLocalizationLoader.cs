// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using HoloCure.Launcher.Core.Fluent;
using Linguini.Bundle;
using Linguini.Bundle.Builder;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace HoloCure.Launcher.Game.Localization;

public class LauncherLocalizationLoader : Drawable, IFluentLocalizationLoader
{
    public static readonly LanguageCode DEFAULT_LANGUAGE = LanguageCode.en;

    [Resolved]
    private osu.Framework.Game game { get; set; } = null!;

    // TODO: Clean all this code up. Write tests?
    public virtual Languages GetLanguages()
    {
        static IEnumerable<string> matchLocale(IEnumerable<string> resources, string locale) => resources.Where(x => x.Split('/')[1] == locale);
        string[] getUnparsedResources(IEnumerable<string> resources) => resources.Select(x => new StreamReader(game.Resources.GetStream(x)).ReadToEnd()).ToArray();

        // '/' is safe to use here since it's sanitized.
        var resources = game.Resources.GetAvailableResources().Where(x => x.StartsWith("Localization/")).ToList();
        var locales = resources.Select(x => x.Split('/')[1]).Distinct();
        var stores = new List<IFluentLocalizationStore>();

        var defaultResources = matchLocale(resources, DEFAULT_LANGUAGE.Code);
        var defaultBundle = CreateBundle(DEFAULT_LANGUAGE.Code, getUnparsedResources(defaultResources));
        stores.Add(CreateStore(DEFAULT_LANGUAGE, new MultiSourcedFluentBundle(defaultBundle)));

        foreach (string locale in locales)
        {
            // Skip the default language since we load that first by default.
            if (locale == DEFAULT_LANGUAGE.Code) continue;

            var langCodeField = typeof(LanguageCode).GetField(locale, BindingFlags.Public | BindingFlags.Static);

            if (langCodeField is null)
            {
                // TODO: Error handing eventually?
                Logger.Log("Invalid locale detected: " + locale, LoggingTarget.Runtime, LogLevel.Error);
                continue;
            }

            var localeResources = matchLocale(resources, locale);
            var localeBundle = CreateBundle(locale, getUnparsedResources(localeResources));
            var bundle = new MultiSourcedFluentBundle(localeBundle, defaultBundle);

            stores.Add(CreateStore((LanguageCode)langCodeField.GetValue(null), bundle));
        }

        return new Languages(DEFAULT_LANGUAGE, stores);
    }

    protected virtual IFluentLocalizationStore CreateStore(LanguageCode languageCode, MultiSourcedFluentBundle bundle) => new MultiBundledFluentLocalizationStore(bundle, languageCode);

    protected virtual FluentBundle CreateBundle(string code, params string[] unparsedResourceArray) =>
        LinguiniBuilder.Builder()
                       .CultureInfo(new CultureInfo(code))
                       .AddResources(unparsedResourceArray)
                       .SetUseIsolating(false)
                       .UncheckedBuild();
}
