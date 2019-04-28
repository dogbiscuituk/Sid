namespace Sid.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    public class Parser
    {
        #region Public interface

        public Expression Parse(string formula)
        {
            if (formula == string.Empty)
                return Expression.Empty();
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

        #endregion

        #region Fields

        private string Formula;
        private int Index;
        private Stack<Expression> Operands;
        private Stack<string> Operators;

        #endregion

        #region Make methods

        private Expression MakeBinary(string op, Expression left, Expression right)
        {
            switch (op.GetOperandTypes())
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
            return Expression.MakeBinary(op.GetExpressionType(), left, right);
        }

        private Expression MakeConditional(Expression test, Expression then, Expression otherwise) =>
            Expression.Condition(test.ToBoolean(), then, otherwise);

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
            {
                var cValue = (double)c.Value;
                switch (op)
                {
                    case Ops.UnaryMinus:
                        return (-cValue).Constant();
                    case "!":
                    case "~":
                        return (cValue == 0.0 ? 1.0 : 0.0).Constant();
                    case Ops.SquareRoot:
                        return Math.Sqrt(cValue).Constant();
                }
            }
            switch (op)
            {
                case Ops.UnaryPlus:
                    return operand;
                case Ops.SquareRoot:
                    return MakeFunction("Sqrt", operand);
            }
            return Expression.MakeUnary(op.GetExpressionType(), operand, null);
        }

        #endregion

        #region Match methods

        private string MatchFunction() => MatchRegex(@"^[\p{Lu}\p{Ll}\d]+").ToLower();

        private string MatchNumber() => MatchRegex(@"^\d*\.?\d*([eE][+-]?\d+)?");

        private string MatchRegex(string pattern)
        {
            var match = Regex.Match(Formula.Substring(Index), pattern);
            return Formula.Substring(Index + match.Index, match.Length);
        }

        private string MatchSubscript() => MatchRegex($"^[{Utility.Subscripts}]+");
        private string MatchSuperscript() => MatchRegex($"^[{Utility.Superscripts}]+");

        #endregion

        #region Parse methods

        private void ParseExpression()
        {
            do
            {
                ParseOperand();
            nextOperator:
                var op = PeekToken();
                switch (op)
                {
                    case ")":
                    case "?":
                    case ":":
                    case "||":
                    case "&&":
                    case "|":
                    case "&":
                    case "=":
                    case "==":
                    case "≠":
                    case "<>":
                    case "!=":
                    case "<":
                    case ">":
                    case "≮":
                    case ">=":
                    case "≯":
                    case "<=":
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                    case "^":
                        ParseOperator(op.ToString());
                        if (op == ")")
                            return;
                        break;
                    case "'": // Postfix apostrophe => differentiate
                        ParseTick();
                        goto nextOperator;
                    case "$" when Index == Formula.Length + 2: // End of input (normal)
                        return;
                    case "$" when Index < Formula.Length + 2: // End of input (unexpected)
                        throw new FormatException(
                            $"Unexpected end of text, input='{Formula}', index={Index}");
                    default:
                        switch (op[0])
                        {
                            case char c when c.IsSuperscript():
                                ParseOperator(Ops.SuperscriptPower); //
                                break;
                            default:
                                if (Operands.Peek() is ConstantExpression)
                                {
                                    ParseOperator(Ops.ImpliedProduct); // Implied multiplication
                                    break;
                                }
                                throw new FormatException(
                                    $"Unexpected token '{op}', input='{Formula}', index={Index}");
                        }
                        break;
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
            var value = constant.GetNamedValue();
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
            var token = PeekToken();
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
                case '!':
                case '~':
                case '√':
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
                var ours = op.GetPrecedence();
                var pending = Operators.Peek();
                if (pending == "(")
                    break;
                var theirs = pending.GetPrecedence();
                // Operator '^' is right associative: a^b^c = a^(b^c).
                if (theirs > ours || theirs == ours && op != "^")
                {
                    Operators.Pop();
                    var operand = Operands.Pop();
                    if (theirs == Precedence.Unary)
                        switch (pending)
                        {
                            case Ops.UnaryPlus:
                                break;
                            case Ops.UnaryMinus:
                            case "!":
                            case "~":
                                operand = MakeUnary(pending, operand);
                                break;
                            case Ops.SquareRoot:
                                operand = MakeFunction("Sqrt", operand);
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
                        if (pending == Ops.ImpliedProduct)
                            pending = "*";
                        if (pending == ":") // End of a conditional
                        {
                            var otherwise = operand;
                            var then = Operands.Pop();
                            Operators.Pop(); // Must be '?'
                            var test = Operands.Pop();
                            operand = MakeConditional(test, operand, Operands.Pop());
                        }
                        else
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
            if (op != Ops.ImpliedProduct && op != Ops.SuperscriptPower)
                ReadPast(op);
        }

        private void ParseParameter(string token)
        {
            Operands.Push(Expressions.x);
            ReadPast(token);
        }

        private void ParseSuperscript(string superscript)
        {
            Operands.Push(new Parser().Parse(superscript.FromSuperscript()));
            ReadPast(superscript);
        }

        private void ParseTick()
        {
            Operands.Push(Operands.Pop().Differentiate());
            ReadPast("'");
        }

        private void ParseUnary(string unary)
        {
            Operators.Push($"({unary})");
            ReadPast(unary);
        }

        #endregion

        #region Read methods

        private char PeekChar()
        {
            var count = Formula.Length;
            while (Index < count && Formula[Index] == ' ')
                Index++;
            return Index < count ? Formula[Index] : Index == count ? ')' : '$';
        }

        private string PeekToken()
        {
            var nextChar = PeekChar();
            if ("()?:≠≮≯+-*/^~√'".IndexOf(nextChar) >= 0)
                return nextChar.ToString();
            switch (nextChar)
            {
                case char c when char.IsDigit(c):
                case '.':
                    return MatchNumber();
                case char c when c.IsSuperscript():
                    return MatchSuperscript();
                case char c when char.IsLetter(c):
                    return MatchFunction();
            }
            if ("|&=<>!".IndexOf(nextChar) >= 0)
            {
                var lookahead = Formula.Substring(Index, 2);
                switch (lookahead)
                {
                    case "||":
                    case "&&":
                    case "==":
                    case "<>":
                    case "!=":
                    case "<=":
                    case ">=":
                        return lookahead;
                }
                return nextChar.ToString();
            }
            throw new FormatException(
                $"Unexpected character '{nextChar}', input='{Formula}', index={Index}");
        }

        private void ReadPast(string token) => Index += token.Length;

        #endregion
    }
}
