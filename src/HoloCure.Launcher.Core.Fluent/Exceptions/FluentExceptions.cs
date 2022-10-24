// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Linguini.Bundle.Errors;

namespace HoloCure.Launcher.Core.Fluent.Exceptions;

[Serializable]
public class FluentErrorsException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public FluentErrorsException(IList<FluentError>? errors = null)
        : base(makeErrorMessage(null, errors))
    {
    }

    public FluentErrorsException(string message, IList<FluentError>? errors = null)
        : base(makeErrorMessage(message, errors))
    {
    }

    public FluentErrorsException(string message, Exception inner, IList<FluentError>? errors = null)
        : base(makeErrorMessage(message, errors), inner)
    {
    }

    protected FluentErrorsException(
        SerializationInfo info,
        StreamingContext context
    )
        : base(info, context)
    {
    }

    private static string makeErrorMessage(string? message = null, IList<FluentError>? errors = null)
    {
        StringBuilder sb = new();

        if (message is not null)
        {
            sb.Append(message);

            if (errors is not null) sb.Append("\n\n");
        }

        if (errors is not null)
        {
            sb.Append("The following Fluent errors were provided:\n");

            foreach (FluentError error in errors) sb.AppendLine(error.ToString());
        }

        return sb.ToString();
    }
}