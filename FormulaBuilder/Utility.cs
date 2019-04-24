namespace FormulaBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    public static class Utility
    {
        #region Colours

        private static IEnumerable<string> _nonSystemColourNames = null;
        public static IEnumerable<string> NonSystemColourNames
        {
            get
            {
                if (_nonSystemColourNames == null)
                    _nonSystemColourNames =
                        Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>()
                        .Select(Color.FromKnownColor).Where(c => !c.IsSystemColor)
                        .OrderBy(c => c.GetHue()).ThenBy(c => c.GetSaturation())
                        .Select(c => c.Name);
                return _nonSystemColourNames;
            }
        }

        public static int AlphaFromTransparencyPercent(int transparencyPercent) =>
            (int)Math.Round(255 * (1 - transparencyPercent / 100.0));

        public static int AlphaToTransparencyPercent(int alpha) =>
            (int)Math.Round(100 * (1 - alpha / 255.0));

        public static Color MakeColour(Color baseColour, int transparencyPerCent) =>
            Color.FromArgb(AlphaFromTransparencyPercent(transparencyPerCent), baseColour);

        #endregion

        #region Functions

        public static string[] FunctionNames = new[]
        {
            "Abs", "Acos", "Acosh", "Acot", "Acoth", "Acsc", "Acsch", "Asec", "Asech", "Asin",
            "Asinh", "Atan", "Atanh", "Ceiling", "Cos", "Cosh", "Cot", "Coth", "Csc", "Csch",
            "Erf", "Exp", "Floor", "Ln", "Log10", "Round", "Sec", "Sech", "Sign", "Sin",
            "Sinh", "Sqrt", "Step", "Tan", "Tanh"
        };

        #endregion

        #region Strings

        /// <summary>
        /// Convert a string containing ampersands for use as a menu item caption
        /// </summary>
        /// <param name="s">The input string</param>
        /// <returns>The input string with all ampersands escaped (doubled up)</returns>
        public static string AmpersandEscape(this string s) => s.Replace("&", "&&");

        /// <summary>
        /// Convert a string obtained from a menu caption for use in other contexts
        /// </summary>
        /// <param name="s">The string obtained from a menu caption</param>
        /// <returns>The input string with all escaped (doubled) ampersands unescaped</returns>
        public static string AmpersandUnescape(this string s) => s.Replace("&&", "&");

        #endregion

        #region Subscripts & Superscripts

        public const string
            Subscripts = "₀₁₂₃₄₅₆₇₈₉₊₋₌₍₎ₐₑₕᵢⱼₖₗₘₙₒₚᵣₛₜᵤᵥₓᵦᵧᵨᵩᵪ",
            Transcripts = "0123456789+-=()aehijklmnoprstuvxβγρψχbcdfgwyzABDEGHIJKLMNOPRTUVWαδεθιφ",
            Superscripts = "⁰¹²³⁴⁵⁶⁷⁸⁹⁺⁻⁼⁽⁾ᵃᵉʰⁱʲᵏˡᵐⁿᵒᵖʳˢᵗᵘᵛˣᵝᵞρᵠᵡᵇᶜᵈᶠᵍʷʸᶻᴬᴮᴰᴱᴳᴴᴵᴶᴷᴸᴹᴺᴼᴾᴿᵀᵁⱽᵂᵅᵟᵋᶿᶥᶲ";

        public static bool IsSubscript(this char c) => Subscripts.IndexOf(c) >= 0;
        public static bool IsSuperscript(this char c) => Superscripts.IndexOf(c) >= 0;

        public static string FromSubscript(this string s) => Transcribe(s, Subscripts, Transcripts);
        public static string FromSuperscript(this string s) => Transcribe(s, Superscripts, Transcripts);
        public static string SubToSuperscript(this string s) => Transcribe(s, Subscripts, Superscripts);
        public static string SuperToSubscript(this string s) => Transcribe(s, Superscripts, Subscripts);
        public static string ToSubscript(this string s) => Transcribe(s, Transcripts, Subscripts);
        public static string ToSuperscript(this string s) => Transcribe(s, Transcripts, Superscripts);

        public static string Transcribe(this string s, string source, string target)
        {
            var stringBuilder = new StringBuilder(s);
            for (var index = 0; index < Math.Min(source.Length, target.Length); index++)
                stringBuilder.Replace(source[index], target[index]);
            return stringBuilder.ToString();
        }

        #endregion
    }
}
