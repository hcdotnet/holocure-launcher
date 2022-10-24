// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HoloCure.Launcher.Core.Fluent.Exceptions;
using Linguini.Bundle;
using Linguini.Bundle.Errors;
using FluentArgs = System.Collections.Generic.IDictionary<string, Linguini.Shared.Types.Bundle.IFluentType>;
using FluentErrors = System.Collections.Generic.IList<Linguini.Bundle.Errors.FluentError>;

namespace HoloCure.Launcher.Core.Fluent;

/// <summary>
///     Helper struct capable of storing <see cref="FluentBundle"/> which way be sourced for messages in descending order, where the first added item is checked first, the second item checked second, etc., which each item acting as a fallback. <br />
///     This object does not support adding functions, entries, resources, etc., that should be done before adding bundles. Additionally, bundles are expected to have access to the same information.
/// </summary>
/// <param name="Bundles"></param>
public readonly record struct MultiSourcedFluentBundle(params FluentBundle[] Bundles)
{
    public FluentBundle[] Bundles { get; } = Bundles;

    public bool HasMessage(string identifier) => Bundles.Any(bundle => bundle.HasMessage(identifier));

    public bool TryGetMsg(string id, FluentArgs? args, out FluentErrors errors, [NotNullWhen(true)] out string? message)
    {
        foreach (FluentBundle bundle in Bundles)
        {
            if (bundle.TryGetMsg(id, args, out errors, out message)) return true;
        }

        errors = new List<FluentError>();
        message = null;
        return false;
    }

    public bool TryGetMsg(string id, string? attribute, FluentArgs? args, out FluentErrors errors, [NotNullWhen(true)] out string? message)
    {
        foreach (FluentBundle bundle in Bundles)
        {
            if (bundle.TryGetMsg(id, attribute, args, out errors, out message)) return true;
        }

        errors = new List<FluentError>();
        message = null;
        return false;
    }

    public string GetMsg(string id, FluentArgs? args)
    {
        return TryGetMsg(id, args, out FluentErrors errors, out string? message) ? message : throw new FluentErrorsException(errors);
    }
}
