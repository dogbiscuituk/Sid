namespace FormulaBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    public class Parser
    {
        private string Formula;
        private int Index;
        private Stack<Expression> Operands;
        private Stack<string> Operations;

        public Expression Parse(string formula)
        {
            Formula = formula;
            Index = 0;
            Operands = new Stack<Expression>();
            Operations = new Stack<string>(new[] { "(" });
            ParseExpression();
            var result = Operands.Peek();
            System.Diagnostics.Debug.WriteLine(result);
            return result;
        }

        private void ConsumeToken(string token)
        {
            Index += token.Length;
        }

        private void Error()
        {
            throw new FormatException();
        }

        private void ExpectToken(string token)
        {
            if (NextToken() == token)
                ConsumeToken(token);
            else
                Error();
        }

        private static ExpressionType GetExpressionType(string operation)
        {
            switch (operation)
            {
                case "+": return ExpressionType.Add;
                case "-": return ExpressionType.Subtract;
                case "*": return ExpressionType.Multiply;
                case "/": return ExpressionType.Divide;
                case "^": return ExpressionType.Power;
                case "u+": return ExpressionType.UnaryPlus;
                case "u-": return ExpressionType.Negate;
            }
            throw new FormatException();
        }

        private static int GetPrecedence(string operation)
        {
            switch (operation)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
            }
            return 4;
        }

        private bool IsOperation(string token) => "+-*/^".IndexOf(token[0]) >= 0;

        private Expression MakeBinary(string operation, Expression operand1, Expression operand2) =>
            Expression.MakeBinary(GetExpressionType(operation), operand1, operand2);

        private Expression MakeFunction(string f, Expression operand) =>
            Expressions.Function(char.ToUpper(f[0]) + f.ToLower().Substring(1), operand);

        private Expression MakeUnary(string operation, Expression operand) =>
            Expression.MakeUnary(GetExpressionType(operation), operand, null);

        private string MatchFunction() => MatchRegex(@"\w+");

        private string MatchNumber() => MatchRegex(@"^\d+\.?\d*([eE][-+]?\d+)?");

        private string MatchRegex(string pattern)
        {
            var match = Regex.Match(Formula.Substring(Index), pattern);
            return Formula.Substring(Index + match.Index, match.Length);
        }

        private string NextToken()
        {
            var peek = Peek();
            switch (peek)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                case "(":
                case ")":
                    return peek.ToString();
            }
            switch (peek[0])
            {
                case char c when char.IsDigit(c):
                    return MatchNumber();
                case char c when char.IsLetter(c):
                    return MatchFunction();
            }
            throw new FormatException();
        }

        private void ParseExpression()
        {
            do
            {
                ParseOperand();
                var operation = Peek();
                if ("+-*/^)".IndexOf(operation) < 0)
                    break;
                ParseOperation(operation);
                if (operation == ")")
                    return;
            }
            while (true);
        }

        private void ParseFunction(string function)
        {
            Operations.Push(function);
            ConsumeToken(function);
        }

        private void ParseNumber(string number)
        {
            var operand = double.Parse(number).Constant();
            Operands.Push(operand);
            ConsumeToken(number);
        }

        private void ParseOperand()
        {
            var token = NextToken();
            switch (token[0])
            {
                case 'x':
                    ParseParameter(token);
                    break;
                case char c when char.IsDigit(c):
                    ParseNumber(token);
                    break;
                case '(':
                    Operations.Push(token);
                    ConsumeToken(token);
                    ParseExpression();
                    break;
                case '+':
                case '-':
                    ParseUnary(token);
                    ParseOperand();
                    break;
                case char c when char.IsLetter(c):
                    ParseFunction(token);
                    ParseOperand();
                    break;
                default:
                    Error();
                    break;
            }
        }

        private void ParseOperation(string operation)
        {
            do
            {
                var ours = GetPrecedence(operation);
                var pending = Operations.Peek();
                if (pending == "(")
                    break;
                var theirs = GetPrecedence(pending);
                // Operator '^' is right associative, i.e., a^b^c = a^(b^c).
                if (theirs > ours || theirs == ours && operation != "^")
                {
                    Operations.Pop();
                    var operand = Operands.Pop();
                    if (theirs > 3)
                        switch (pending)
                        {
                            case "u+":
                                break;
                            case "u-":
                                operand = MakeUnary(pending, operand);
                                break;
                            default:
                                operand = MakeFunction(pending, operand);
                                break;
                        }
                    else
                        operand = MakeBinary(pending, Operands.Pop(), operand);
                    Operands.Push(operand);
                }
                else
                    break;
            }
            while (true);
            if (operation == ")")
                Operations.Pop();
            else
                Operations.Push(operation);
            ConsumeToken(operation);
        }

        private void ApplyUnaries()
        {

        }

        private void ParseParameter(string token)
        {
            var operand = Expressions.x;
            Operands.Push(operand);
            ConsumeToken(token);
        }

        private void ParseUnary(string unary)
        {
            Operations.Push("u" + unary);
            ConsumeToken(unary);
        }

        private string Peek()
        {
            var count = Formula.Length;
            while (Index < count && Formula[Index] == ' ')
                Index++;
            return Index < count ? Formula[Index].ToString() : Index == count ? ")" : "$";
        }
    }
}
