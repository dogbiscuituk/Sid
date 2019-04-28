namespace Sid.Expressions
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;

    partial class Expressions
    {
        public static void Check(double expected, double actual) =>
            Check(expected.ToString(), actual.ToString());

        public static void Check(string expected, string actual)
        {
            const string message = "Comparison failed";
            var details = $"Expected: \"{expected}\", Actual: \"{actual}\".";
            if (actual == expected)
                Debug.WriteLine($"OK: {actual}");
            else
                Debug.WriteLine($"*** {message}. {details}");
            Debug.Assert(actual == expected, message, details);
        }

        public static void TestAll()
        {
            TestAlphaConversions();
            TestComparisons();
            TestCompoundExpression();
            TestTrigonometricExpression();
            TestSimplifications();
            TestFunctionDerivatives();
            TestPolynomialDerivative();
            TestChainRule();
            TestParser();
        }

        /// <summary>
        /// Check the alpha conversion functions correctly round-trip any transparency percent between 0 and 100.
        /// </summary>
        public static void TestAlphaConversions()
        {
            for (int percent = 0; percent <= 100; percent++)
                Check(percent, Utility.AlphaToTransparencyPercent(Utility.AlphaFromTransparencyPercent(percent)));
        }

        public static void TestChainRule()
        {
            TestDerivative(Exp(x.Squared()), "(Exp((x^2))*(x*2))");      // d(exp(x²))/dx = exp(x²)*2x
            TestDerivative(Ln(Sin(x)), "((1/Sin(x))*Cos(x))");           // d(ln(sin(x)))/dx = cot(x)
            TestDerivative(Tan(x.Cubed().Plus(8.Times(x))),
                "((Sec(((x^3)+(x*8)))^2)*(((x^2)*3)+8))");               // d(tan(x³+8x))/dx = sec²(x³+8x)*(3x²+8)
            TestDerivative(Sqrt(x.Power(4).Minus(1)),
                "((0.5/Sqrt(((x^4)-1)))*((x^3)*4))");                    // d(√(x⁴-1))/dx = 2x³/√(x⁴-1)
        }

        public static void TestComparisons()
        {
            Check("(x==10)", x.Equal(10).AsString());
            Check("(x==10)", x.Equal(10).AsString());
            Check("(x!=10)", x.NotEqual(10).AsString());
            Check("(x<10)", x.LessThan(10).AsString());
            Check("(x>10)", x.GreaterThan(10).AsString());
            Check("(x<=10)", x.LessThanOrEqual(10).AsString());
            Check("(x>=10)", x.GreaterThanOrEqual(10).AsString());
        }

        public static void TestCompoundExpression()
        {
            var f = x.Squared().Plus(3.Times(x)).Minus(5);               // f(x) = x²+3x-5
            Check("(((x^2)+(3*x))-5)", f.AsString());                    // Check the built expression formula
            Check(Math.Pow(7, 2) + 3 * 7 - 5, f.AsDouble(7));            // Check the expression value at x = 7 (should be 65)
        }

        public static void TestDerivative(Expression e, string expected) =>
            Check(expected, Differentiate(e).AsString());

        public static void TestFunctionDerivatives()
        {
            TestDerivative(Abs(x), "(x/Abs(x))");                        // d(|x|)/dx = x/|x|
            TestDerivative(Acos(x), "(-1/Sqrt((1-(x^2))))");             // d(acos x)/dx = -1/√(1-x²)
            TestDerivative(Acosh(x), "(1/Sqrt(((x^2)-1)))");             // d(acosh x)/dx = 1/√(x²-1),     x>1
            TestDerivative(Acot(x), "(-1/((x^2)+1))");                   // d(acot x)/dx = -1/(1+x²)
            TestDerivative(Acoth(x), "(1/(1-(x^2)))");                   // d(acoth x)/dx = 1/(1-x²),      |x|>1
            TestDerivative(Acsc(x), "(-1/(Abs(x)*Sqrt(((x^2)-1))))");    // d(acsc x)/dx = -1/|x|√(x²-1)
            TestDerivative(Acsch(x), "(-1/(x*Sqrt(((x^2)+1))))");        // d(acsch x)/dx = -1/x√(1+x²),   x≠0
            TestDerivative(Asec(x), "(1/(Abs(x)*Sqrt(((x^2)-1))))");     // d(asec x)/dx = 1/|x|√(x²-1)
            TestDerivative(Asech(x), "(-1/(x*Sqrt((1-(x^2)))))");        // d(asech x)/dx = -1/x√(1-x²),   0<x<1
            TestDerivative(Asin(x), "(1/Sqrt((1-(x^2))))");              // d(asin x)/dx = 1/√(1-x²)
            TestDerivative(Asinh(x), "(1/Sqrt(((x^2)+1)))");             // d(asinh x)/dx = 1/√(1+x²)
            TestDerivative(Atan(x), "(1/((x^2)+1))");                    // d(atan x)/dx = 1/(x²+1)
            TestDerivative(Atanh(x), "(1/(1-(x^2)))");                   // d(atanh x)/dx = 1/(1-x²),      |x|<1
            TestDerivative(Ceiling(x), "0");                             // d(ceiling x)/dx = 0
            TestDerivative(Cos(x), "-Sin(x)");                           // d(cos x)/dx = -sin x
            TestDerivative(Cosh(x), "Sinh(x)");                          // d(cosh x)/dx = sinh x
            TestDerivative(Cot(x), "-(Csc(x)^2)");                       // d(cot x)/dx = -csc²x
            TestDerivative(Coth(x), "-(Csch(x)^2)");                     // d(coth x)/dx = -csch²x,        x≠0
            TestDerivative(Csc(x), "-(Csc(x)*Cot(x))");                  // d(csc x)/dx = -csc x cot x
            TestDerivative(Csch(x), "-(Csch(x)*Coth(x))");               // d(csch x)/dx = -csch x coth x, x≠0
            TestDerivative(Erf(x), "(Exp(-(x^2))*1.12837916709551)");    // d(erf x)/dx = exp(-x²)*2/√π
            TestDerivative(Exp(x), "Exp(x)");                            // d(eˣ)/dx = eˣ
            TestDerivative(Floor(x), "0");                               // d(floor x)/dx = 0
            TestDerivative(Ln(x), "(1/x)");                              // d(ln x)/dx = 1/x
            TestDerivative(Log10(x), "(0.434294481903252/x)");           // d(log₁₀ x)/dx = log₁₀e/x
            TestDerivative(Round(x), "0");                               // d(round x)/dx = 0
            TestDerivative(Sec(x), "(Sec(x)*Tan(x))");                   // d(sec x)/dx = sec x tan x
            TestDerivative(Sech(x), "-(Sech(x)*Tanh(x))");               // d(sech x)/dx = -sech x tanh x
            TestDerivative(Sign(x), "0");                                // d(sign x)/dx = 0
            TestDerivative(Sin(x), "Cos(x)");                            // d(sin x)/dx = cos x
            TestDerivative(Sinh(x), "Cosh(x)");                          // d(sinh x)/dx = cosh x
            TestDerivative(Sqrt(x), "(0.5/Sqrt(x))");                    // d(√x)/dx = 1/(2√x)
            TestDerivative(Step(x), "0");                                // d(step x)/dx = 0
            TestDerivative(Tan(x), "(Sec(x)^2)");                        // d(tan x)/dx = sec²x
            TestDerivative(Tanh(x), "(Sech(x)^2)");                      // d(tanh x)/dx = sech²x
        }

        public static void TestPolynomialDerivative()
        {
            TestDerivative(x.Power(4).Minus(3.Times(x.Cubed())).Plus(6.Times(x.Squared())).Minus(3.Times(x)).Plus(1),
                "(((((x^3)*4)-((x^2)*9))+(x*12))-3)");                   // d(x⁴-3x³+6x²-3x+1)/dx = 4x³-9x²+12x-3
        }

        public static void TestSimplifications()
        {
            TestSimplify(x.Plus(6).Plus(2), "(x+8)");
            TestSimplify(6.Plus(x).Plus(2), "(x+8)");
            TestSimplify(6.Plus(x.Plus(2)), "(x+8)");
            TestSimplify(x.Plus(6).Minus(2), "(x+4)");
            TestSimplify(6.Plus(x).Minus(2), "(x+4)");
            TestSimplify(x.Minus(6).Plus(2), "(x-4)");
            TestSimplify(x.Minus(6).Minus(2), "(x-8)");
            TestSimplify(x.Times(6).Times(2), "(x*12)");
            TestSimplify(6.Times(x).Times(2), "(x*12)");
            TestSimplify(6.Times(x.Times(2)), "(x*12)");
            TestSimplify(x.Times(6).Over(2), "(x*3)");
            TestSimplify(6.Times(x).Over(2), "(x*3)");
            TestSimplify(x.Over(2).Times(8), "(x/0.25)");
            TestSimplify(x.Over(2).Over(8), "(x/16)");
        }

        public static void TestSimplify(Expression e, string expected) =>
            Check(expected, Simplify(e).AsString());

        public static void TestTrigonometricExpression()
        {
            var f = Sin(x).Squared().Plus(Cos(x).Squared());             // f(x) = sin²x+cos²x
            Check("((Sin(x)^2)+(Cos(x)^2))", f.AsString());              // Check the built expression formula
            double p3 = Math.PI / 3, s = Math.Sin(p3), c = Math.Cos(p3); // Check the expression value at x = π/3
            Check(Math.Pow(s, 2) + Math.Pow(c, 2), f.AsDouble(p3));      // (should be 1)
        }
    }
}
