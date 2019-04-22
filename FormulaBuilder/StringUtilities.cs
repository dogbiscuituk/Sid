﻿namespace FormulaBuilder
{
    using System.Text;

    public static class StringUtilities
    {
        const string
            Subscripts = "₊₋₀₁₂₃₄₅₆₇₈₉",
            Superscripts = "⁺⁻⁰¹²³⁴⁵⁶⁷⁸⁹",
            Transcripts = "+-0123456789";

        public static bool IsSubscript(this char c) => Subscripts.IndexOf(c) >= 0;
        public static bool IsSuperscript(this char c) => Superscripts.IndexOf(c) >= 0;

        public static string NormalToSubscript(this string number) => Transcribe(number, Transcripts, Subscripts);
        public static string NormalToSuperscript(this string number) => Transcribe(number, Transcripts, Superscripts);
        public static string SubscriptToNormal(this string number) => Transcribe(number, Subscripts, Transcripts);
        public static string SubscriptToSuperscript(this string number) => Transcribe(number, Subscripts, Superscripts);
        public static string SuperscriptToNormal(this string number) => Transcribe(number, Superscripts, Transcripts);
        public static string SuperscriptToSubscript(this string number) => Transcribe(number, Superscripts, Subscripts);

        public static string Transcribe(this string number, string source, string target)
        {
            var stringBuilder = new StringBuilder(number);
            for (var index = 0; index < source.Length; index++)
                stringBuilder.Replace(source[index], target[index]);
            return stringBuilder.ToString();
        }
    }
}
