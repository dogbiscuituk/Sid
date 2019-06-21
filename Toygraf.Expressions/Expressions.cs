namespace ToyGraf.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using ToyGraf.Expressions.Enumerations;

    using System.Runtime;

    public static partial class Expressions
    {
        #region Configuration

        public static bool UseMaxima = true;

        #endregion

        #region Parameters

        public static ParameterExpression x = Expression.Variable(typeof(double), "x");
        public static ParameterExpression t = Expression.Variable(typeof(double), "t");

        #endregion

        #region Constants

        public static Expression DefaultVoid = Expression.Default(typeof(void));
        public static ConstantExpression Constant(this double c) => Expression.Constant(c);
        public static ConstantExpression Constant(this int c) => Expression.Constant((double)c);
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

        #endregion

        #region Unary & Binary Expressions

        public static UnaryExpression Negate(this Expression e) => Expression.Negate(e);

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

        public static BinaryExpression Reciprocal(this Expression e) => e.Power(-1);
        public static BinaryExpression Squared(this Expression e) => e.Power(2);
        public static BinaryExpression Cubed(this Expression e) => e.Power(3);

        #endregion

        #region Functions

        public static MethodCallExpression Function(this string functionName, Expression e) =>
            Expression.Call(typeof(Functions).GetMethod(functionName,
                new[] { typeof(double) }), e);

        public static MethodCallExpression Function(this string functionName,
            Expression index, Expression ticks, Expression e1, Expression e2) =>
            Expression.Call(typeof(Functions).GetMethod(functionName,
                new[] { typeof(int), typeof(int), typeof(double), typeof(double) }), index, ticks, e1, e2);

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
        public static MethodCallExpression Hstep(this Expression e) => Function("Hstep", e);
        public static MethodCallExpression Log(this Expression e) => Function("Log", e);
        public static MethodCallExpression Log10(this Expression e) => Function("Log10", e);
        public static MethodCallExpression Round(this Expression e) => Function("Round", e);
        public static MethodCallExpression Sec(this Expression e) => Function("Sec", e);
        public static MethodCallExpression Sech(this Expression e) => Function("Sech", e);
        public static MethodCallExpression Sign(this Expression e) => Function("Sign", e);
        public static MethodCallExpression Sin(this Expression e) => Function("Sin", e);
        public static MethodCallExpression Sinh(this Expression e) => Function("Sinh", e);
        public static MethodCallExpression Sqrt(this Expression e) => Function("Sqrt", e);
        public static MethodCallExpression Tan(this Expression e) => Function("Tan", e);
        public static MethodCallExpression Tanh(this Expression e) => Function("Tanh", e);

        #endregion

        #region Conversion

        public static double AsDouble(this Expression e, double x, double t = 0) => AsFunction(e)(x, t);

        public static Func<double, double, double> AsFunction(this Expression e)
        {
            if (e.IsDefaultVoid())
                return (x, t) => double.NaN;
            return Expression.Lambda<Func<double, double, double>>(e.ToDouble(), x, t).Compile();
        }

        /// <summary>
        /// Rewrite an expression, possibly containing encoded references to other expressions in a
        /// given array. Since expressions are immutable, this is done by duplicating the original
        /// structure, and replacing any such references with similarly decoded versions of express-
        /// ions in the original array. Cross-references have the form "Xref(index, x, t)", where
        /// "index" is the position of the referred expression in the array.
        /// </summary>
        /// <param name="e">The expression to be rewritten.</param>
        /// <param name="ex">The expression representing the "x" parameter.</param>
        /// <param name="et">The expression representing the "t" parameter.</param>
        /// <param name="refs">The array of expressions available for referencing.</param>
        /// <returns>The input expression, rewritten with references replaced by the referred expressions.</returns>
        public static Expression AsProxy(this Expression e, Expression ex, Expression et, Expression[] refs)
        {
            if (e == x) return ex == x ? ex : ex.AsProxy(x, t, refs);
            if (e == t) return et == t ? et : et.AsProxy(x, t, refs);
            if (e is UnaryExpression u)
                return u.NodeType.MakeUnary(u.Operand.AsProxy(ex, et, refs), u.Type);
            if (e is MethodCallExpression m)
            {
                var methodName = m.Method.Name;
                if (methodName == "Xref")
                {
                    var index = (int)((ConstantExpression)m.Arguments[0]).Value;
                    if (index < 0 || index >= refs.Length)
                        return DefaultVoid;
                    var result = refs[index].AsProxy(m.Arguments[2], m.Arguments[3], refs);
                    for (var ticks = 0; ticks < (int)((ConstantExpression)m.Arguments[1]).Value; ticks++)
                        result = Differentiate(result);
                    return result;
                }
                return methodName.Function(m.Arguments[0].AsProxy(ex, et, refs));
            }
            if (e is BinaryExpression b)
                return MakeBinary(b.NodeType, b.Left.AsProxy(ex, et, refs), b.Right.AsProxy(ex, et, refs));
            return e;
        }

        public static string AsString(this Expression e) => e
            .AsString(Precedence.Assignment)
            .Replace("+-", "-")
            .Replace("Sqrt ", "√")
            .Replace("Sqrt", "√");

        public static string AsString(this Expression e, Precedence context)
        {
            switch (e)
            {
                case ConstantExpression ce:
                    return ce.Value.ToString();
                case ParameterExpression pe:
                    return pe == x ? "x" : pe == t ? "t" : pe.ToString();
                case UnaryExpression ue:
                    switch (ue.NodeType)
                    {
                        case ExpressionType.UnaryPlus:
                            return ue.Operand.AsString(Precedence.Unary);
                        case ExpressionType.Negate:
                            return $"-{ue.Operand.AsString(Precedence.Unary)}";
                        default:
                            return ue.ToString();
                    }
                case MethodCallExpression me:
                    var name = me.Method.Name;
                    var operand = me.Arguments[0].AsString(Precedence.Unary);
                    return context <= Precedence.Unary
                        ? operand.StartsWith("(")
                            ? $"{name}{operand}"
                            : $"{name} {operand}"
                        : $"{name}({operand})";
                case BinaryExpression be:
                    Expression left = be.Left, right = be.Right;
                    ExpressionType op = be.NodeType, opChild = op, opOther = ExpressionType.Multiply;
                    var ours = op.GetBinaryPrecedence();
                    bool flip = false;
                    switch (op)
                    {
                        case ExpressionType.Subtract:
                        case ExpressionType.Divide:
                            if (right is BinaryExpression beRight)
                            {
                                opChild = beRight.NodeType;
                                var theirs = opChild.GetBinaryPrecedence();
                                flip = ours == theirs;
                                if (flip)
                                {
                                    opOther = opChild.Invert();
                                    return MakeBinary(opOther, MakeBinary(op, left, beRight.Left), beRight.Right).AsString(context);
                                }
                            }
                            break;
                        case ExpressionType.Power:
                            if (left is BinaryExpression beLeft)
                            {
                                opChild = beLeft.NodeType;
                                var theirs = opChild.GetBinaryPrecedence();
                                flip = ours == theirs;
                                if (flip)
                                    return MakeBinary(op, beLeft.Left, MakeBinary(opOther, beLeft.Right, right)).AsString(context);
                            }
                            break;
                    }
                    var binary = AsString(left, op, right, ref ours);
                    return context <= ours ? binary : $"({binary})";
                case ConditionalExpression cond:
                    const Precedence pt = Precedence.Ternary;
                    Expression test = cond.Test, ifSo = cond.IfTrue, ifNot = cond.IfFalse;
                    var condition = $"{test.AsString(pt)}?{ifSo.AsString(pt)}:{ifNot.AsString(pt)}";
                    return context <= pt ? condition : $"({condition})";
                default:
                    return e.ToString();
            }
        }

        public static string AsString(Expression left, ExpressionType op, Expression right, ref Precedence context)
        {
            switch (op)
            {
                case ExpressionType.Multiply when left is ConstantExpression ce && !(right is ConstantExpression):
                    return $"{ce.Value}{right.AsString(Precedence.Implied)}";
                case ExpressionType.Power when right is ConstantExpression ce:
                    var ceValue = (double)ce.Value;
                    var ceFloor = (int)Math.Floor(ceValue);
                    if (ceFloor == ceValue && ceFloor != -1) // Avoid confusion with inverse function syntax!
                    {
                        var ceSuper = ceFloor.ToString().ToSuperscript();
                        context = Precedence.Superscript;
                        return
                            left is MethodCallExpression me && me.Arguments.Count == 1 && me.Method.Name != "Sqrt"
                            ? $"{me.Method.Name}{ceSuper}{me.Arguments[0].AsString(context)}"
                            : $"{left.AsString(Precedence.Superscript)}{ceSuper}";
                    }
                    break;
            }
            return $"{left.AsString(context)}{op.AsString()}{right.AsString(context)}";
        }

        public static Expression Parse(this string formula) => new Parser().Parse(formula);

        private static bool IsDefaultVoid(this Expression e) =>
            e is DefaultExpression && e.Type == typeof(void);

        #endregion

        #region Make methods

        public static Expression MakeBinary(
            this string op, Expression left, Expression right) =>
            MakeBinary(op.GetExpressionType(), left, right);

        public static Expression MakeBinary(
            this ExpressionType nodeType, Expression left, Expression right)
        {
            if (left.IsDefaultVoid() || right.IsDefaultVoid())
                return DefaultVoid;
            switch (nodeType.GetBinaryOperandTypes())
            {
                case OperandTypes.Boolean:
                    left = left.ToBoolean();
                    right = right.ToBoolean();
                    break;
                case OperandTypes.Double:
                    left = left.ToDouble();
                    right = right.ToDouble();
                    break;
            }
            return Expression.MakeBinary(nodeType, left, right);
        }

        public static Expression MakeConditional(
            this Expression test, Expression then, Expression otherwise) =>
            Expression.Condition(test.ToBoolean(), then, otherwise);

        public static Expression MakeFunction(this string f, Expression operand)
        {
            f = f.ToTitleCase();
            var match = Regex.Match(f, @"^F(\d+)$");
            if (match.Success)
            {
                var index = Expression.Constant(int.Parse(match.Groups[1].Value));
                Expression left, right;
                if (operand is BinaryExpression b && b.NodeType == ExpressionType.Modulo)
                {
                    left = b.Left;
                    right = b.Right;
                }
                else
                {
                    left = operand;
                    right = 0.0.Constant();
                }
                return Function("Xref", index, Expression.Constant(0), left, right);
            }
            var result = Function(f, operand);
            if (operand is ConstantExpression c)
                return result.AsDouble((double)c.Value).Constant();
            return result;
        }

        public static Expression MakeUnary(
            this ExpressionType nodeType, Expression operand, Type type = null) =>
            Expression.MakeUnary(nodeType, operand, type);

        public static Expression MakeUnary(this string op, Expression operand)
        {
            if (operand is ConstantExpression c)
            {
                var cValue = (double)c.Value;
                switch (op)
                {
                    case Ops.UnaryMinus:
                        return Constant(-cValue);
                    case "!":
                    case "~":
                        return Constant(cValue == 0.0 ? 1.0 : 0.0);
                    case Ops.SquareRoot:
                        return Constant(Math.Sqrt(cValue));
                    case Ops.CubeRoot:
                        return Constant(Math.Pow(cValue, 1.0 / 3.0));
                    case Ops.FourthRoot:
                        return Constant(Math.Pow(cValue, 0.25));
                }
            }
            switch (op)
            {
                case Ops.UnaryPlus:
                    return operand;
                case Ops.SquareRoot:
                    return MakeFunction("Sqrt", operand);
                case Ops.CubeRoot:
                    return operand.Power(1.0 / 3.0);
                case Ops.FourthRoot:
                    return operand.Power(0.25);
            }
            return op.GetExpressionType().MakeUnary(operand);
        }

        #endregion
    }
}
