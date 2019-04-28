namespace Sid.Expressions
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
            Assignment,     // 
            Ternary,        // x<0 ? -x : +x
            LogicalOr,      // x<1 || x>2
            LogicalAnd,     // x>1 && x<2
            BitwiseOr,      // x<1 | x>2
            BitwiseAnd,     // x>1 & x<2
            Equality,       // x=2
            Relational,     // x>2
            Additive,       // x+2, x-2
            Multiplicative, // x*2. x/2
            Exponential,    // x^2
            Unary,          // +x, -x, sin x
            Implied,        // 2x
            Derivative,     // (sin x)'
            Superscript     // x²
        }

        const string
            ImpliedProduct = "i*",
            SuperscriptPower = "s^",
            UnaryMinus = "(-)",
            UnaryPlus = "(+)",
            SquareRoot = "(√)";

        private string Formula;
        private int Index;
        private Stack<Expression> Operands;
        private Stack<string> Operators;

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
                case "?":
                case ":":
                    return ExpressionType.Conditional;
                case "|":
                case "||":
                    return ExpressionType.Or;
                case "&":
                case "&&":
                    return ExpressionType.And;
                case "=":
                case "==":
                    return ExpressionType.Equal;
                case "≠":
                case "<>":
                case "!=":
                    return ExpressionType.NotEqual;
                case "<":
                    return ExpressionType.LessThan;
                case ">":
                    return ExpressionType.GreaterThan;
                case "≯":
                case "<=":
                    return ExpressionType.LessThanOrEqual;
                case "≮":
                case ">=":
                    return ExpressionType.GreaterThanOrEqual;
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
                case "!":
                case "~":
                    return ExpressionType.Not;
            }
            throw new FormatException();
        }

        private static Precedence GetPrecedence(string op)
        {
            switch (op)
            {
                case ")":
                    return Precedence.Assignment;
                case "?":
                case ":":
                    return Precedence.Ternary;
                case "||":
                    return Precedence.LogicalOr;
                case "&&":
                    return Precedence.LogicalAnd;
                case "|":
                    return Precedence.BitwiseOr;
                case "&":
                    return Precedence.BitwiseAnd;
                case "=":
                case "==":
                case "≠":
                case "<>":
                case "!=":
                    return Precedence.Equality;
                case "<":
                case ">":
                case "≮":
                case ">=":
                case "≯":
                case "<=":
                    return Precedence.Relational;
                case "+":
                case "-":
                    return Precedence.Additive;
                case "*":
                case "/":
                    return Precedence.Multiplicative;
                case "^":
                    return Precedence.Exponential;
                case ImpliedProduct:
                    return Precedence.Implied;
                case "'":
                    return Precedence.Derivative;
                case SuperscriptPower:
                    return Precedence.Superscript;
            }
            return Precedence.Unary;
        }

        private Expression MakeBinary(string op, Expression lhs, Expression rhs) =>
            Expression.MakeBinary(GetExpressionType(op), lhs, rhs);

        private Expression MakeConditional(Expression test, Expression then, Expression otherwise) =>
            Expression.Condition(test, then, otherwise);

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
                    case UnaryMinus:
                        return (-cValue).Constant();
                    case "!":
                    case "~":
                        return (cValue == 0.0 ? 1.0 : 0.0).Constant();
                    case SquareRoot:
                        return Math.Sqrt(cValue).Constant();
                }
            }
            switch (op)
            {
                case UnaryPlus:
                    return operand;
                case SquareRoot:
                    return MakeFunction("Sqrt", operand);
            }
            return Expression.MakeUnary(GetExpressionType(op), operand, null);
        }

        private string MatchFunction() => MatchRegex(@"^[\p{Lu}\p{Ll}\d]+").ToLower();

        private string MatchNumber() => MatchRegex(@"^\d*\.?\d*([eE][+-]?\d+)?");

        private string MatchRegex(string pattern)
        {
            var match = Regex.Match(Formula.Substring(Index), pattern);
            return Formula.Substring(Index + match.Index, match.Length);
        }

        private string MatchSubscript() => MatchRegex($"^[{Utility.Subscripts}]+");
        private string MatchSuperscript() => MatchRegex($"^[{Utility.Superscripts}]+");

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

        private void ParseExpression()
        {
            do
            {
                ParseOperand();
            nextOperator:
                var op = NextToken();
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
                                ParseOperator(SuperscriptPower); //
                                break;
                            default:
                                if (Operands.Peek() is ConstantExpression)
                                {
                                    ParseOperator(ImpliedProduct); // Implied multiplication
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
                var ours = GetPrecedence(op);
                var pending = Operators.Peek();
                if (pending == "(")
                    break;
                var theirs = GetPrecedence(pending);
                // Operator '^' is right associative: a^b^c = a^(b^c).
                // Also, both ternary operators in a?b:c must be pended.
                if (theirs > ours || theirs == ours && op != "^" && op != "?")
                {
                    Operators.Pop();
                    var operand = Operands.Pop();
                    if (theirs == Precedence.Unary)
                        switch (pending)
                        {
                            case UnaryPlus:
                                break;
                            case UnaryMinus:
                            case "!":
                            case "~":
                                operand = MakeUnary(pending, operand);
                                break;
                            case SquareRoot:
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
                        if (pending == ImpliedProduct)
                            pending = "*";
                        if (op == ":") // End of a conditional
                        {
                            var then = Operands.Pop();
                            operand = MakeConditional(Operands.Pop(), then, operand);
                            Operators.Pop();
//                            if (Operators.Peek() != "?")
//                                throw new FormatException(
//                                    $"Badly formed conditional, input='{Formula}', index={Index}");
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
            if (op != ImpliedProduct && op != SuperscriptPower)
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

        private void ReadPast(string token) => Index += token.Length;
    }
}
