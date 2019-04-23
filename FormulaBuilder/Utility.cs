namespace FormulaBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    public static class Utility
    {
        public static string AmpersandEscape(this string s) => s.Replace("&", "&&");
        public static string AmpersandUnescape(this string s) => s.Replace("&&", "&");

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
    }
}
