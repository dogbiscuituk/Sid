namespace ToyGraf.Expressions
{
    using System;
    using System.Linq.Expressions;

    partial class Expressions
    {
        #region Differentiation

        public static Expression Differentiate(this Expression e) => Simplify(D(e));

        private static Expression D(this Expression e)
        {
            if (e.IsDefaultVoid())
                return e;
            switch (e)
            {
                case ConstantExpression ce:           // d(c)/dx = 0
                    return Constant(0);
                case ParameterExpression pe:          // d(x)/dx = 1
                    return pe.Name == "x" ? Constant(1) : Constant(0);
                case UnaryExpression ue:
                    var v = D(ue.Operand);
                    return ue.NodeType == ExpressionType.UnaryPlus ? v : Negate(v);
                case MethodCallExpression me:         // Use the Chain Rule
                    var methodName = me.Method.Name;
                    if (methodName == "Udf")
                    {
                        var index = me.Arguments[0];
                        var ticks = Expression.Constant((int)((ConstantExpression)me.Arguments[1]).Value + 1);
                        var fx = me.Arguments[2];
                        var ft = me.Arguments[3];
                        return Function("Udf", index, ticks, fx, ft).Times(D(fx));
                    }
                    var gx = me.Arguments[0];
                    return DifferentiateFunction(methodName, gx).Times(D(gx));
                case BinaryExpression be:
                    Expression f = be.Left, g = be.Right;
                    switch (be.NodeType)
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
                    return Constant(0);               // Logical/Equality/Relational
                case ConditionalExpression cond:      // (e?f:g)' = e?f':g'
                    return MakeConditional(cond.Test, D(cond.IfTrue), D(cond.IfFalse));
                default:
                    return Constant(0);
            }
            throw new FormatException($"Unable to differentiate expression {e.AsString()}.");
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
            throw new FormatException($"Unable to differentiate function {methodName}({x.AsString()}).");
        }

        #endregion

        #region Simplification

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
            var methodName = m.Method.Name;
            if (methodName == "Udf")
                return Function("Udf",
                    m.Arguments[0], m.Arguments[1], m.Arguments[2].Simplify(), m.Arguments[3]);
            var operand = Simplify(m.Arguments[0]);
            if (operand is ConstantExpression ce)
            {
                var c = (double)ce.Value;
                return Constant(Function(m.Method.Name, x).AsDouble(c));
            }
            return Function(methodName, operand);
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

        #endregion
    }
}
