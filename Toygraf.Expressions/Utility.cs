namespace ToyGraf.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using ToyGraf.Expressions.Enumerations;

    public static class Utility
    {
        #region Colours

        private static readonly Dictionary<string, Func<Color, float>> ColourOrders =
            new Dictionary<string, Func<Color, float>>
            {
                { "Alpha", c => c.A },
                { "Red", c => c.R },
                { "Green", c=> c.G },
                { "Blue", c => c.B },
                { "Hue", c => c.GetHue() },
                { "Saturation", c => c.GetSaturation() },
                { "Brightness", c => c.GetBrightness() }
            };

        public static int AlphaFromTransparencyPercent(int transparencyPercent) =>
            (int)Math.Round(255 * (1 - transparencyPercent / 100.0));

        public static int AlphaToTransparencyPercent(int alpha) =>
            (int)Math.Round(100 * (1 - alpha / 255.0));

        public static Color Contrast(this Color colour) => colour.IsBright() ? Color.Black : Color.White;

        public static IEnumerable<Color> GetColours() =>
            Enum.GetValues(typeof(KnownColor))
            .Cast<KnownColor>()
            .Select(Color.FromKnownColor)
            .Where(c => !c.IsSystemColor);

        public static string GetName(this Color colour)
        {
            var argb = colour.ToArgb();
            return GetColours().FirstOrDefault(c => c.ToArgb() == argb).Name;
        }

        public static IEnumerable<string> GetNonSystemColourNames(string orderByColourProperties) =>
            GetColours()
            .OrderByColourProperties(orderByColourProperties)
            .Select(c => c.Name);

        public static bool IsBright(this Color colour) => colour.Luma() > 0.5;
        public static bool IsDark(this Color colour) => colour.Luma() <= 0.5;
        public static bool IsVeryBright(this Color colour) => colour.Luma() > 0.75;
        public static bool IsVeryDark(this Color colour) => colour.Luma() <= 0.25;

        /// <summary>
        /// Luma can be used to determine whether or not a Color is "bright", "dark", etc.
        /// https://en.wikipedia.org/wiki/Luma_%28video%29
        /// </summary>
        /// <param name="colour">The sample colour.</param>
        /// <returns>The sample colour's Luma value.</returns>
        public static double Luma(this Color colour) =>
            (0.2126 * colour.R + 0.7152 * colour.G + 0.0722 * colour.B) / 255;

        public static Color MakeColour(Color baseColour, int transparencyPerCent) =>
            baseColour == Color.Transparent
            ? baseColour
            : Color.FromArgb(AlphaFromTransparencyPercent(transparencyPerCent), baseColour);

        private static IEnumerable<Color> OrderByColourProperties(
            this IEnumerable<Color> colours, string colourProperties)
        {
            IOrderedEnumerable<Color> result = null;
            var first = true;
            foreach (var colourProperty in colourProperties.Split(',')
                .Select(p => p.Trim().ToTitleCase()))
            {
                var colourOrder = ColourOrders[colourProperty];
                result =
                    first
                    ? colours.OrderBy(colourOrder)
                    : result.ThenBy(colourOrder);
                first = false;
            }
            return result;
        }

        #endregion

        #region Enums

        public static void PopulateWithDescriptions(this IList list, Type enumType)
        {
            list.Clear();
            foreach (var description in
                enumType
                .GetFields()
                .Select(p => p.GetCustomAttribute<DescriptionAttribute>())
                .OfType<DescriptionAttribute>()
                .Select(p => p.Description))
            { list.Add(description); }
        }

        public static void PopulateWithNames(this IList list, Type enumType)
        {
            list.Clear();
            foreach (var name in Enum.GetValues(enumType)
                .Cast<object>()
                .Distinct()
                .Select(p => Enum.GetName(enumType, p)))
            { list.Add(name); }
        }

        #endregion

        #region Expressions

        public const double piOver180 = Math.PI / 180;

        public static string AsString(this ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.And:
                    return "&";
                case ExpressionType.AndAlso:
                    return "&&";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return "≥";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "≤";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Negate:
                    return "-";
                case ExpressionType.Not:
                    return "!";
                case ExpressionType.NotEqual:
                    return "≠";
                case ExpressionType.Or:
                    return "|";
                case ExpressionType.OrElse:
                    return "||";
                case ExpressionType.Power:
                    return "^";
                case ExpressionType.Subtract:
                    return "-";
                case ExpressionType.UnaryPlus:
                    return "+";
                default:
                    return nodeType.ToString();
            }
        }

        public static float DegreesToRadians(this float degrees) => (float)(degrees * piOver180);
        public static double DegreesToRadians(this double degrees) => degrees * piOver180;

        public static OperandTypes GetBinaryOperandTypes(this ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Or:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                case ExpressionType.ExclusiveOr:
                    return OperandTypes.Boolean;
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                case ExpressionType.Power:
                // Since there is no comma operator in System.Linq.Expressions,
                // we spoof parameter lists by co-opting the otherwise unused
                // Modulo node type. This will be replaced later in processing.
                case ExpressionType.Modulo:
                    return OperandTypes.Double;
            }
            return OperandTypes.Unknown;
        }

        public static OperandTypes GetBinaryOperandTypes(this string op)
        {
            return GetBinaryOperandTypes(op.GetExpressionType());
        }

        public static Precedence GetPrecedence(this ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.OrElse:
                    return Precedence.LogicalOr;
                case ExpressionType.AndAlso:
                    return Precedence.LogicalAnd;
                case ExpressionType.Or:
                    return Precedence.BitwiseOr;
                case ExpressionType.And:
                    return Precedence.BitwiseAnd;
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                    return Precedence.Equality;
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    return Precedence.Relational;
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                    return Precedence.Additive;
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                    return Precedence.Multiplicative;
                case ExpressionType.Power:
                    return Precedence.Exponential;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        public static ExpressionType GetExpressionType(this string op)
        {
            switch (op)
            {
                case ",":
                    // Since there is no comma operator in System.Linq.Expressions,
                    // we spoof parameter lists by co-opting the otherwise unused
                    // Modulo node type. This will be replaced later in processing.
                    return ExpressionType.Modulo;
                case "?":
                case ":":
                    return ExpressionType.Conditional;
                case "|":
                    return ExpressionType.Or;
                case "||":
                    return ExpressionType.OrElse;
                case "&":
                    return ExpressionType.And;
                case "&&":
                    return ExpressionType.AndAlso;
                case "=":
                case "==":
                    return ExpressionType.Equal;
                case "≠":
                case "<>":
                case "!=":
                    return ExpressionType.NotEqual;
                case "<":
                case "≱":
                    return ExpressionType.LessThan;
                case ">":
                case "≰":
                    return ExpressionType.GreaterThan;
                case "≯":
                case "≤":
                case "<=":
                    return ExpressionType.LessThanOrEqual;
                case "≮":
                case "≥":
                case ">=":
                    return ExpressionType.GreaterThanOrEqual;
                case "+":
                    return ExpressionType.Add;
                case "-":
                    return ExpressionType.Subtract;
                case "*":
                case "×":
                case Ops.ImpliedProduct:
                    return ExpressionType.Multiply;
                case "/":
                case "÷":
                case "⁄": // Unicode Fraction Slash (U+2044)
                    return ExpressionType.Divide;
                case "^":
                case Ops.SuperscriptPower:
                    return ExpressionType.Power;
                case Ops.UnaryPlus:
                    return ExpressionType.UnaryPlus;
                case Ops.UnaryMinus:
                    return ExpressionType.Negate;
                case "!":
                case "~":
                    return ExpressionType.Not;
            }
            throw new FormatException();
        }

        public static double GetNamedValue(this string constant)
        {
            switch (constant.ToLower())
            {
                case "e":
                    return Math.E;
                case "π":
                case "pi":
                    return Math.PI;
                case "ϕ":
                case "phi":
                    return (1 + Math.Sqrt(5)) / 2;
            }
            return 0;
        }

        public static Precedence GetPrecedence(this string op)
        {
            switch (op)
            {
                case ")":
                    return Precedence.Assignment;
                case ",":
                    return Precedence.Sequential;
                case "?":
                case ":":
                    return Precedence.Ternary;
                case "||":
                    return Precedence.LogicalOr;
                case "&&":
                    return Precedence.LogicalAnd;
                case "|":
                    return Precedence.BitwiseOr;
                case "&":
                    return Precedence.BitwiseAnd;
                case "=":
                case "==":
                case "≠":
                case "<>":
                case "!=":
                    return Precedence.Equality;
                case "<":
                case ">":
                case "≮":
                case "≯":
                case "≤":
                case "≥":
                case "<=":
                case ">=":
                case "≰":
                case "≱":
                    return Precedence.Relational;
                case "+":
                case "-":
                    return Precedence.Additive;
                case "*":
                case "×":
                case "/":
                case "÷":
                    return Precedence.Multiplicative;
                case "^":
                    return Precedence.Exponential;
                case Ops.ImpliedProduct:
                    return Precedence.Implied;
                case "'":
                    return Precedence.Postfix;
                case Ops.SuperscriptPower:
                case Ops.SuperscriptPowerSwap:
                    return Precedence.Superscript;
                case "⁄": // Unicode Fraction Slash (U+2044)
                    return Precedence.Fraction;
            }
            return Precedence.Unary;
        }

        /// <summary>
        /// Find the rightmost non-relational descendant in a relational subtree
        /// </summary>
        /// <param name="e">The source expression.</param>
        /// <returns>The rightmost non-relational descendant of e.</returns>
        public static Expression GetRightmostRelation(this Expression e)
        {
            while (e.IsRelational())
                e = ((BinaryExpression)e).Right;
            return e;
        }

        /// <summary>
        /// Determine whether a given ExpressionType is relational, i.e.,
        /// "<", "<=", ">=" or ">".
        /// </summary>
        /// <param name="nodeType">The given ExpressionType.</param>
        /// <returns>True if nodeType is relational, otherwise false.</returns>
        public static bool IsRelational(this ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.GreaterThan:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Determine whether a given Expression is relational, i.e.,
        /// either a relational binary itself ("<", "<=", ">=" or ">"),
        /// or a conjunction ("&") of two relational expressions.
        /// </summary>
        /// <param name="e">The given Expression.</param>
        /// <returns>True if the Expression is relational, otherwise false.</returns>
        public static bool IsRelational(this Expression e)
        {
            if (e is BinaryExpression b)
            {
                var nodeType = b.NodeType;
                return nodeType.IsRelational()
                    || nodeType == ExpressionType.And
                    && b.Left.IsRelational()
                    && b.Right.IsRelational();
            }
            return false;
        }

        public static float RadiansToDegrees(this float radians) => (float)(radians / piOver180);
        public static double RadiansToDegrees(this double radians) => radians / piOver180;

        public static Expression ToBoolean(this Expression e)
        {
            var type = e.Type;
            if (type == typeof(bool))
                return e;
            if (type == typeof(double))
                return Expression.NotEqual(e, 0.0.Constant());
            throw new FormatException($"Unsuppported type {type}");
        }

        public static Expression ToDouble(this Expression e)
        {
            var type = e.Type;
            if (type == typeof(bool))
                return Expression.Condition(e, 1.0.Constant(), 0.0.Constant());
            if (type == typeof(double))
                return e;
            throw new FormatException($"Unsuppported type {type}");
        }

        public static bool UsesTime(this Expression e)
        {
            if (e == Expressions.t)
                return true;
            if (e is UnaryExpression u)
                return u.Operand.UsesTime();
            if (e is MethodCallExpression m)
                return m.Arguments.Any(p => p.UsesTime());
            if (e is BinaryExpression b)
                return b.Left.UsesTime() || b.Right.UsesTime();
            return false;
        }

        public static double VulgarFractionToDouble(this char fraction)
        {
            switch (fraction)
            {
                case '½': return 0.5;
                case '⅓': return 1.0 / 3;
                case '⅔': return 2.0 / 3;
                case '¼': return 0.25;
                case '¾': return 0.75;
                case '⅕': return 0.2;
                case '⅖': return 0.4;
                case '⅗': return 0.6;
                case '⅘': return 0.8;
                case '⅙': return 1.0 / 6;
                case '⅚': return 5.0 / 6;
                case '⅐': return 1.0 / 7;
                case '⅛': return 0.125;
                case '⅜': return 0.375;
                case '⅝': return 0.625;
                case '⅞': return 0.875;
                case '⅑': return 1.0 / 9;
                case '⅒': return 0.1;
            }
            return 0;
        }

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
        /// Convert a string, potentially containing ampersands, for use in an
        /// accelerator-enabled UI context (label text, menu item caption, etc).
        /// </summary>
        /// <param name="s">The input string</param>
        /// <returns>The input string with all ampersands escaped (doubled up).</returns>
        public static string AmpersandEscape(this string s) => s?.Replace("&", "&&");

        /// <summary>
        /// Convert a string, obtained from an accelerator-enabled UI context
        /// (label text, menu item caption, etc) and potentially containing double
        /// ampersands, for use in other contexts.
        /// </summary>
        /// <param name="s">The string obtained from the UI context.</param>
        /// <returns>The input string with all escaped (doubled) ampersands unescaped.</returns>
        public static string AmpersandUnescape(this string s) => s?.Replace("&&", "&");

        public static string ToTitleCase(this string s) =>
            s == string.Empty
            ? string.Empty
            : $"{char.ToUpper(s[0])}{s.ToLower().Substring(1)}";

        /// <summary>
        /// Make a legal file name from a given string which may contain prohibited
        /// characters or substrings.
        /// 
        /// https://docs.microsoft.com/en-us/windows/desktop/fileio/naming-a-file
        /// 
        /// Do not assume case sensitivity. Use any character in the current code page
        /// for a name, including Unicode characters and characters in the extended
        /// character set (128–255), except for the following: <>:"/\|?*
        /// 
        /// Do not use the following reserved names for the name of a file:
        /// CON PRN AUX CLOCK$ NUL COM# LPT# (where # is a digit, 0..9).
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToFilename(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return "(untitled)";
            var t = new StringBuilder(s.Trim());
            t.Replace('.', ',');
            foreach (var c in Path.GetInvalidFileNameChars()) t.Replace(c, '_');
            s = t.ToString();
            return Regex.IsMatch(s, @"(^CON$|^PRN$|^AUX$|^CLOCK\$$|^NUL$|^COM[0-9]$|^LPT[0-9]$)",
                RegexOptions.IgnoreCase) ? s + "_" : s;
        }

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
