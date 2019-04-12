namespace FormulaBuilder
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;

    public static class Differentiator
    {
        public static ParameterExpression x = Expression.Variable(typeof(double));

        public static ConstantExpression Constant(double c) => Expression.Constant(c);

        public static string AsString(this Expression e, string variableName = "x") =>
            e.ToString().Replace("Param_0", variableName);

        public static Func<double, double> AsFunction(this Expression e) =>
            Expression.Lambda<Func<double, double>>(e, x).Compile();

        public static double AsDouble(this Expression e, double x) => AsFunction(e)(x);

        public static void Check(double expected, double actual) =>
            Check(expected.ToString(), actual.ToString());

        public static void Check(string expected, string actual)
        {
            if (actual == expected)
                Debug.WriteLine("OK: " + actual);
            else
                Debug.WriteLine("*** Comparison failed. Expected: {0}, Actual: {1}.", expected, actual);
        }

        public static BinaryExpression Plus(this Expression f, Expression g) => Expression.Add(f, g);
        public static BinaryExpression Minus(this Expression f, Expression g) => Expression.Subtract(f, g);
        public static BinaryExpression Times(this Expression f, Expression g) => Expression.Multiply(f, g);
        public static BinaryExpression Over(this Expression f, Expression g) => Expression.Divide(f, g);
        public static BinaryExpression Power(this Expression f, Expression g) => Expression.Power(f, g);

        public static BinaryExpression Plus(this Expression f, double g) => f.Plus(Constant(g));
        public static BinaryExpression Minus(this Expression f, double g) => f.Minus(Constant(g));
        public static BinaryExpression Times(this Expression f, double g) => f.Times(Constant(g));
        public static BinaryExpression Over(this Expression f, double g) => f.Over(Constant(g));
        public static BinaryExpression Power(this Expression f, double g) => f.Power(Constant(g));

        public static BinaryExpression Plus(this double f, Expression g) => Constant(f).Plus(g);
        public static BinaryExpression Minus(this double f, Expression g) => Constant(f).Minus(g);
        public static BinaryExpression Times(this double f, Expression g) => Constant(f).Times(g);
        public static BinaryExpression Over(this double f, Expression g) => Constant(f).Over(g);
        public static BinaryExpression Power(this double f, Expression g) => Constant(f).Power(g);

        public static BinaryExpression Plus(this int f, Expression g) => Constant(f).Plus(g);
        public static BinaryExpression Minus(this int f, Expression g) => Constant(f).Minus(g);
        public static BinaryExpression Times(this int f, Expression g) => Constant(f).Times(g);
        public static BinaryExpression Over(this int f, Expression g) => Constant(f).Over(g);
        public static BinaryExpression Power(this int f, Expression g) => Constant(f).Power(g);

        public static UnaryExpression Negate(this Expression e) => Expression.Negate(e);
        public static BinaryExpression Reciprocal(this Expression e) => e.Power(-1);
        public static BinaryExpression Squared(this Expression e) => e.Power(2);
        public static BinaryExpression Cubed(this Expression e) => e.Power(3);

        public static void TestCompoundExpression()
        {
            var f = x.Squared().Plus(3.Times(x)).Minus(5);    // f(x) = x²+3x-5
            Check("(((x ^ 2) + (3 * x)) - 5)", f.AsString()); // Check the built expression formula
            Check(Math.Pow(7, 2) + 3 * 7 - 5, f.AsDouble(7)); // Check the expression value at x = 7 (should be 65)
        }

        public static MethodCallExpression Function(string functionName, Expression e) =>
            Expression.Call(typeof(Functions).GetMethod(functionName, new[] { typeof(double) }), e);

        public static MethodCallExpression Abs(this Expression e) => Function("Abs", e);
        public static MethodCallExpression Acos(this Expression e) => Function("Acos", e);
        public static MethodCallExpression Asin(this Expression e) => Function("Asin", e);
        public static MethodCallExpression Atan(this Expression e) => Function("Atan", e);
        public static MethodCallExpression Ceiling(this Expression e) => Function("Ceiling", e);
        public static MethodCallExpression Cos(this Expression e) => Function("Cos", e);
        public static MethodCallExpression Cosh(this Expression e) => Function("Cosh", e);
        public static MethodCallExpression Exp(this Expression e) => Function("Exp", e);
        public static MethodCallExpression Floor(this Expression e) => Function("Floor", e);
        public static MethodCallExpression Log(this Expression e) => Function("Log", e);
        public static MethodCallExpression Log10(this Expression e) => Function("Log10", e);
        public static MethodCallExpression Round(this Expression e) => Function("Round", e);
        public static MethodCallExpression Sign(this Expression e) => Function("Sign", e);
        public static MethodCallExpression Sin(this Expression e) => Function("Sin", e);
        public static MethodCallExpression Sinh(this Expression e) => Function("Sinh", e);
        public static MethodCallExpression Sqrt(this Expression e) => Function("Sqrt", e);
        public static MethodCallExpression Step(this Expression e) => Function("Step", e);
        public static MethodCallExpression Tan(this Expression e) => Function("Tan", e);
        public static MethodCallExpression Tanh(this Expression e) => Function("Tanh", e);

        public static MethodCallExpression Acot(this Expression e) => Atan(Reciprocal(e));
        public static MethodCallExpression Acsc(this Expression e) => Asin(Reciprocal(e));
        public static MethodCallExpression Asec(this Expression e) => Acos(Reciprocal(e));
        public static BinaryExpression Cot(this Expression e) => Reciprocal(Tan(e));
        public static BinaryExpression Coth(this Expression e) => Reciprocal(Tanh(e));
        public static BinaryExpression Csc(this Expression e) => Reciprocal(Sin(e));
        public static BinaryExpression Csch(this Expression e) => Reciprocal(Sinh(e));
        public static BinaryExpression Sec(this Expression e) => Reciprocal(Cos(e));
        public static BinaryExpression Sech(this Expression e) => Reciprocal(Cosh(e));

        public static MethodCallExpression Acosh(this Expression e) => Log(e.Plus(Sqrt(e.Squared().Minus(1))));
        public static BinaryExpression Acoth(this Expression e) => Log(e.Plus(1).Over(e.Minus(1))).Over(2);
        public static MethodCallExpression Acsch(this Expression e) => Log(1.Plus(Sqrt(1.Plus(e.Squared()))).Over(e));
        public static MethodCallExpression Asech(this Expression e) => Log(1.Plus(Sqrt(1.Minus(e.Squared()))).Over(e));
        public static MethodCallExpression Asinh(this Expression e) => Log(e.Plus(Sqrt(e.Squared().Plus(1))));
        public static BinaryExpression Atanh(this Expression e) => Log(1.Plus(e).Over(1.Minus(e))).Over(2);

        public static void TestTrigonometricExpression()
        {
            var f = Sin(x).Squared().Plus(Cos(x).Squared());             // f(x) = sin²x+cos²x
            Check("((Sin(x) ^ 2) + (Cos(x) ^ 2))", f.AsString());        // Check the built expression formula
            double p3 = Math.PI / 3, s = Math.Sin(p3), c = Math.Cos(p3); // Check the expression value at x = π/3
            Check(Math.Pow(s, 2) + Math.Pow(c, 2), f.AsDouble(p3));      // (should be 1)
        }

        public static Expression Call(this Expression e, Func<double, double> func)
        {
            Expression<Func<double, double>> f = x => func(x);
            return f;
        }

        public static Expression D(this Expression e)
        {
            switch (e)
            {
                case ConstantExpression c: // d(c)/dx = 0
                    return Constant(0);
                case ParameterExpression p: // d(x)/dx = 1
                    return Constant(1);
                case MethodCallExpression m:
                    var a = m.Arguments[0];
                    return DifferentiateFunction(m.Method.Name, a).Times(D(a)); // Chain Rule
                case UnaryExpression u:
                    var v = D(u.Operand);
                    return u.NodeType == ExpressionType.UnaryPlus ? v : Negate(v);
                case BinaryExpression b:
                    Expression f = b.Left, g = b.Right;
                    switch (b.NodeType)
                    {
                        case ExpressionType.Add:      // (f+g)' = f'+g'
                            return D(f).Plus(D(g));
                        case ExpressionType.Subtract: // (f-g)' = f'-g'
                            return D(f).Minus(D(g));
                        case ExpressionType.Multiply: // (fg)' = f'g+fg'
                            return D(f).Times(g).Plus(f.Times(D(g)));
                        case ExpressionType.Divide:   // (f÷g)' = (f'g-fg')÷(g^2) = f'÷g-fg'÷(g^2)
                            return D(f).Over(g).Minus(f.Times(D(g)).Over(g.Squared()));
                        case ExpressionType.Power:    // (f^g)' = (f^g)*(f'g÷f+g'Log(f)) = (f'g+fg'Log(f))f^(g-1)
                            return D(f).Times(g).Plus(f.Times(D(g)).Times(Log(f))).Times(f.Power(g.Minus(1)));
                    }
                    break;
            }
            throw new InvalidOperationException();
        }

        private static Expression DifferentiateFunction(string methodName, Expression x)
        {
            switch (methodName)
            {
                case "Abs": return x.Over(Abs(x));                            // d(|x|)/dx = x/|x|
                case "Sqrt": return x.Power(-0.5).Over(2);                    // d(√x)/dx = 1/(2√x)
                case "Exp": return Exp(x);                                    // d(eˣ)/dx = eˣ
                case "Log": return Reciprocal(x);                             // d(ln(x))/dx = 1/x
                case "Log10": return Reciprocal(x).Times(Math.Log10(Math.E)); // d(log₁₀(x))/dx = log₁₀(e)/x
                case "Sin": return Cos(x);                                    // d(sin(x))/dx = cos(x)
                case "Cos": return Negate(Sin(x));                            // d(cos(x))/dx = -sin(x)
                case "Tan": return Cos(x).Power(-2);                          // d(tan(x))/dx = sec²(x)
                case "Asin": return 1.Minus(x.Squared()).Power(-0.5);         // d(arcsin(x))/dx = 1/√(1-x²)
                case "Acos": return Negate(1.Minus(x.Squared()).Power(-0.5)); // d(arccos(x))/dx = -1/√(1-x²)
                case "Atan": return 1.Over(1.Plus(x.Squared()));              // d(arctan(x))/dx = 1/(1+x²)
                case "Sinh": return Cosh(x);                                  // d(sinh(x))/dx = cosh(x)
                case "Cosh": return Sinh(x);                                  // d(cosh(x))/dx = sinh(x)
                case "Tanh": return 1.Minus(Tanh(x).Squared());               // d(tanh(x))/dx = 1-tanh²(x)
                default: return Constant(0);
            }
            throw new InvalidOperationException();
        }

        public static Expression Differentiate(this Expression e) => Simplify(D(e));

        public static Expression Simplify(this Expression e)
        {
            switch (e)
            {
                case MethodCallExpression m:
                    return SimplifyMethodCall(m);
                case UnaryExpression u:
                    return SimplifyUnary(u);
                case BinaryExpression b:
                    return SimplifyBinary(b);
            }
            return e;
        }

        public static Expression SimplifyMethodCall(MethodCallExpression m)
        {
            var operand = Simplify(m.Arguments[0]);
            if (operand is ConstantExpression ce)
            {
                var c = (double)ce.Value;
                return Constant(Function(m.Method.Name, x).AsDouble(c));
            }
            return Function(m.Method.Name, operand);
        }

        public static Expression SimplifyUnary(UnaryExpression u)
        {
            var operand = Simplify(u.Operand);
            if (operand is ConstantExpression ce)
            {
                var c = (double)ce.Value;
                return Constant(u.NodeType == ExpressionType.UnaryPlus ? c : -c);
            }
            return u.NodeType == ExpressionType.UnaryPlus ? operand : Negate(operand);
        }

        public static Expression SimplifyBinary(BinaryExpression b)
        {
            b = Expression.MakeBinary(b.NodeType, Simplify(b.Left), Simplify(b.Right));
            if (b.Left is ConstantExpression ce1)
            {
                var c1 = (double)ce1.Value;
                if (b.Right is ConstantExpression ce2)
                    return Constant(b.NodeType, c1, (double)ce2.Value);
                switch (b.NodeType)
                {
                    case ExpressionType.Add:                   // c + x → x + c
                    case ExpressionType.Multiply:              // c * x → x * c
                        b = Expression.MakeBinary(b.NodeType, b.Right, b.Left);
                        break;
                    case ExpressionType.Subtract:
                        if (c1 == 0) return Negate(b.Right);   // 0 - x → -x
                        break;
                    case ExpressionType.Divide:
                        if (c1 == 0) return Constant(0);       // 0 ÷ x → 0
                        break;
                    case ExpressionType.Power:
                        if (c1 == 0) return Constant(0);       // 0 ^ x → 0
                        if (c1 == 1) return Constant(1);       // 1 ^ x → 1
                        break;
                }
            }
            if (b.Right is ConstantExpression ce3)
            {
                var c3 = (double)ce3.Value;
                switch (b.NodeType)
                {
                    case ExpressionType.Add:
                    case ExpressionType.Subtract:
                        if (c3 == 0) return b.Left;            // x ± 0 → x
                        break;
                    case ExpressionType.Multiply:
                        if (c3 == 0) return Constant(0);       // x * 0 → 0
                        if (c3 == 1) return b.Left;            // x * 1 → x
                        if (c3 == -1) return Negate(b.Left);   // x * -1 → -x
                        break;
                    case ExpressionType.Divide:
                        if (c3 == 0)
                            throw new DivideByZeroException();
                        if (c3 == 1) return b.Left;            // x ÷ 1 → x
                        if (c3 == -1) return Negate(b.Left);   // x ÷ -1 → -x
                        break;
                    case ExpressionType.Power:
                        if (c3 == 0) return Constant(1);       // x ^ 0 → 1
                        if (c3 == 1) return b.Left;            // x ^ 1 → x
                        break;
                }
                switch (b.NodeType)
                {
                    case ExpressionType.Add:                   // (x + c1) ± c2 → x + (c1 ± c2)
                    case ExpressionType.Subtract:              // (x - c1) ± c2 → x - (c1 ∓ c2)
                    case ExpressionType.Multiply:              // (x * c1) */ c2 → x * (c1 */ c2)
                    case ExpressionType.Divide:                // (x / c1) */ c2 → x / (c1 /* c2)
                        if (!(b.Left is BinaryExpression c && c.Right is ConstantExpression ce4))
                            break;
                        bool same = c.NodeType == b.NodeType, opps = c.NodeType == Invert(b.NodeType);
                        if (!(same || opps))
                            break;
                        var c4 = (double)ce4.Value;
                        var nodeType = c.NodeType == ExpressionType.Add || c.NodeType == ExpressionType.Subtract
                            ? ExpressionType.Add
                            : ExpressionType.Multiply;
                        if (opps) nodeType = Invert(nodeType);
                        return Expression.MakeBinary(c.NodeType, c.Left, Constant(nodeType, c4, c3));
                }
            }
            return b;
        }

        public static ExpressionType Invert(this ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Add: return ExpressionType.Subtract;
                case ExpressionType.Subtract: return ExpressionType.Add;
                case ExpressionType.Multiply: return ExpressionType.Divide;
                case ExpressionType.Divide: return ExpressionType.Multiply;
            }
            throw new InvalidOperationException();
        }

        public static ConstantExpression Constant(ExpressionType nodeType, double c, double d) =>
            Constant(Apply(nodeType, c, d));

        public static double Apply(ExpressionType nodeType, double c, double d)
        {
            switch (nodeType)
            {
                case ExpressionType.Add: return c + d;
                case ExpressionType.Subtract: return c - d;
                case ExpressionType.Multiply: return c * d;
                case ExpressionType.Divide:
                    if (d == 0)
                        throw new DivideByZeroException();
                    return c / d;
                case ExpressionType.Power:
                    if (c == 0 && d == 0)
                        throw new InvalidOperationException();
                    return Math.Pow(c, d);
            }
            throw new InvalidOperationException();
        }

        public static void TestSimplify(Expression e, string expected) => Check(expected, Simplify(e).AsString());

        public static void TestSimplifications()
        {
            TestSimplify(x.Plus(6).Plus(2), "(x + 8)");
            TestSimplify(6.Plus(x).Plus(2), "(x + 8)");
            TestSimplify(6.Plus(x.Plus(2)), "(x + 8)");
            TestSimplify(x.Plus(6).Minus(2), "(x + 4)");
            TestSimplify(6.Plus(x).Minus(2), "(x + 4)");
            TestSimplify(x.Minus(6).Plus(2), "(x - 4)");
            TestSimplify(x.Minus(6).Minus(2), "(x - 8)");
            TestSimplify(x.Times(6).Times(2), "(x * 12)");
            TestSimplify(6.Times(x).Times(2), "(x * 12)");
            TestSimplify(6.Times(x.Times(2)), "(x * 12)");
            TestSimplify(x.Times(6).Over(2), "(x * 3)");
            TestSimplify(6.Times(x).Over(2), "(x * 3)");
            TestSimplify(x.Over(2).Times(8), "(x / 0.25)");
            TestSimplify(x.Over(2).Over(8), "(x / 16)");
        }

        public static void TestDerivative(Expression e, string expected) => Check(expected, Differentiate(e).AsString());

        public static void TestFunctionDerivatives()
        {
            TestDerivative(Abs(x), "(x / Abs(x))");                       // d(|x|)/dx = x/|x|
            TestDerivative(Sqrt(x), "((x ^ -0.5) / 2)");                  // d(√x)/dx = 1/(2√x)
            TestDerivative(Exp(x), "Exp(x)");                             // d(eˣ)/dx = eˣ
            TestDerivative(Log(x), "(x ^ -1)");                           // d(ln(x))/dx = 1/x
            TestDerivative(Log10(x), "((x ^ -1) * 0.434294481903252)");   // d(log₁₀(x))/dx = log₁₀(e)/x
            TestDerivative(Sin(x), "Cos(x)");                             // d(sin(x))/dx = cos(x)
            TestDerivative(Cos(x), "-Sin(x)");                            // d(cos(x))/dx = -sin(x)
            TestDerivative(Tan(x), "(Cos(x) ^ -2)");                      // d(tan(x))/dx = sec²(x)
            TestDerivative(Asin(x), "((1 - (x ^ 2)) ^ -0.5)");            // d(arcsin(x))/dx = 1/√(1-x²)
            TestDerivative(Acos(x), "-((1 - (x ^ 2)) ^ -0.5)");           // d(arccos(x))/dx = -1/√(1-x²)
            TestDerivative(Atan(x), "(1 / ((x ^ 2) + 1))");               // d(arctan(x))/dx = 1/(x²+1)
            TestDerivative(Sinh(x), "Cosh(x)");                           // d(sinh(x))/dx = cosh(x)
            TestDerivative(Cosh(x), "Sinh(x)");                           // d(cosh(x))/dx = sinh(x)
            TestDerivative(Tanh(x), "(1 - (Tanh(x) ^ 2))");               // d(tanh(x))/dx = 1-tanh²(x)
        }

        public static void TestPolynomialDerivative()
        {
            TestDerivative(x.Power(4).Minus(3.Times(x.Cubed())).Plus(6.Times(x.Squared())).Minus(3.Times(x)).Plus(1),
                "(((((x ^ 3) * 4) - ((x ^ 2) * 9)) + (x * 12)) - 3)");    // d(x⁴-3x³+6x²-3x+1)/dx = 4x³-9x²+12x-3
        }

        public static void TestChainRule()
        {
            TestDerivative(Exp(x.Squared()), "(Exp((x ^ 2)) * (x * 2))"); // d(exp(x²))/dx = exp(x²)*2x
            TestDerivative(Log(Sin(x)), "((Sin(x) ^ -1) * Cos(x))");      // d(ln(sin(x)))/dx = cot(x)
            TestDerivative(Tan(x.Cubed().Plus(8.Times(x))),               // d(tan(x³+8x))/dx = sec²(x³+8x)*(3x²+8)
                "((Cos(((x ^ 3) + (x * 8))) ^ -2) * (((x ^ 2) * 3) + 8))");
            TestDerivative(Sqrt(x.Power(4).Minus(1)),                     // d(√(x⁴-1))/dx = 2x³/√(x⁴-1)
                "(((((x ^ 4) - 1) ^ -0.5) / 2) * ((x ^ 3) * 4))");
        }

        public static void TestAll()
        {
            TestCompoundExpression();
            TestTrigonometricExpression();
            TestSimplifications();
            TestFunctionDerivatives();
            TestPolynomialDerivative();
            TestChainRule();
        }
    }
}
