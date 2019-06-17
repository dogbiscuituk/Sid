namespace ToyGraf.Expressions
{
    using System;
    using System.Linq.Expressions;

    partial class Expressions
    {
        public static Expression Differentiate(this Expression e) =>
            UseMaxima
            ? e.ToMaxima().Differentiate().FromMaxima()
            : Simplify(D(e));

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
    }
}
