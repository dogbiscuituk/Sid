namespace ToyGraf.Expressions
{
    using System;
    using System.Linq.Expressions;

    partial class Expressions
    {
        public static Expression Integrate(this Expression e, string wrt = "x") =>
            UseMaxima
            ? e.ToMaxima().Integrate(wrt).FromMaxima()
            : e.I(wrt).Simplify();

        public static double Integrate(this Expression e, double from, double to, string wrt = "x")
        {
            var F = e.Integrate(wrt).AsFunction();
            switch (wrt)
            {
                case "x":
                    return F(to, 0) - F(from, 0);
                case "t":
                    return F(0, to) - F(0, from);
            }
            return 0;
        }

        private static Expression I(this Expression e, string wrt = "x")
        {
            if (e.IsDefaultVoid())
                return e;
            switch (e)
            {
                case ConstantExpression ce:           // ∫k dx = kx + c
                    var k = (double)ce.Value;
                    return x.Times(k);
                case ParameterExpression pe:          // ∫x dx = ½x²
                    return pe.Name == "x" ? pe.Squared().Over(2) : x.Times(e);
                case MethodCallExpression me:         // ???
                    return NI(e);
                case UnaryExpression ue:
                    var v = I(ue.Operand);
                    return ue.NodeType == ExpressionType.UnaryPlus ? v : Negate(v);
                case BinaryExpression be:
                    Expression f = be.Left, g = be.Right;
                    switch (be.NodeType)
                    {
                        case ExpressionType.Add:      // ∫(f+g)dx = ∫f dx + ∫g dx
                            return I(f).Plus(I(g));
                        case ExpressionType.Subtract: // ∫(f-g)dx = ∫f dx - ∫g dx
                            return I(f).Minus(I(g));
                        /*case ExpressionType.Multiply: // ∫f dg = fg - ∫g df
                            return IntegrateByParts(be);
                        case ExpressionType.Divide:   // ∫f dg = fg - ∫g df
                            return IntegrateByParts(MakeBinary(
                                "*", be.Left, be.Right.Reciprocal()));*/
                    }
                    return NI(e);
                case ConditionalExpression cond:      // ∫(e?f:g)dx = e ? ∫f dx : ∫g dx
                    return MakeConditional(cond.Test, I(cond.IfTrue), I(cond.IfFalse));
            }
            return NI(e);
        }

        private static Expression IntegrateByParts(BinaryExpression be)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Numeric Intergation (TODO).
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static Expression NI(Expression e)
        {
            throw new NotImplementedException();
        }
    }
}
