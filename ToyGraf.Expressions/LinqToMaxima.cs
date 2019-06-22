using System.Linq.Expressions;

namespace ToyGraf.Expressions
{
    public static class LinqToMaxima
    {
        #region Public Interface

        public static Expression FromMaxima(this string s) => s.FromMaxima("x", "t");

        public static Expression FromMaxima(this string s, params string[] wrt)
        {
            var ok = new Parser().TryParse(s
                .Replace("%e", "e")
                .Replace("%gamma", "γ")
                .Replace("%phi", "ϕ")
                .Replace("%pi", "π"),
                out Expression result, out _);
            return ok ? result : Expressions.DefaultVoid;
        }

        public static string ToMaxima(this Expression e) => e.ToMaxima("x");

        public static string ToMaxima(this Expression e, params string[] wrt) => e.ToMax(wrt).Simplify();

        #endregion

        #region Private Methods

        private static string ToMax(this Expression e, string[] wrt)
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
                            return $"-({ue.Operand.ToMax(wrt)})";
                        default:
                            return $"({ue.Operand.ToMax(wrt)})";
                    }
                case MethodCallExpression me:
                    string
                        functionName = me.Method.Name.ToLower(),
                        argument = me.Arguments[0].ToMax(wrt);
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
                    return $"({be.Left.ToMax(wrt)}){be.NodeType.AsString()}({be.Right.ToMax(wrt)})";
            }
            return string.Empty;
        }

        #endregion
    }
}
