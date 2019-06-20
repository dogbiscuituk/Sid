namespace ToyGraf.Expressions
{
    using System;
    using System.Linq.Expressions;

    partial class Expressions
    {
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
            if (methodName == "Xref")
                return Function("Xref",
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
    }
}
