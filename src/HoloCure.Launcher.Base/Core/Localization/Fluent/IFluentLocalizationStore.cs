// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Threading;
using System.Threading.Tasks;
using osu.Framework.Localisation;
using FluentArgs = System.Collections.Generic.IDictionary<string, Linguini.Shared.Types.Bundle.IFluentType>;

namespace HoloCure.Launcher.Base.Core.Localization.Fluent;

public interface IFluentLocalizationStore : ILocalisationStore
{
    /// <summary>
    ///     The language of the store.
    /// </summary>
    LanguageCode LangCode { get; }

    /// <summary>
    /// Retrieves an object from the store.
    /// </summary>
    /// <param name="name">The name of the object.</param>
    /// <param name="args">The Fluent arguments to pass in.</param>
    /// <returns>The object.</returns>
    string Get(string name, FluentArgs? args);

    /// <summary>
    /// Retrieves an object from the store asynchronously.
    /// </summary>
    /// <param name="name">The name of the object.</param>
    /// /// <param name="args">The Fluent arguments to pass in.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The object.</returns>
    Task<string> GetAsync(string name, FluentArgs? args, CancellationToken cancellationToken = default);
}
