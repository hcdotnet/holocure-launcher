// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HoloCure.Launcher.Core.Fluent;
using Linguini.Shared.Types.Bundle;

namespace HoloCure.Launcher.Game.Localization;

public class MultiBundledFluentLocalizationStore : IFluentLocalizationStore
{
    public virtual LanguageCode LangCode { get; }

    public virtual CultureInfo EffectiveCulture { get; }

    protected virtual MultiSourcedFluentBundle Bundle { get; }

    public MultiBundledFluentLocalizationStore(MultiSourcedFluentBundle bundle, LanguageCode languageCode)
    {
        Bundle = bundle;
        LangCode = languageCode;
        EffectiveCulture = new CultureInfo(languageCode.Code);
    }

    #region IFluentLocalizationStore Impl

    // TODO TryGetMsg/HasMessage - error checking?
    public string Get(string name, IDictionary<string, IFluentType>? args) => Bundle.GetMsg(name, args);

    public Task<string> GetAsync(string name, IDictionary<string, IFluentType>? args, CancellationToken cancellationToken = default) => Task.FromResult(Get(name, args));

    #endregion

    #region ILocalisationStore Impl

    public virtual string Get(string name) => Get(name, null);

    public virtual Task<string> GetAsync(string name, CancellationToken cancellationToken = new()) => Task.FromResult(Get(name));

    public virtual Stream GetStream(string name) => throw new NotImplementedException();

    public virtual IEnumerable<string> GetAvailableResources() => throw new NotImplementedException();

    #endregion

    #region IDisposable Impl

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
