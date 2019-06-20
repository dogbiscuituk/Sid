using System.Linq.Expressions;

namespace ToyGraf.Expressions
{
    public static class LinqToMaxima
    {
        public static Expression FromMaxima(this string s) => s.FromMaxima("x", "t");

        public static Expression FromMaxima(this string s, params string[] paramNames)
        {
            var ok = new Parser().TryParse(s
                .Replace("%e", "e")
                .Replace("%gamma", "γ")
                .Replace("%phi", "ϕ")
                .Replace("%pi", "π"),
                out object result);
            return ok ? (Expression)result : Expressions.DefaultVoid;
        }

        public static string ToMaxima(this Expression e) => e.ToMaxima(new[] { "x", "t" });

        public static string ToMaxima(this Expression e, string[] paramNames)
        {
            switch (e)
            {
                case ParameterExpression pe:
                    return pe.Name;
                case ConstantExpression ce:
                    return ce.Value.ToString();
                case UnaryExpression ue:
                    switch (ue.NodeType)
                    {
                        case ExpressionType.Negate:
                            return $"-({ue.Operand.ToMaxima()})";
                        default:
                            return $"({ue.Operand.ToMaxima()})";
                    }
                case MethodCallExpression me:
                    string
                        functionName = me.Method.Name.ToLower(),
                        argument = me.Arguments[0].ToMaxima();
                    switch (functionName)
                    {
                        case "exp":
                            return $"%e^({argument})";
                        case "log10":
                            return $"(log({argument})/log(10))";
                        default:
                            return $"{functionName}({argument})";
                    }
                case BinaryExpression be:
                    return $"({be.Left.ToMaxima()}){be.NodeType.AsString()}({be.Right.ToMaxima()})";
            }
            return string.Empty;
        }
    }
}
