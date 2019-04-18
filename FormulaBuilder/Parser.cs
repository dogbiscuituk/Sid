namespace FormulaBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    public class Parser
    {
        private enum Precedence
        {
            RightParenthesis,
            Additive,
            Multiplicative,
            Exponential,
            Functional
        }

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
            return Operands.Peek();
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

        private static Precedence GetPrecedence(string operation)
        {
            switch (operation)
            {
                case ")": return Precedence.RightParenthesis;
                case "+":
                case "-": return Precedence.Additive;
                case "*":
                case "/": return Precedence.Multiplicative;
                case "^": return Precedence.Exponential;
            }
            return Precedence.Functional;
        }

        private Expression MakeBinary(string operation, Expression operand1, Expression operand2) =>
            Expression.MakeBinary(GetExpressionType(operation), operand1, operand2);

        private Expression MakeFunction(string f, Expression operand) =>
            Expressions.Function(char.ToUpper(f[0]) + f.ToLower().Substring(1), operand);

        private Expression MakeUnary(string operation, Expression operand) =>
            Expression.MakeUnary(GetExpressionType(operation), operand, null);

        private string MatchFunction() => MatchRegex(@"\w+").ToLower();

        private string MatchNumber() => MatchRegex(@"^\d*\.?\d*([eE][+-]?\d+)?");

        private string MatchRegex(string pattern)
        {
            var match = Regex.Match(Formula.Substring(Index), pattern);
            return Formula.Substring(Index + match.Index, match.Length);
        }

        private string NextChar()
        {
            var count = Formula.Length;
            while (Index < count && Formula[Index] == ' ')
                Index++;
            return Index < count ? Formula[Index].ToString() : Index == count ? ")" : "$";
        }

        private string NextToken()
        {
            var nextChar = NextChar();
            switch (nextChar)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                case "(":
                case ")":
                    return nextChar.ToString();
            }
            switch (nextChar[0])
            {
                case char c when char.IsDigit(c):
                case '.':
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
                var operation = NextChar();
                if ("+-*/^)".IndexOf(operation) < 0)
                    break;
                ParseOperation(operation);
                if (operation == ")")
                    break;
            }
            while (true);
        }

        private void ParseFunction(string function)
        {
            Operations.Push(function);
            ReadPast(function);
        }

        private void ParseNumber(string number)
        {
            var operand = double.Parse(number).Constant();
            Operands.Push(operand);
            ReadPast(number);
        }

        private void ParseOperand()
        {
            var token = NextToken();
            switch (char.ToLower(token[0]))
            {
                case 'x' when token.Length == 1:
                    ParseParameter(token);
                    break;
                case char c when char.IsDigit(c):
                case '.':
                    ParseNumber(token);
                    break;
                case '(':
                    Operations.Push(token);
                    ReadPast(token);
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
                    throw new FormatException();
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
                // Operator '^' is right associative: a^b^c = a^(b^c).
                if (theirs > ours || theirs == ours && operation != "^")
                {
                    Operations.Pop();
                    var operand = Operands.Pop();
                    if (theirs > Precedence.Exponential)
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
            ReadPast(operation);
        }

        private void ParseParameter(string token)
        {
            var operand = Expressions.x;
            Operands.Push(operand);
            ReadPast(token);
        }

        private void ParseUnary(string unary)
        {
            Operations.Push($"u{unary}");
            ReadPast(unary);
        }

        private void ReadPast(string token) => Index += token.Length;
    }
}
