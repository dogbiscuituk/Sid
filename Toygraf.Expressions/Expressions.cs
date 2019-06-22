namespace ToyGraf.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using ToyGraf.Expressions.Enumerations;

    using System.Runtime;
    using System.Text;

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

        public static MethodCallExpression abs(this Expression e) => Function("abs", e);
        public static MethodCallExpression acos(this Expression e) => Function("acos", e);
        public static MethodCallExpression acosh(this Expression e) => Function("acosh", e);
        public static MethodCallExpression acot(this Expression e) => Function("acot", e);
        public static MethodCallExpression acoth(this Expression e) => Function("acoth", e);
        public static MethodCallExpression acsc(this Expression e) => Function("acsc", e);
        public static MethodCallExpression acsch(this Expression e) => Function("acsch", e);
        public static MethodCallExpression asec(this Expression e) => Function("asec", e);
        public static MethodCallExpression asech(this Expression e) => Function("asech", e);
        public static MethodCallExpression asin(this Expression e) => Function("asin", e);
        public static MethodCallExpression asinh(this Expression e) => Function("asinh", e);
        public static MethodCallExpression atan(this Expression e) => Function("atan", e);
        public static MethodCallExpression atanh(this Expression e) => Function("atanh", e);
        public static MethodCallExpression ceiling(this Expression e) => Function("ceiling", e);
        public static MethodCallExpression cos(this Expression e) => Function("cos", e);
        public static MethodCallExpression cosh(this Expression e) => Function("cosh", e);
        public static MethodCallExpression cot(this Expression e) => Function("cot", e);
        public static MethodCallExpression coth(this Expression e) => Function("coth", e);
        public static MethodCallExpression csc(this Expression e) => Function("csc", e);
        public static MethodCallExpression csch(this Expression e) => Function("csch", e);
        public static MethodCallExpression erf(this Expression e) => Function("erf", e);
        public static MethodCallExpression exp(this Expression e) => Function("exp", e);
        public static MethodCallExpression floor(this Expression e) => Function("floor", e);
        public static MethodCallExpression hstep(this Expression e) => Function("hstep", e);
        public static MethodCallExpression log(this Expression e) => Function("log", e);
        public static MethodCallExpression log10(this Expression e) => Function("log10", e);
        public static MethodCallExpression round(this Expression e) => Function("round", e);
        public static MethodCallExpression sec(this Expression e) => Function("sec", e);
        public static MethodCallExpression sech(this Expression e) => Function("sech", e);
        public static MethodCallExpression sign(this Expression e) => Function("sign", e);
        public static MethodCallExpression sin(this Expression e) => Function("sin", e);
        public static MethodCallExpression sinh(this Expression e) => Function("sinh", e);
        public static MethodCallExpression sqrt(this Expression e) => Function("sqrt", e);
        public static MethodCallExpression tan(this Expression e) => Function("tan", e);
        public static MethodCallExpression tanh(this Expression e) => Function("tanh", e);

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
        /// ions in the original array. Cross-references have the form "xref(index, x, t)", where
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
                if (methodName == "xref")
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
            ?.AsString(Precedence.Assignment)
            ?.Replace("+-", "-")
            ?.Replace("sqrt ", "√")
            ?.Replace("sqrt", "√");

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
                    var result = new StringBuilder(name);
                    var count = me.Arguments.Count;
                    var group = count > 1;
                    var precedence = group ? Precedence.Sequential : Precedence.Unary;
                    group |= context > precedence;
                    if (group)
                        result.Append('(');
                    for (var index = 0; index < count; index++)
                    {
                        var operand = me.Arguments[index].AsString(precedence);
                        if (!group && !operand.StartsWith("("))
                            result.Append(' ');
                        result.Append(operand);
                        if (index < count - 1)
                            result.Append(',');
                    }
                    if (group)
                        result.Append(')');
                    return result.ToString();
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
                    return e?.ToString();
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
                            left is MethodCallExpression me && me.Arguments.Count == 1 && me.Method.Name != "sqrt"
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
            f = f.FromSubscript().ToLower();
            var match = Regex.Match(f, @"^f(\d+)$");
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
                return Function("xref", index, Expression.Constant(0), left, right);
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
                    return MakeFunction("sqrt", operand);
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
