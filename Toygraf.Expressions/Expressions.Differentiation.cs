namespace ToyGraf.Expressions
{
    using System;
    using System.Linq.Expressions;

    partial class Expressions
    {
        public static Expression Differentiate(this Expression e) =>
            e.IsDefaultVoid() ? e : UseMaxima
                ? e.ToMaxima().Differentiate().FromMaxima()
                : e.D().Simplify();

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
                    if (methodName == "xref")
                    {
                        var index = me.Arguments[0];
                        var ticks = Expression.Constant((int)((ConstantExpression)me.Arguments[1]).Value + 1);
                        var fx = me.Arguments[2];
                        var ft = me.Arguments[3];
                        return Function("xref", index, ticks, fx, ft).Times(D(fx));
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
                            return D(f).Times(g).Plus(f.Times(D(g)).Times(log(f))).Times(f.Power(g.Minus(1)));
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
                case "abs": return x.Over(abs(x));                                           // d(|x|)/dx = x/|x|
                case "acos": return (-1).Over(sqrt(1.Minus(x.Squared())));                   // d(acos x)/dx = -1/√(1-x²)
                case "acosh": return 1.Over(sqrt(x.Squared().Minus(1)));                     // d(acosh x)/dx = 1/√(x²-1),     x>1
                case "acot": return (-1).Over(1.Plus(x.Squared()));                          // d(acot x)/dx = -1/(1+x²)
                case "acoth": return 1.Over(1.Minus(x.Squared()));                           // d(acoth x)/dx = 1/(1-x²),      |x|>1
                case "acsc": return (-1).Over(abs(x).Times(sqrt(x.Squared().Minus(1))));     // d(acsc x)/dx = -1/|x|√(x²-1)
                case "acsch": return (-1).Over(x.Times(sqrt(1.Plus(x.Squared()))));          // d(acsch x)/dx = -1/x√(1+x²),   x≠0
                case "asec": return 1.Over(abs(x).Times(sqrt(x.Squared().Minus(1))));        // d(asec x)/dx = 1/|x|√(x²-1)
                case "asech": return (-1).Over(x.Times(sqrt(1.Minus(x.Squared()))));         // d(asech x)/dx = -1/x√(1-x²),   0<x<1
                case "asin": return 1.Over(sqrt(1.Minus(x.Squared())));                      // d(asin x)/dx = 1/√(1-x²)
                case "asinh": return 1.Over(sqrt(1.Plus(x.Squared())));                      // d(asinh x)/dx = 1/√(1+x²)
                case "atan": return 1.Over(1.Plus(x.Squared()));                             // d(atan x)/dx = 1/(1+x²)
                case "atanh": return 1.Over(1.Minus(x.Squared()));                           // d(atanh x)/dx = 1/(1-x²),      |x|<1
                case "cos": return Negate(sin(x));                                           // d(cos x)/dx = -sin x
                case "cosh": return sinh(x);                                                 // d(cosh x)/dx = sinh x
                case "cot": return Negate(csc(x).Squared());                                 // d(cot x)/dx = -csc²x
                case "coth": return Negate(csch(x).Squared());                               // d(coth x)/dx = -csch²x,        x≠0
                case "csc": return Negate(csc(x).Times(cot(x)));                             // d(csc x)/dx = -csc x cot x
                case "csch": return Negate(csch(x).Times(coth(x)));                          // d(csch x)/dx = -csch x coth x, x≠0
                case "erf": return x.Squared().Negate().exp().Times(2 / Math.Sqrt(Math.PI)); // d(erf x)/dx = exp(-x²)*2/√π
                case "exp": return exp(x);                                                   // d(eˣ)/dx = eˣ
                case "log": return 1.Over(x);                                                // d(ln x)/dx = 1/x
                case "log10": return Math.Log10(Math.E).Over(x);                             // d(log₁₀ x)/dx = log₁₀e/x
                case "sec": return sec(x).Times(tan(x));                                     // d(sec x)/dx = sec x tan x
                case "sech": return Negate(sech(x).Times(tanh(x)));                          // d(sech x)/dx = -sech x tanh x
                case "sin": return cos(x);                                                   // d(sin x)/dx = cos x
                case "sinh": return cosh(x);                                                 // d(sinh x)/dx = cosh x
                case "sqrt": return 0.5.Over(sqrt(x));                                       // d(√x)/dx = 1/(2√x)
                case "tan": return sec(x).Squared();                                         // d(tan x)/dx = sec²x
                case "tanh": return sech(x).Squared();                                       // d(tanh x)/dx = sech²x
                default: return Constant(0);
            }
            throw new FormatException($"Unable to differentiate function {methodName}({x.AsString()}).");
        }
    }
}
