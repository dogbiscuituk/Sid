namespace ToyGraf.Expressions
{
    using System.Diagnostics;
    using System.Linq.Expressions;
    using ToyGraf.Expressions.Enumerations;

    partial class Expressions
    {
        #region Public Interface

        public static void TestAll()
        {
            TestParsers();
            TestDiffFunctions();
            TestDiffPolynomials();
            TestIntegrateFunctions();
            Maxima.DebugOff();
        }

        #endregion

        #region Private Properties

        private static Parser Parser = new Parser();

        #endregion

        #region Private Helpers

        private static void Check(string source, string expected, string actual)
        {
            var ok = actual == expected;
            if (!ok)
                ok = Parser.TryParse(expected, out Expression m_expected, out _)
                    && Parser.TryParse(actual, out Expression m_actual, out _)
                    && m_actual.ToMaxima().IsEquivalentTo(m_expected.ToMaxima());
            if (!ok)
                Debug.WriteLine($"*** ERROR *** source: {source}, expected: {expected}, actual: {actual}.");
            Debug.Assert(ok);
        }

        private static string TestDiff(string source, string expected)
        {
            var e_source = Parser.Parse(source);
            var derivative = e_source.Differentiate();
            var actual = derivative.AsString();
            Check($"({source})'", expected, actual);
            return actual;
        }

        private static void TestDiffInt(string source, string expected)
        {
            var actual = TestDiff(source, expected);
            TestIntegrate(source: actual, expected: source);
        }

        private static void TestIntDiff(string source, string expected)
        {
            var actual = TestIntegrate(source, expected);
            TestDiff(source: actual, expected: source);
        }

        private static string TestIntegrate(string source, string expected)
        {
            var e_source = Parser.Parse(source);
            var integral = e_source.Integrate();
            var actual = integral.AsString();
            Check($"∫{source} dx", expected, actual);
            return actual;
        }

        private static void TestParse(string source, string expected, Language language = Language.ToyGraf)
        {
            Parser.Language = language;
            var actual = Parser.Parse(source).Simplify().AsString();
            Parser.Language = Language.ToyGraf;
            Check(source, expected, actual);
        }

        #endregion

        #region Private Tests

        private static void TestDiffFunctions()
        {
            Debug.WriteLine("########################################## TestDiffFunctions()");
            TestDiff("abs x", "x/abs x");
            TestDiff("acos x", "-1/√(1-x²)");
            TestDiff("acosh x", "1/√(x²-1)");
            TestDiff("acot x", "-1/(x²+1)");
            TestDiff("acoth x", "-1/(x²-1)");
            TestDiff("acsc x", "-1/√(x²-1)/abs x");
            TestDiff("acsch x", "-1/√(x²+1)/abs x");
            TestDiff("asec x", "1/√(x²-1)/abs x");
            TestDiff("asech x", "-1/√(1-x²)/abs x");
            TestDiff("asin x", "1/√(1-x²)");
            TestDiff("asinh x", "1/√(x²+1)");
            TestDiff("atan x", "1/(x²+1)");
            TestDiff("atanh x", "-1/(x²-1)");
            //TestDiff("ceiling x", "default(Void)");
            TestDiff("cos x", "-sin x");
            TestDiff("cosh x", "sinh x");
            TestDiff("cot x", "-1/sin²x");
            TestDiff("coth x", "-1/sinh²x");
            TestDiff("csc x", "-cos x/sin²x");
            TestDiff("csch x", "-cosh x/sinh²x");
            TestDiff("exp x", "exp x");
            //TestDiff("floor x", "default(Void)");
            //TestDiff("hstep x", "default(Void)");
            TestDiff("log x", "1/x");
            //TestDiff("log10 x", "0");
            //TestDiff("round x", "default(Void)");
            TestDiff("sec x", "sin x/cos²x");
            TestDiff("sech x", "-sinh x/cosh²x");
            TestDiff("sign x", "0");
            TestDiff("sin x", "cos x");
            TestDiff("sinh x", "cosh x");
            TestDiff("sqrt x", "1/2√x");
            TestDiff("tan x", "1/cos²x");
            TestDiff("tanh x", "1/cosh²x");
        }

        private static void TestDiffPolynomials()
        {
            Debug.WriteLine("########################################## TestDiffPolynomials()");
            TestDiff("123", "0");
            TestDiff("x", "1");
            TestDiff("t", "0");
            TestDiff("x²", "2x");
            TestDiff("2x²+3x+5", "4x+3");
            TestDiff("2x²+3x+5", "4x+3");
            TestDiff("(x+1)⁶", "6(x+1)⁵");
            TestDiff("(x+1)⁶", "6x⁵+30x⁴+60x³+60x²+30x+6");
            TestDiff("x⁶+6x⁵+15x⁴+20x³+15x²+6x+1", "6(x+1)⁵");
            TestDiff("x⁶+6x⁵+15x⁴+20x³+15x²+6x+1", "6x⁵+30x⁴+60x³+60x²+30x+6");
        }

        private static void TestIntDiffFunctions()
        {
            TestIntDiff("abs x", "x*abs x/2");
            TestIntDiff("acos x", "x*acos x-√(1-x²)");
            TestIntDiff("acosh x", "x*acosh x-√(x²-1)");
            TestIntDiff("acot x", "(log(x²+1)+2x*acot x)/2");
            TestIntDiff("acoth x", "(log(x²-1)+2x*acoth x)/2");
            TestIntDiff("acsc x", "(log((abs x+√(x²-1))/abs x)-log((√(x²-1)-abs x)/abs x)+2x*acsc x)/2");
            TestIntDiff("acsch x", "(log((abs x+√(x²+1))/abs x)-log((√(x²+1)-abs x)/abs x)+2x*acsch x)/2");
            TestIntDiff("asec x", "-(log((abs x+√(x²-1))/abs x)-log((√(x²-1)-abs x)/abs x)-2x*asec x)/2");
            TestIntDiff("asech x", "-(atan(√(1-x²)/abs x)-x*asech x)");
            TestIntDiff("asin x", "x*asin x+√(1-x²)");
            TestIntDiff("asinh x", "x*asinh x-√(x²+1)");
            TestIntDiff("atan x", "-(log(x²+1)-2x*atan x)/2");
            TestIntDiff("atanh x", "(log(1-x²)+2x*atanh x)/2");
            TestIntDiff("cos x", "sin x");
            TestIntDiff("cosh x", "sinh x");
            TestIntDiff("cot x", "log sin x");
            TestIntDiff("coth x", "log sinh x");
            TestIntDiff("csc x", "-log(csc x+cot x)");
            TestIntDiff("csch x", "log tanh(x/2)");
            TestIntDiff("exp x", "exp x");
            TestIntDiff("log x", "x*(log x-1)");
            //TestIntDiff("log10 x", "0");
            TestIntDiff("sec x", "log(tan x+sec x)");
            TestIntDiff("sech x", "atan sinh x");
            TestIntDiff("sin x", "-cos x");
            TestIntDiff("sinh x", "cosh x");
            TestIntDiff("sqrt x", "2(x^(3/2))/3");
            TestIntDiff("tan x", "log sec x");
            TestIntDiff("tanh x", "log cosh x");
        }

        private static void TestIntegrateFunctions()
        {
            TestIntegrate("ceiling x", "-ceiling x*(ceiling x-2x-1)/2");
            TestIntegrate("floor x", "-floor x*(floor x-2x+1)/2");
            //TestIntegrate("hstep x", "0");
            //TestIntegrate("round x", "0", false);
            //TestIntegrate("sign x", "0");
        }

        private static void TestParsers()
        {
            TestParse("-2^2", "4");
            TestParse("-2^2", "-4", Language.Maxima);
        }

        #endregion
    }
}
