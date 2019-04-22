namespace FormulaBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            Functional,
            ImpliedProduct,
            SuperscriptPower
        }

        const string
            ImpliedProduct = "i*",
            SuperscriptPower = "s^",
            UnaryMinus = "u-",
            UnaryPlus = "u+";

        private string Formula;
        private int Index;
        private Stack<Expression> Operands;
        private Stack<string> Operators;

        public Expression Parse(string formula)
        {
            Formula = formula;
            Index = 0;
            Operands = new Stack<Expression>();
            Operators = new Stack<string>(new[] { "(" });
            ParseExpression();
            if (Operators.Any())
                throw new FormatException(
                    $"Unexpected end of expression, input='{Formula}'");
            return Operands.Peek();
        }

        public bool TryParse(string formula, out object result)
        {
            try
            {
                result = Parse(formula);
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;
                return false;
            }
        }

        private static double GetNamedConstantValue(string constant)
        {
            switch (constant.ToLower())
            {
                case "e":
                    return Math.E;
                case "π":
                case "pi":
                    return Math.PI;
                case "ϕ":
                case "phi":
                    return (1 + Math.Sqrt(5)) / 2;
            }
            return 0;
        }

        private static ExpressionType GetExpressionType(string op)
        {
            switch (op)
            {
                case "+":
                    return ExpressionType.Add;
                case "-":
                    return ExpressionType.Subtract;
                case "*":
                case "i*":
                    return ExpressionType.Multiply;
                case "/":
                    return ExpressionType.Divide;
                case "^":
                case "s^":
                    return ExpressionType.Power;
                case UnaryPlus:
                    return ExpressionType.UnaryPlus;
                case UnaryMinus:
                    return ExpressionType.Negate;
            }
            throw new FormatException();
        }

        private static Precedence GetPrecedence(string op)
        {
            switch (op)
            {
                case ")":
                    return Precedence.RightParenthesis;
                case "+":
                case "-":
                    return Precedence.Additive;
                case "*":
                case "/":
                    return Precedence.Multiplicative;
                case "^":
                    return Precedence.Exponential;
                case ImpliedProduct:
                    return Precedence.ImpliedProduct;
                case SuperscriptPower:
                    return Precedence.SuperscriptPower;
            }
            return Precedence.Functional;
        }

        private Expression MakeBinary(string op, Expression lhs, Expression rhs) =>
            Expression.MakeBinary(GetExpressionType(op), lhs, rhs);

        private Expression MakeFunction(string f, Expression operand)
        {
            f = $"{char.ToUpper(f[0])}{f.ToLower().Substring(1)}";
            var result = Expressions.Function(f, operand);
            if (operand is ConstantExpression c)
                return result.AsDouble((double)c.Value).Constant();
            return result;
        }

        private Expression MakeUnary(string op, Expression operand)
        {
            if (operand is ConstantExpression c)
                switch (op)
                {
                    case UnaryPlus: return operand;
                    case UnaryMinus: return (-(double)c.Value).Constant();
                }
            return Expression.MakeUnary(GetExpressionType(op), operand, null);
        }

        private string MatchFunction() => MatchRegex(@"^\w+").ToLower();

        private string MatchNumber() => MatchRegex(@"^\d*\.?\d*([eE][+-]?\d+)?");

        private string MatchRegex(string pattern)
        {
            var match = Regex.Match(Formula.Substring(Index), pattern);
            return Formula.Substring(Index + match.Index, match.Length);
        }

        private string MatchSuperscript() => MatchRegex(@"^[⁺⁻]?[⁰¹²³⁴⁵⁶⁷⁸⁹]+");

        private char NextChar()
        {
            var count = Formula.Length;
            while (Index < count && Formula[Index] == ' ')
                Index++;
            return Index < count ? Formula[Index] : Index == count ? ')' : '$';
        }

        private string NextToken()
        {
            var nextChar = NextChar();
            switch (nextChar)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '^':
                case '(':
                case ')':
                    return nextChar.ToString();
                case char c when char.IsDigit(c):
                case '.':
                    return MatchNumber();
                case char c when c.IsSuperscript():
                    return MatchSuperscript();
                case char c when char.IsLetter(c):
                    return MatchFunction();
            }
            throw new FormatException(
                $"Unexpected character '{nextChar}', input='{Formula}', index={Index}");
        }

        private void ParseExpression()
        {
            do
            {
                ParseOperand();
                var op = NextChar();
                switch (op)
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '^':
                    case ')':
                        ParseOperator(op.ToString());
                        if (op == ')')
                            return;
                        break;
                    case '$' when Index == Formula.Length + 2: // End of input (normal)
                        return;
                    case '$' when Index < Formula.Length + 2: // End of input (unexpected)
                        throw new FormatException(
                            $"Unexpected end of text, input='{Formula}', index={Index}");
                    case char c when c.IsSuperscript():
                        ParseOperator(SuperscriptPower); //
                        break;
                    default:
                        if (Operands.Peek() is ConstantExpression)
                        {
                            ParseOperator(ImpliedProduct); // Implied multiplication
                            break;
                        }
                        throw new FormatException(
                            $"Unexpected character '{op}', input='{Formula}', index={Index}");
                }
            }
            while (true);
        }

        private void ParseFunction(string function)
        {
            Operators.Push(function);
            ReadPast(function);
        }

        private bool ParseNamedConstant(string constant)
        {
            var value = GetNamedConstantValue(constant);
            if (value == 0)
                return false;
            Operands.Push(value.Constant());
            ReadPast(constant);
            return true;
        }

        private void ParseNumber(string number)
        {
            try
            {
                Operands.Push(double.Parse(number).Constant());
            }
            catch (FormatException)
            {
                throw new FormatException(
                    $"Invalid number format '{number}', input='{Formula}', index={Index}");
            }
            catch (OverflowException)
            {
                throw new FormatException(
                    $"Numerical overflow '{number}', input='{Formula}', index={Index}");
            }
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
                case char c when c.IsSuperscript():
                    ParseSuperscript(token);
                    break;
                case '(':
                    Operators.Push(token);
                    ReadPast(token);
                    ParseExpression();
                    break;
                case '+':
                case '-':
                    ParseUnary(token);
                    ParseOperand();
                    break;
                case char c when char.IsLetter(c):
                    if (!ParseNamedConstant(token))
                    {
                        ParseFunction(token);
                        ParseOperand();
                    }
                    break;
                default:
                    throw new FormatException(
                        $"Missing operand, input='{Formula}', index={Index}");
            }
        }

        private void ParseOperator(string op)
        {
            do
            {
                var ours = GetPrecedence(op);
                var pending = Operators.Peek();
                if (pending == "(")
                    break;
                var theirs = GetPrecedence(pending);
                // Operator '^' is right associative: a^b^c = a^(b^c).
                if (theirs > ours || theirs == ours && op != "^")
                {
                    Operators.Pop();
                    var operand = Operands.Pop();
                    if (theirs == Precedence.Functional)
                        switch (pending)
                        {
                            case UnaryPlus:
                                break;
                            case UnaryMinus:
                                operand = MakeUnary(pending, operand);
                                break;
                            default:
                                try
                                {
                                    operand = MakeFunction(pending, operand);
                                }
                                catch (ArgumentNullException)
                                {
                                    throw new FormatException(
                                        $"Unrecognised function '{pending}', input='{Formula}'");
                                };
                                break;
                        }
                    else
                    {
                        if (pending == ImpliedProduct)
                            pending = "*";
                        operand = MakeBinary(pending, Operands.Pop(), operand);
                    }
                    Operands.Push(operand);
                }
                else
                    break;
            }
            while (true);
            if (op == ")")
                Operators.Pop();
            else
                Operators.Push(op);
            if (op != ImpliedProduct && op != SuperscriptPower)
                ReadPast(op);
        }

        private void ParseParameter(string token)
        {
            Operands.Push(Expressions.x);
            ReadPast(token);
        }

        private void ParseSuperscript(string number)
        {
            ParseNumber(number.SuperscriptToNormal());
        }

        private void ParseUnary(string unary)
        {
            Operators.Push($"u{unary}");
            ReadPast(unary);
        }

        private void ReadPast(string token) => Index += token.Length;
    }
}
