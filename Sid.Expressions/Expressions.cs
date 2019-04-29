namespace Sid.Expressions
{
    using System;
    using System.Linq.Expressions;

    public static partial class Expressions
    {
        public static ParameterExpression x = Expression.Variable(typeof(double));

        public static ConstantExpression Constant(this double c) => Expression.Constant(c);

        public static string AsString(this Expression e, string variableName = "x") =>
            e.ToString().Replace(" ", "").Replace("Param_0", variableName);

        public static Func<double, double> AsFunction(this Expression e)
        {
            if (e is DefaultExpression && e.Type == typeof(void))
                return (x) => double.NaN;
            return Expression.Lambda<Func<double, double>>(e.ToDouble(), x).Compile();
        }

        public static double AsDouble(this Expression e, double x) => AsFunction(e)(x);

        public static ConstantExpression e = Math.PI.Constant();

        public static Expression Parse(this string formula) => new Parser().Parse(formula);

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

        public static BinaryExpression Equal(this Expression f, Expression g) => Expression.Equal(f, g);
        public static BinaryExpression NotEqual(this Expression f, Expression g) => Expression.NotEqual(f, g);
        public static BinaryExpression LessThan(this Expression f, Expression g) => Expression.LessThan(f, g);
        public static BinaryExpression GreaterThan(this Expression f, Expression g) => Expression.GreaterThan(f, g);
        public static BinaryExpression LessThanOrEqual(this Expression f, Expression g) => Expression.LessThanOrEqual(f, g);
        public static BinaryExpression GreaterThanOrEqual(this Expression f, Expression g) => Expression.GreaterThanOrEqual(f, g);

        public static BinaryExpression Equal(this Expression f, double g) => Expression.Equal(f, Constant(g));
        public static BinaryExpression NotEqual(this Expression f, double g) => Expression.NotEqual(f, Constant(g));
        public static BinaryExpression LessThan(this Expression f, double g) => Expression.LessThan(f, Constant(g));
        public static BinaryExpression GreaterThan(this Expression f, double g) => Expression.GreaterThan(f, Constant(g));
        public static BinaryExpression LessThanOrEqual(this Expression f, double g) => Expression.LessThanOrEqual(f, Constant(g));
        public static BinaryExpression GreaterThanOrEqual(this Expression f, double g) => Expression.GreaterThanOrEqual(f, Constant(g));

        public static BinaryExpression Equal(this double f, Expression g) => Expression.Equal(Constant(f), g);
        public static BinaryExpression NotEqual(this double f, Expression g) => Expression.NotEqual(Constant(f), g);
        public static BinaryExpression LessThan(this double f, Expression g) => Expression.LessThan(Constant(f), g);
        public static BinaryExpression GreaterThan(this double f, Expression g) => Expression.GreaterThan(Constant(f), g);
        public static BinaryExpression LessThanOrEqual(this double f, Expression g) => Expression.LessThanOrEqual(Constant(f), g);
        public static BinaryExpression GreaterThanOrEqual(this double f, Expression g) => Expression.GreaterThanOrEqual(Constant(f), g);

        public static BinaryExpression Equal(this int f, Expression g) => Expression.Equal(Constant(f), g);
        public static BinaryExpression NotEqual(this int f, Expression g) => Expression.NotEqual(Constant(f), g);
        public static BinaryExpression LessThan(this int f, Expression g) => Expression.LessThan(Constant(f), g);
        public static BinaryExpression GreaterThan(this int f, Expression g) => Expression.GreaterThan(Constant(f), g);
        public static BinaryExpression LessThanOrEqual(this int f, Expression g) => Expression.LessThanOrEqual(Constant(f), g);
        public static BinaryExpression GreaterThanOrEqual(this int f, Expression g) => Expression.GreaterThanOrEqual(Constant(f), g);

        public static UnaryExpression Negate(this Expression e) => Expression.Negate(e);
        public static BinaryExpression Reciprocal(this Expression e) => e.Power(-1);
        public static BinaryExpression Squared(this Expression e) => e.Power(2);
        public static BinaryExpression Cubed(this Expression e) => e.Power(3);

        public static MethodCallExpression Function(string functionName, Expression e) =>
            Expression.Call(typeof(Functions).GetMethod(functionName, new[] { typeof(double) }), e);

        public static MethodCallExpression Abs(this Expression e) => Function("Abs", e);
        public static MethodCallExpression Acos(this Expression e) => Function("Acos", e);
        public static MethodCallExpression Acosh(this Expression e) => Function("Acosh", e);
        public static MethodCallExpression Acot(this Expression e) => Function("Acot", e);
        public static MethodCallExpression Acoth(this Expression e) => Function("Acoth", e);
        public static MethodCallExpression Acsc(this Expression e) => Function("Acsc", e);
        public static MethodCallExpression Acsch(this Expression e) => Function("Acsch", e);
        public static MethodCallExpression Asec(this Expression e) => Function("Asec", e);
        public static MethodCallExpression Asech(this Expression e) => Function("Asech", e);
        public static MethodCallExpression Asin(this Expression e) => Function("Asin", e);
        public static MethodCallExpression Asinh(this Expression e) => Function("Asinh", e);
        public static MethodCallExpression Atan(this Expression e) => Function("Atan", e);
        public static MethodCallExpression Atanh(this Expression e) => Function("Atanh", e);
        public static MethodCallExpression Ceiling(this Expression e) => Function("Ceiling", e);
        public static MethodCallExpression Cos(this Expression e) => Function("Cos", e);
        public static MethodCallExpression Cosh(this Expression e) => Function("Cosh", e);
        public static MethodCallExpression Cot(this Expression e) => Function("Cot", e);
        public static MethodCallExpression Coth(this Expression e) => Function("Coth", e);
        public static MethodCallExpression Csc(this Expression e) => Function("Csc", e);
        public static MethodCallExpression Csch(this Expression e) => Function("Csch", e);
        public static MethodCallExpression Erf(this Expression e) => Function("Erf", e);
        public static MethodCallExpression Exp(this Expression e) => Function("Exp", e);
        public static MethodCallExpression Floor(this Expression e) => Function("Floor", e);
        public static MethodCallExpression Ln(this Expression e) => Function("Ln", e);
        public static MethodCallExpression Log10(this Expression e) => Function("Log10", e);
        public static MethodCallExpression Round(this Expression e) => Function("Round", e);
        public static MethodCallExpression Sec(this Expression e) => Function("Sec", e);
        public static MethodCallExpression Sech(this Expression e) => Function("Sech", e);
        public static MethodCallExpression Sign(this Expression e) => Function("Sign", e);
        public static MethodCallExpression Sin(this Expression e) => Function("Sin", e);
        public static MethodCallExpression Sinh(this Expression e) => Function("Sinh", e);
        public static MethodCallExpression Sqrt(this Expression e) => Function("Sqrt", e);
        public static MethodCallExpression Step(this Expression e) => Function("Step", e);
        public static MethodCallExpression Tan(this Expression e) => Function("Tan", e);
        public static MethodCallExpression Tanh(this Expression e) => Function("Tanh", e);

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
                        case ExpressionType.Power:    // (f^g)' = (f^g)*(f'g÷f+g'Ln(f)) = (f'g+fg'Ln(f))f^(g-1)
                            return D(f).Times(g).Plus(f.Times(D(g)).Times(Ln(f))).Times(f.Power(g.Minus(1)));
                    }
                    break;
            }
            throw new InvalidOperationException();
        }

        private static Expression DifferentiateFunction(string methodName, Expression x)
        {
            switch (methodName)
            {
                case "Abs": return x.Over(Abs(x));                                           // d(|x|)/dx = x/|x|
                case "Acos": return (-1).Over(Sqrt(1.Minus(x.Squared())));                   // d(acos x)/dx = -1/√(1-x²)
                case "Acosh": return 1.Over(Sqrt(x.Squared().Minus(1)));                     // d(acosh x)/dx = 1/√(x²-1),     x>1
                case "Acot": return (-1).Over(1.Plus(x.Squared()));                          // d(acot x)/dx = -1/(1+x²)
                case "Acoth": return 1.Over(1.Minus(x.Squared()));                           // d(acoth x)/dx = 1/(1-x²),      |x|>1
                case "Acsc": return (-1).Over(Abs(x).Times(Sqrt(x.Squared().Minus(1))));     // d(acsc x)/dx = -1/|x|√(x²-1)
                case "Acsch": return (-1).Over(x.Times(Sqrt(1.Plus(x.Squared()))));          // d(acsch x)/dx = -1/x√(1+x²),   x≠0
                case "Asec": return 1.Over(Abs(x).Times(Sqrt(x.Squared().Minus(1))));        // d(asec x)/dx = 1/|x|√(x²-1)
                case "Asech": return (-1).Over(x.Times(Sqrt(1.Minus(x.Squared()))));         // d(asech x)/dx = -1/x√(1-x²),   0<x<1
                case "Asin": return 1.Over(Sqrt(1.Minus(x.Squared())));                      // d(asin x)/dx = 1/√(1-x²)
                case "Asinh": return 1.Over(Sqrt(1.Plus(x.Squared())));                      // d(asinh x)/dx = 1/√(1+x²)
                case "Atan": return 1.Over(1.Plus(x.Squared()));                             // d(atan x)/dx = 1/(1+x²)
                case "Atanh": return 1.Over(1.Minus(x.Squared()));                           // d(atanh x)/dx = 1/(1-x²),      |x|<1
                case "Cos": return Negate(Sin(x));                                           // d(cos x)/dx = -sin x
                case "Cosh": return Sinh(x);                                                 // d(cosh x)/dx = sinh x
                case "Cot": return Negate(Csc(x).Squared());                                 // d(cot x)/dx = -csc²x
                case "Coth": return Negate(Csch(x).Squared());                               // d(coth x)/dx = -csch²x,        x≠0
                case "Csc": return Negate(Csc(x).Times(Cot(x)));                             // d(csc x)/dx = -csc x cot x
                case "Csch": return Negate(Csch(x).Times(Coth(x)));                          // d(csch x)/dx = -csch x coth x, x≠0
                case "Erf": return x.Squared().Negate().Exp().Times(2 / Math.Sqrt(Math.PI)); // d(erf x)/dx = exp(-x²)*2/√π
                case "Exp": return Exp(x);                                                   // d(eˣ)/dx = eˣ
                case "Ln": return 1.Over(x);                                                 // d(ln x)/dx = 1/x
                case "Log10": return Math.Log10(Math.E).Over(x);                             // d(log₁₀ x)/dx = log₁₀e/x
                case "Sec": return Sec(x).Times(Tan(x));                                     // d(sec x)/dx = sec x tan x
                case "Sech": return Negate(Sech(x).Times(Tanh(x)));                          // d(sech x)/dx = -sech x tanh x
                case "Sin": return Cos(x);                                                   // d(sin x)/dx = cos x
                case "Sinh": return Cosh(x);                                                 // d(sinh x)/dx = cosh x
                case "Sqrt": return 0.5.Over(Sqrt(x));                                       // d(√x)/dx = 1/(2√x)
                case "Tan": return Sec(x).Squared();                                         // d(tan x)/dx = sec²x
                case "Tanh": return Sech(x).Squared();                                       // d(tanh x)/dx = sech²x
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
    }
}
