namespace ToyGraf.Expressions
{
    using System.Diagnostics;

    partial class Expressions
    {
        #region Public Interface

        public static int TestMaxima()
        {
            return 0
            + RunDifferentiationTests()
            + RunIntegrationTests()
            + RunRepeatedDifferentiationTests()
            ;
        }

        internal static TestMode Mode { get; set; }

        internal enum TestMode
        {
            Testing,
            Analysing
        }

        #endregion

        #region Private Methods

        private static int Check(string source, string expected, string actual)
        {
            if (Mode == TestMode.Testing)
            {
                var ok = actual == expected;
                if (!ok)
                    Debug.WriteLine($"Expression: {source}, Expected: \"{expected}\", Actual: \"{actual}\".");
                Debug.Assert(ok);
            }
            return actual.Length;
        }

        private static int TestCalculus(string source, string expected, string target) =>
            Check(source, expected, target);

        private static int TestDifferentiation(string source, string expected) =>
            TestCalculus(source, expected, source.Differentiate());

        private static int TestIntegration(string source, string expected) =>
            TestCalculus(source, expected, source.Integrate());

        private static int RunDifferentiationTests()
        {
            return 0
            // Polynomials
            + TestDifferentiation("(x+1)^4", "4*x^3+12*x^2+12*x+4")
            + TestDifferentiation("x^4+4*x^3+6*x^2+4*x+1", "4*x^3+12*x^2+12*x+4")
            // Logarithms & Exponentials
            + TestDifferentiation("log(x)", "1/x")
            + TestDifferentiation("exp(x)", "%e^x")
            + TestDifferentiation("%e^x", "%e^x")
            // Trigonometric
            + TestDifferentiation("sin(x)", "cos(x)")
            + TestDifferentiation("cos(x)", "-sin(x)")
            + TestDifferentiation("tan(x)", "sec(x)^2")
            + TestDifferentiation("sec(x)", "sec(x)*tan(x)")
            + TestDifferentiation("csc(x)", "-cot(x)*csc(x)")
            + TestDifferentiation("cot(x)", "-csc(x)^2")
            // Inverse Trigonometric
            + TestDifferentiation("asin(x)", "1/sqrt(1-x^2)")
            + TestDifferentiation("acos(x)", "-1/sqrt(1-x^2)")
            + TestDifferentiation("atan(x)", "1/(x^2+1)")
            + TestDifferentiation("asec(x)", "1/(sqrt(x^2-1)*abs(x))")
            + TestDifferentiation("acsc(x)", "-1/(sqrt(x^2-1)*abs(x))")
            + TestDifferentiation("acot(x)", "-1/(x^2+1)")
            // Hyperbolic
            + TestDifferentiation("sinh(x)", "cosh(x)")
            + TestDifferentiation("cosh(x)", "sinh(x)")
            + TestDifferentiation("tanh(x)", "sech(x)^2")
            + TestDifferentiation("sech(x)", "-sech(x)*tanh(x)")
            + TestDifferentiation("csch(x)", "-coth(x)*csch(x)")
            + TestDifferentiation("coth(x)", "-csch(x)^2")
            // Inverse Hyperbolic
            + TestDifferentiation("asinh(x)", "1/sqrt(x^2+1)")
            + TestDifferentiation("acosh(x)", "1/sqrt(x^2-1)")
            + TestDifferentiation("atanh(x)", "-1/(x^2-1)")
            + TestDifferentiation("asech(x)", "-1/(sqrt(1-x^2)*abs(x))")
            + TestDifferentiation("acsch(x)", "-1/(sqrt(x^2+1)*abs(x))")
            + TestDifferentiation("acoth(x)", "-1/(x^2-1)")
            // Error Function
            + TestDifferentiation("erf(x)", "2*%e^-x^2/sqrt(%pi)")
            + TestDifferentiation("erfc(x)", "-2*%e^-x^2/sqrt(%pi)")
            + TestDifferentiation("erfi(x)", "2*%e^x^2/sqrt(%pi)")
            + TestDifferentiation("fresnel_c(x)", "cos(%pi*x^2/2)")
            + TestDifferentiation("fresnel_s(x)", "sin(%pi*x^2/2)")
            // Combinatorial
            + TestDifferentiation("factorial(x)", "x!*psi[0](x+1)")
            + TestDifferentiation("gamma(x)", "psi[0](x)*gamma(x)")
            + TestDifferentiation("log_gamma(x)", "psi[0](x)")
            // Miscellaneous
            + TestDifferentiation("abs(x)", "x/abs(x)")
            + TestDifferentiation("ceiling(x)", "'diff(ceiling(x),x,1)")
            + TestDifferentiation("entier(x)", "'diff(floor(x),x,1)")
            + TestDifferentiation("floor(x)", "'diff(floor(x),x,1)")
            + TestDifferentiation("fix(x)", "'diff(floor(x),x,1)")
            + TestDifferentiation("lambert_w(x)", "1/(%e^lambert_w(x)*lambert_w(x)+%e^lambert_w(x))")
            + TestDifferentiation("round(x)", "'diff('round(x),x,1)")
            + TestDifferentiation("signum(x)", "'diff(signum(x),x,1)")
            + TestDifferentiation("sqrt(x)", "1/(2*sqrt(x))")
            + TestDifferentiation("truncate(x)", "'diff('truncate(x),x,1)")
            ;
        }

