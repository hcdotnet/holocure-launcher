// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE-MIT file in the repository root for full licence text.

using osu.Framework.Graphics.Sprites;

// https://github.com/ppy/osu/blob/master/osu.Game/Graphics/OsuFont.cs

namespace HoloCure.Launcher.Base.Graphics;

public static class LauncherFont
{
    /// <summary>
    ///     The default font dize.
    /// </summary>
    private const float default_font_size = 16f;

    /// <summary>
    /// The default font.
    /// </summary>
    public static FontUsage Default => GetFont();

    public static FontUsage Numeric => GetFont(Typeface.Venera, weight: FontWeight.Bold);

    public static FontUsage Torus => GetFont(Typeface.Torus, weight: FontWeight.Regular);

    public static FontUsage TorusAlternate => GetFont(Typeface.TorusAlternate, weight: FontWeight.Regular);

    public static FontUsage Inter => GetFont(Typeface.Inter, weight: FontWeight.Regular);

    /// <summary>
    /// Retrieves a <see cref="FontUsage"/>.
    /// </summary>
    /// <param name="typeface">The font typeface.</param>
    /// <param name="size">The size of the text in local space. For a value of 16, a single line will have a height of 16px.</param>
    /// <param name="weight">The font weight.</param>
    /// <param name="italics">Whether the font is italic.</param>
    /// <param name="fixedWidth">Whether all characters should be spaced the same distance apart.</param>
    /// <returns>The <see cref="FontUsage"/>.</returns>
    public static FontUsage GetFont(Typeface typeface = Typeface.Torus, float size = default_font_size, FontWeight weight = FontWeight.Medium, bool italics = false, bool fixedWidth = false)
    {
        string? familyString = GetFamilyString(typeface);
        return new FontUsage(familyString, size, GetWeightString(familyString, weight), getItalics(italics), fixedWidth);
    }

    private static bool getItalics(in bool italicsRequested)
    {
        // right now none of our fonts support italics.
        // should add exceptions to this rule if they come up.
        return false;
    }

    /// <summary>
    /// Retrieves the string representation of a <see cref="Typeface"/>.
    /// </summary>
    /// <param name="typeface">The <see cref="Typeface"/>.</param>
    /// <returns>The string representation.</returns>
    public static string? GetFamilyString(Typeface typeface)
    {
        return typeface switch
        {
            Typeface.Venera => "Venera",
            Typeface.Torus => "Torus",
            Typeface.TorusAlternate => "Torus-Alternate",
            Typeface.Inter => "Inter",
            _ => null
        };
    }

    /// <summary>
    /// Retrieves the string representation of a <see cref="FontWeight"/>.
    /// </summary>
    /// <param name="family">The font family.</param>
    /// <param name="weight">The font weight.</param>
    /// <returns>The string representation of <paramref name="weight"/> in the specified <paramref name="family"/>.</returns>
    public static string? GetWeightString(string? family, FontWeight weight)
    {
        if ((family == GetFamilyString(Typeface.Torus) || family == GetFamilyString(Typeface.TorusAlternate)) && weight == FontWeight.Medium)
            // torus doesn't have a medium; fallback to regular.
            weight = FontWeight.Regular;

        return weight.ToString();
    }
}

public enum Typeface
{
    Venera,
    Torus,
    TorusAlternate,
    Inter
}

public enum FontWeight
{
    /// <summary>
    /// Equivalent to weight 300.
    /// </summary>
    Light = 300,

    /// <summary>
    /// Equivalent to weight 400.
    /// </summary>
    Regular = 400,

    /// <summary>
    /// Equivalent to weight 500.
    /// </summary>
    Medium = 500,

    /// <summary>
    /// Equivalent to weight 600.
    /// </summary>
    SemiBold = 600,

    /// <summary>
    /// Equivalent to weight 700.
    /// </summary>
    Bold = 700,

    /// <summary>
    /// Equivalent to weight 900.
    /// </summary>
    Black = 900
}

public static class LauncherFontExtensions
{
    /// <summary>
    /// Creates a new <see cref="FontUsage"/> by applying adjustments to this <see cref="FontUsage"/>.
    /// </summary>
    /// <param name="usage">The base <see cref="FontUsage"/>.</param>
    /// <param name="typeface">The font typeface. If null, the value is copied from this <see cref="FontUsage"/>.</param>
    /// <param name="size">The text size. If null, the value is copied from this <see cref="FontUsage"/>.</param>
    /// <param name="weight">The font weight. If null, the value is copied from this <see cref="FontUsage"/>.</param>
    /// <param name="italics">Whether the font is italic. If null, the value is copied from this <see cref="FontUsage"/>.</param>
    /// <param name="fixedWidth">Whether all characters should be spaced apart the same distance. If null, the value is copied from this <see cref="FontUsage"/>.</param>
    /// <returns>The resulting <see cref="FontUsage"/>.</returns>
    public static FontUsage With(this FontUsage usage, Typeface? typeface = null, float? size = null, FontWeight? weight = null, bool? italics = null, bool? fixedWidth = null)
    {
        string? familyString = typeface is not null ? LauncherFont.GetFamilyString(typeface.Value) : usage.Family;
        string? weightString = weight is not null ? LauncherFont.GetWeightString(familyString, weight.Value) : usage.Weight;

        return usage.With(familyString, size, weightString, italics, fixedWidth);
    }
}
