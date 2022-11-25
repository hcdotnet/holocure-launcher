// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE-MIT file in the repository root for full licence text.

namespace HoloCure.Launcher.Base.Core.Localization;

// Existing standards are kind of messy and all over the place.
// This is based on crowdin's documentation: https://developer.crowdin.com/language-codes/
public readonly partial record struct LanguageCode(string Code, string Name)
{
    public string Code { get; } = Code;

    public string Name { get; } = Name;
}