        private static int RunIntegrationTests()
        {
            return 0
            // Polynomials
            + TestIntegration("(x+1)^4", "(x^5+5*x^4+10*x^3+10*x^2+5*x)/5")
            + TestIntegration("x^4+4*x^3+6*x^2+4*x+1", "(x^5+5*x^4+10*x^3+10*x^2+5*x)/5")
            // Logarithms & Exponentials
            + TestIntegration("log(x)", "x*log(x)-x")
            + TestIntegration("exp(x)", "%e^x")
            + TestIntegration("%e^x", "%e^x")
            // Trigonometric
            + TestIntegration("sin(x)", "-cos(x)")
            + TestIntegration("cos(x)", "sin(x)")
            + TestIntegration("tan(x)", "-log(cos(x))")
            + TestIntegration("sec(x)", "log(sec(x)*(sin(x)+1))")
            + TestIntegration("csc(x)", "-log(csc(x)+cot(x))")
            + TestIntegration("cot(x)", "log(sin(x))")
            // Inverse Trigonometric
            + TestIntegration("asin(x)", "x*asin(x)+sqrt(1-x^2)")
            + TestIntegration("acos(x)", "x*acos(x)-sqrt(1-x^2)")
            + TestIntegration("atan(x)", "-(log(x^2+1)-2*x*atan(x))/2")
            + TestIntegration("asec(x)", "-(log(sqrt(x^2-1)/abs(x)+1)-log(sqrt(x^2-1)/abs(x)-1)-2*x*asec(x))/2")
            + TestIntegration("acsc(x)", "(log(sqrt(x^2-1)/abs(x)+1)-log(sqrt(x^2-1)/abs(x)-1)+2*x*acsc(x))/2")
            + TestIntegration("acot(x)", "(log(x^2+1)+2*x*acot(x))/2")
            // Hyperbolic
            + TestIntegration("sinh(x)", "cosh(x)")
            + TestIntegration("cosh(x)", "sinh(x)")
            + TestIntegration("tanh(x)", "log(cosh(x))")
            + TestIntegration("sech(x)", "atan(sinh(x))")
            + TestIntegration("csch(x)", "log(tanh(x/2))")
            + TestIntegration("coth(x)", "log(sinh(x))")
            // Inverse Hyperbolic
            + TestIntegration("asinh(x)", "x*asinh(x)-sqrt(x^2+1)")
            + TestIntegration("acosh(x)", "x*acosh(x)-sqrt(x^2-1)")
            + TestIntegration("atanh(x)", "(log(1-x^2)+2*x*atanh(x))/2")
            + TestIntegration("asech(x)", "x*asech(x)-atan(sqrt(1-x^2)/abs(x))")
            + TestIntegration("acsch(x)", "(log(sqrt(x^2+1)/abs(x)+1)-log(sqrt(x^2+1)/abs(x)-1)+2*x*acsch(x))/2")
            + TestIntegration("acoth(x)", "(log(x^2-1)+2*x*acoth(x))/2")
            // Error Function
            + TestIntegration("erf(x)", "x*erf(x)+%e^-x^2/sqrt(%pi)")
            + TestIntegration("erfc(x)", "x*erfc(x)-%e^-x^2/sqrt(%pi)")
            + TestIntegration("erfi(x)", "x*erfi(x)-%e^x^2/sqrt(%pi)")
            + TestIntegration("fresnel_c(x)", "x*fresnel_c(x)-sin(%pi*x^2/2)/%pi")
            + TestIntegration("fresnel_s(x)", "cos(%pi*x^2/2)/%pi+x*fresnel_s(x)")
            // Combinatorial
            + TestIntegration("factorial(x)", "'integrate(x!,x)")
            + TestIntegration("gamma(x)", "'integrate(gamma(x),x)")
            + TestIntegration("log_gamma(x)", "'integrate(log_gamma(x),x)")
            // Miscellaneous
            + TestIntegration("abs(x)", "x*abs(x)/2")
            + TestIntegration("ceiling(x)", "-(ceiling(x)^2-2*x*ceiling(x)-ceiling(x))/2")
            + TestIntegration("entier(x)", "-(floor(x)^2-2*x*floor(x)+floor(x))/2")
            + TestIntegration("floor(x)", "-(floor(x)^2-2*x*floor(x)+floor(x))/2")
            + TestIntegration("fix(x)", "-(floor(x)^2-2*x*floor(x)+floor(x))/2")
            + TestIntegration("lambert_w(x)", "x*lambert_w(x)+x/lambert_w(x)-x")
            + TestIntegration("round(x)", "'integrate('round(x),x)")
            + TestIntegration("signum(x)", "'integrate(signum(x),x)")
            + TestIntegration("sqrt(x)", "2*x^(3/2)/3")
            + TestIntegration("truncate(x)", "'integrate('truncate(x),x)")
            ;
        }

        private static int RunRepeatedDifferentiationTests()
        {
            string[] expected = new[]
            {
                "sec(x)^2",
                "2*sec(x)^2*tan(x)",
                "sec(x)^4*(4-2*cos(2*x))",
                "sec(x)^5*(22*sin(x)-2*sin(3*x))",
                "-sec(x)^6*(-2*cos(4*x)+52*cos(2*x)-66)",
                "-sec(x)^7*(-2*sin(5*x)+114*sin(3*x)-604*sin(x))",
                "sec(x)^8*(-2*cos(6*x)+240*cos(4*x)-2382*cos(2*x)+2416)",
                "sec(x)^9*(-2*sin(7*x)+494*sin(5*x)-8586*sin(3*x)+31238*sin(x))",
                "-sec(x)^10*(-2*cos(8*x)+1004*cos(6*x)-29216*cos(4*x)+176468*cos(2*x)-156190)",
                "-sec(x)^11*(-2*sin(9*x)+2026*sin(7*x)-95680*sin(5*x)+910384*sin(3*x)-2620708*sin(x))"
            };
            var source = "tan(x)";
            var result = 0;
            for (int index = 0; index < expected.Length; index++)
                result += Check(source, expected[index], source = source.Differentiate());
            return result;
        }

        #endregion
    }
}
