// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using HoloCure.Launcher.Base.Core.Localization.Fluent;

namespace HoloCure.Launcher.Base.Core.Localization;

public record Languages(LanguageCode DefaultLanguage, List<IFluentLocalizationStore> Stores)
{
    public LanguageCode DefaultLanguage { get; } = DefaultLanguage;

    public List<IFluentLocalizationStore> Stores { get; } = Stores;
}
