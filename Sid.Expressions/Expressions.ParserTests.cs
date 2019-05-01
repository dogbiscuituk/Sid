namespace Sid.Expressions
{
    using System;

    partial class Expressions
    {

        public static void TestParse(string input, string output) =>
            Check(output, new Parser().Parse(input).AsString());

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

        public static void TestParser()
        {
            TestParserFailure();
            TestParserSuccess();
        }

        public static void TestParserFailure()
        {
            TestParseFail("x~2", "Unexpected token '~', input='x~2', index=1");
            TestParseFail("x+123,456", "Unexpected character ',', input='x+123,456', index=5");
            TestParseFail("x+1$2", "Unexpected character '$', input='x+1$2', index=3");
            TestParseFail("x+1e999", "Numerical overflow '1e999', input='x+1e999', index=2");
            TestParseFail("x+.", "Invalid number format '.', input='x+.', index=2");
            TestParseFail("x+.E+1", "Invalid number format '.E+1', input='x+.E+1', index=2");
            TestParseFail("x+", "Missing operand, input='x+', index=2");
            TestParseFail("(x+(2*(x+(3)))", "Unexpected character '$', input='(x+(2*(x+(3)))', index=15");
            TestParseFail("2 sin x cos x", "Unexpected token 'cos', input='2 sin x cos x', index=8");
        }

        public static void TestParserSuccess()
        {
            TestParse("0", "0");
            TestParse("e", "2.71828182845905");
            TestParse("π", "3.14159265358979");
            TestParse("pi", "3.14159265358979");
            TestParse("ϕ", "1.61803398874989");
            TestParse("phi", "1.61803398874989");
            TestParse("-x", "-x");
            TestParse("X+1", "(x+1)");
            TestParse("((x+1))", "(x+1)");
            TestParse("x+x*x^x/x-x", "((x+((x*(x^x))/x))-x)");
            TestParse("3*x+x/5", "((3*x)+(x/5))");
            TestParse("x-2-x", "((x-2)-x)");                             // Subtraction is left associative
            TestParse("x^2^x", "(x^(2^x))");                             // Exponentiation is right associative
            TestParse("(x-3)*(5-x)/10", "(((x-3)*(5-x))/10)");
            TestParse("sin x * cos x", "(Sin(x)*Cos(x))");
            TestParse("Ln(sin x - tanh(x)) - 1", "(Ln((Sin(x)-Tanh(x)))-1)");
            TestParse("Abs Cos Sin Tan 1.5", "0.540839774154307");
            TestParse("Abs Cos Sin Tan (x/2)", "Abs(Cos(Sin(Tan((x/2)))))");
            TestParse("2*(sin x + cos x ^ 3 - tan(x^3))/3", "((2*((Sin(x)+(Cos(x)^3))-Tan((x^3))))/3)");
            TestParse("2*(x+3*(x-4^x)-5)/6", "((2*((x+(3*(x-(4^x))))-5))/6)");
            TestParse("1/5x", "(1/(5*x))");                              // Implied products have higher precedence
            TestParse("1/2sqrt(x)", "(1/(2*Sqrt(x)))");
            TestParse("2 sin 3x", "(2*Sin((3*x)))");
            TestParse("2 sin 3x * 5 cos 7x", "((2*Sin((3*x)))*(5*Cos((7*x))))");
            TestParse("2 3", "(2*3)");
            TestParse("2(x+3)", "(2*(x+3))");
            TestParse("2x^3)", "((2*x)^3)");
            TestParse("2(x^3)", "(2*(x^3))");
            TestParse("x⁴-4x³+6x²-4x+1", "(((((x^4)-(4*(x^3)))+(6*(x^2)))-(4*x))+1)");
            TestParse("1/2√(1-x²)", "(1/(2*Sqrt((1-(x^2)))))");
            TestParse("eˣ", "(2.71828182845905^x)");
            TestParse("eᶜᵒˢ⁽ˣ⁾", "(2.71828182845905^Cos(x))");
            TestParse("x'", "1");                                        // d(x)/dx = 1
            TestParse("(sin x)'", "Cos(x)");                             // d(sin x)/dx = cos x
            TestParse("(x³)'", "((x^2)*3)");                             // d(x³)/dx = 3x²
            TestParse("(exp(cos x))'", "(Exp(Cos(x))*-Sin(x))");         // d(eᶜᵒˢ⁽ˣ⁾)/dx = -(sin x)eᶜᵒˢ⁽ˣ⁾
            TestParse("(sin x)''", "-Sin(x)");                           // d²(sin x)/dx² = -sin x
            TestParse("x<1|x>2&x<3", "((x<1)Or((x>2)And(x<3)))");        // Precedence('&') > Precedence('|')
            TestParse("x<1|x>2&&x<3", "(((x<1)Or(x>2))And(x<3))");       // Precedence('&&') < Precedence('|')
            TestParse("x<1||x>2&&x<3", "((x<1)Or((x>2)And(x<3)))");      // Precedence('&&') > Precedence('||')
            TestParse("(x>0)*sin x", "(IIF((x>0),1,0)*Sin(x))");
            TestParse("x<0 ? sin x : tan x", "IIF((x<0),Sin(x),Tan(x))");
            TestParse("x<2 ? (x<1 ? 0 : 1) : (x>3 ? 3 : 2)", "IIF((x<2),IIF((x<1),0,1),IIF((x>3),3,2))");
            TestParse("x<2 ? x<1 ? 0 : 1 : x>3 ? 3 : 2", "IIF((x<2),IIF((x<1),0,1),IIF((x>3),3,2))");
            TestParse("0<x<1", "((0<x)And(x<1))");
            TestParse("0<x<2x<1", "(((0<x)And(x<(2*x)))And((2*x)<1))");
        }
    }
}
