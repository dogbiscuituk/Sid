namespace ToyGraf.Expressions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    partial class Expressions
    {
        public static void TestParse(string input, string expected)
        {
            var inputs = input.Split(';');
            var expressions = new Expression[inputs.Length];
            for (int index = 0; index < inputs.Length; index++)
                expressions[index] =
                    new Parser().TryParse(inputs[index], out object result)
                    ? (Expression)result : Expressions.DefaultVoid;
            var proxies = expressions.Select(p => p.AsProxy(x, t, expressions));
            var actual = proxies.Select(p => p.AsString()).Aggregate((s, t) => $"{s};{t}");
            Check(expected, actual);
        }

        public static void TestParseFail(string input, string error)
        {
            try
            {
                new Parser().Parse(input);
                Check("Exception thrown", "no Exception thrown");
            }
            catch (Exception ex)
            {
                Check(error, ex.Message);
            }
        }

        public static void TestParserIce()
        {
            TestParserFailure();
            TestParserSuccessIce();
        }

        public static void TestParserMaxima()
        {
            TestParserFailure();
            TestParserSuccessMaxima();
        }

        public static void TestParserFailure()
        {
            TestParseFail("x~2", "Unexpected token '~', input='x~2', index=1");
            TestParseFail("x+1$2", "Unexpected character '$', input='x+1$2', index=3");
            TestParseFail("x+1e999", "Numerical overflow '1e999', input='x+1e999', index=2");
            TestParseFail("x+.", "Invalid number format '.', input='x+.', index=2");
            TestParseFail("x+.E+1", "Invalid number format '.E+1', input='x+.E+1', index=2");
            TestParseFail("x+", "Missing operand, input='x+', index=2");
            TestParseFail("(x+(2*(x+(3)))", "Unexpected character '$', input='(x+(2*(x+(3)))', index=15");
            TestParseFail("2 sin x cos x", "Unexpected token 'cos', input='2 sin x cos x', index=8");
        }

        public static void TestParserSuccessCommon()
        {
            TestParse("0", "0");
            TestParse("e", "2.71828182845905");
            TestParse("π", "3.14159265358979");
            TestParse("pi", "3.14159265358979");
            TestParse("ϕ", "1.61803398874989");
            TestParse("phi", "1.61803398874989");
            TestParse("-x", "-x");
            TestParse("X+1", "x+1");
            TestParse("((x+1))", "x+1");
            TestParse("x+2t", "x+2t");
            TestParse("t+2x", "t+2x");
            TestParse("x+x*x^x/x-x", "x+x*x^x/x-x");
            TestParse("3*x+x/5", "3x+x/5");
            TestParse("x-2-x", "x-2-x");                                 // Subtraction is left associative
            TestParse("x-(2+x)", "x-2-x");
            TestParse("x-(2-x)", "x-2+x");
            TestParse("x/2/x", "x/2/x");                                 // Division is left associative
            TestParse("x/(2*x)", "x/2/x");
            TestParse("x/(2/x)", "x/2*x");
            TestParse("x^2^x", "x^2^x");                                 // Exponentiation is right associative
            TestParse("x^(2^x)", "x^2^x");
            TestParse("(x^2)^x", "x^(2x)");
            TestParse("(x-3)*(5-x)/10", "(x-3)*(5-x)/10");
            TestParse("sin x * cos x", "Sin x*Cos x");
            TestParse("Ln(sin x - tanh(x)) - 1", "Ln(Sin x-Tanh x)-1");
            TestParse("Abs Cos Sin Tan 1.5", "0.540839774154307");
            TestParse("Abs Cos Sin Tan (x/2)", "Abs Cos Sin Tan(x/2)");
            TestParse("2*(sin x + cos x ^ 3 - tan(x^3))/3", "2(Sin x+Cos³x-Tan x³)/3");
            TestParse("2(sin x + cos³x - tan x³)/3", "2(Sin x+Cos³x-Tan x³)/3");
            TestParse("2*(x+3*(x-4^x)-5)/6", "2(x+3(x-4^x)-5)/6");
            TestParse("1/5x", "1/5/x");                                  // Implied products have higher precedence
            TestParse("1/2√(x)", "1/2/√x");
            TestParse("sin(x-120°)", "Sin(x-2.0943951023932)");          // Convert degrees to radians
            TestParse("2 sin 3x", "2Sin((3x))");
            TestParse("2 sin 3x * 5 cos 7x", "2Sin((3x))*5Cos((7x))");
            TestParse("2 3", "2*3");
            TestParse("2(x+3)", "2(x+3)");
            TestParse("2x^3)", "(2x)³");
            TestParse("2(x^3)", "2x³");
            TestParse("x⁴-4x³+6x²-4x+1", "x⁴-4x³+6x²-4x+1");
            TestParse("1/2√(1-x²)", "1/2/√(1-x²)");
            TestParse("eˣ", "2.71828182845905^x");
            TestParse("eᶜᵒˢ⁽ˣ⁾", "2.71828182845905^Cos x");
            TestParse("x'", "1");                                        // d(x)/dx = 1
            TestParse("(sin x)'", "Cos x");                              // d(sin x)/dx = cos x
            TestParse("(sin x)''", "-Sin x");                            // d²(sin x)/dx² = -sin x
            TestParse("x<1|x>2&x<3", "x<1|x>2&x<3");                     // Precedence('&') > Precedence('|')
            TestParse("x<1|x>2&&x<3", "x<1|x>2&&x<3");                   // Precedence('&&') < Precedence('|')
            TestParse("x<1||x>2&&x<3", "x<1||x>2&&x<3");                 // Precedence('&&') > Precedence('||')
            TestParse("(x>0)*sin x", "(x>0?1:0)*Sin x");
            TestParse("x<0 ? sin x : tan x", "x<0?Sin x:Tan x");
            TestParse("x<2 ? (x<1 ? 0 : 1) : (x>3 ? 3 : 2)", "x<2?x<1?0:1:x>3?3:2");
            TestParse("x<2 ? x<1 ? 0 : 1 : x>3 ? 3 : 2", "x<2?x<1?0:1:x>3?3:2");
            TestParse("0<x<1", "0<x&x<1");
            TestParse("0<x<2x<1", "0<x&x<2x&2x<1");
            TestParse("t*x", "t*x");
            TestParse("x⁴-4x³*t+6x²*t²-4x*t³+t⁴)", "x⁴-4x³*t+6x²*t²-4x*t³+t⁴");
            TestParse("sin x;f0(x)", "Sin x;Sin x");
            TestParse("sin x;(f0(x))'", "Sin x;Cos x");
            TestParse("sin(x+t);cos(x-t);f0(x,t)+f1(x,t)", "Sin(x+t);Cos(x-t);Sin(x+t)+Cos(x-t)");
            TestParse("(x+t)^3;(x-t)^5;(f0(x,t))'+(f1(x,t))''", "(x+t)³;(x-t)⁵;(x+t)²*3+(x-t)³*20");
            TestParse("sin²x", "Sin²x");
            TestParse("4sec²x * tan²x + 2sec⁴x", "4Sec²x*Tan²x+2Sec⁴x");
        }

        private static void TestParserSuccessIce()
        {
            TestParserSuccessCommon();
            TestParse("(x³)'", "x²*3");                                  // d(x³)/dx = 3x²
            TestParse("(exp(cos x))'", "Exp Cos x*-Sin x");              // d(eᶜᵒˢ⁽ˣ⁾)/dx = -(sin x)eᶜᵒˢ⁽ˣ⁾
            TestParse("(x⁴-4x³*t+6x²*t²-4x*t³+t⁴)'", "x³*4-x²*12*t+x*12*t²-t³*4");
        }

        private static void TestParserSuccessMaxima()
        {
            TestParserSuccessCommon();
            TestParse("(x³)'", "3x²");                                   // d(x³)/dx = 3x²
            TestParse("(exp(cos x))'", "-2.71828182845905^Cos x*Sin x"); // d(eᶜᵒˢ⁽ˣ⁾)/dx = -(sin x)eᶜᵒˢ⁽ˣ⁾
            TestParse("(x⁴-4x³*t+6x²*t²-4x*t³+t⁴)'", "4x³-12t*x²+12t²*x-4t³");
        }
    }
}
