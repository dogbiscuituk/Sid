namespace ToyGraf.Expressions
{
    using System.Diagnostics;
    using System.Linq.Expressions;

    partial class Expressions
    {
        #region Public Interface

        public static void TestAll()
        {
            Maxima.DebugOn();
            TestDiffFunctions();
            TestDiffPolynomials();
            TestIntegrateFunctions();
            Maxima.DebugOff();
        }

        #endregion

        #region Private Methods

        private static void Check(string source, string expected, string actual)
        {
            var ok = actual == expected;
            if (!ok)
                ok = Parser.TryParse(expected, out Expression m_expected, out _)
                    && Parser.TryParse(actual, out Expression m_actual, out _)
                    && m_actual.ToMaxima().IsEquivalentTo(m_expected.ToMaxima());
            if (!ok)
                Debug.WriteLine($"*** ERROR *** source: {source}; expected: {expected}; actual: {actual}.");
            Debug.Assert(ok);
        }

        private static void TestDiff(string source, string expected)
        {
            var e_source = Parser.Parse(source);
            var derivative = e_source.Differentiate();
            var actual = derivative.AsString();
            Check($"({source})'", expected, actual);
        }

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
            //TestDiff("exp x", "0");
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

        private static void TestIntegrate(string source, string expected, bool roundTrip = true)
        {
            var e_source = Parser.Parse(source);
            var integral = e_source.Integrate();
            var actual = integral.AsString();
            Check($"∫{source} dx", expected, actual);
            if (roundTrip)
                TestDiff(source: actual, expected: source);
        }

        private static void TestIntegrateFunctions()
        {
            Debug.WriteLine("########################################## TestIntegrateFunctions()");
            TestIntegrate("abs x", "x*abs x/2");
            TestIntegrate("acos x", "x*acos x-√(1-x²)");
            TestIntegrate("acosh x", "x*acosh x-√(x²-1)");
            TestIntegrate("acot x", "(log(x²+1)+2x*acot x)/2");
            TestIntegrate("acoth x", "(log(x²-1)+2x*acoth x)/2");
            TestIntegrate("acsc x", "(log((abs x+√(x²-1))/abs x)-log((√(x²-1)-abs x)/abs x)+2x*acsc x)/2");
            TestIntegrate("acsch x", "(log((abs x+√(x²+1))/abs x)-log((√(x²+1)-abs x)/abs x)+2x*acsch x)/2");
            TestIntegrate("asec x", "-(log((abs x+√(x²-1))/abs x)-log((√(x²-1)-abs x)/abs x)-2x*asec x)/2");
            TestIntegrate("asech x", "-(atan(√(1-x²)/abs x)-x*asech x)");
            TestIntegrate("asin x", "x*asin x+√(1-x²)");
            TestIntegrate("asinh x", "x*asinh x-√(x²+1)");
            TestIntegrate("atan x", "-(log(x²+1)-2x*atan x)/2");
            TestIntegrate("atanh x", "(log(1-x²)+2x*atanh x)/2");
            TestIntegrate("ceiling x", "-ceiling x*(ceiling x-2x-1)/2", false);
            TestIntegrate("cos x", "sin x");
            TestIntegrate("cosh x", "sinh x");
            TestIntegrate("cot x", "log sin x");
            TestIntegrate("coth x", "log sinh x");
            TestIntegrate("csc x", "-(log(cos x+1)-log(cos x-1))/2", false);
            //TestIntegrate("csch x", "0");
            //TestIntegrate("exp x", "0");
            TestIntegrate("floor x", "-floor x*(floor x-2x+1)/2", false);
            //TestIntegrate("hstep x", "0");
            TestIntegrate("log x", "x*(log x-1)");
            //TestIntegrate("log10 x", "0");
            //TestIntegrate("round x", "0", false);
            TestIntegrate("sec x", "(log(sin x+1)-log(sin x-1))/2", false);
            //TestIntegrate("sech x", "0");
            //TestIntegrate("sign x", "0");
            TestIntegrate("sin x", "-cos x");
            TestIntegrate("sinh x", "cosh x");
            TestIntegrate("sqrt x", "2(x^(3/2))/3");
            TestIntegrate("tan x", "-log cos x");
            TestIntegrate("tanh x", "log cosh x");
        }

        #endregion

        #region Private Properties

        private static Parser Parser = new Parser();

        #endregion
    }
}
