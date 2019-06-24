namespace ToyGraf.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using ToyGraf.Expressions.Enumerations;

    public class Parser
    {
        #region Public interface

        public Parser() : this(Language.ToyGraf) { }

        public Parser(Language language) => Language = language;

        /// <summary>
        /// Convert the string representation of an expression to a System.Linq.Expressions equivalent.
        /// </summary>
        /// <param name="formula">The string representation of an expression.</param>
        /// <returns>A System.Linq.Expressions.Expression equivalent to the input string./returns>
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
            return Operands.Pop();
        }

        /// <summary>
        /// Convert the string representation of an expression to a System.Linq.Expressions equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="formula">The string representation of an expression.</param>
        /// <param name="result">On success, a System.Linq.Expressions.Expression equivalent to the input string.
        /// On failure, an exception (usually System.FormatException) detailing the reason for the failure.</param>
        /// <returns>True if the conversion succeeded, otherwise false.</returns>
        public bool TryParse(string formula, out Expression result, out string error)
        {
            try
            {
                result = Parse(formula);
                error = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                result = null;
                error = e.Message;
                return false;
            }
        }

        public bool TryParseConstant(string formula, out double result)
        {
            var ok = true;
            result = 0;
            try
            {
                var expression = Parse(formula).Simplify();
                if (expression is ConstantExpression ce)
                    result = (double)ce.Value;
                else
                    ok = false;
            }
            catch (Exception) { ok = false; }
            return ok;
        }

        public Language Language;

        #endregion

        #region Private Fields

        private string Formula;
        private int Index;
        private Stack<Expression> Operands;
        private Stack<string> Operators;

        /// <summary>
        /// Intended for use in the debugger, the two properties OperandStack and OperatorStack
        /// afford a graphic view of each stack's contents, with the stack top at the right-hand
        /// end, and elements separated by vertical bars " | ". There is no protection against an
        /// empty stack, because these properties are intended never to be accessed by client code,
        /// only by the debugger.
        /// </summary>
        private string OperandStack => Operands.Reverse().Select(p => p.AsString()).Aggregate((p, q) => $"{p} | {q}");
        private string OperatorStack => Operators.Reverse().Aggregate((p, q) => $"{p} | {q}");

        #endregion

        #region Private Integrate Methods

        private void IntegrateStacktop(string wrt)
        {
            var operand = Operands.Pop();
            operand = operand.Integrate(wrt);
            Operands.Push(operand);
        }

        #endregion

        #region Private Match methods

        private string MatchFunction() => MatchRegex($@"^[\p{{Lu}}\p{{Ll}}\d]+[{Utility.Subscripts}]*").ToLower();
        private string MatchNumber() => MatchRegex(@"^\d*\.?\d*([eE][+-]?\d+)?");
        private string MatchRegex(string pattern) => Regex.Match(Formula.Substring(Index), pattern).Value;
        private string MatchSubscript() => MatchRegex($"^[{Utility.Subscripts}]+");
        private string MatchSuperscript() => MatchRegex($"^[{Utility.Superscripts}]+");

        #endregion

        #region Private Parse methods

        private void ParseDegree()
        {
            if (!(Operands.Pop() is ConstantExpression c))
                throw new FormatException($"Unexpected token '°', input='{Formula}', index={Index}");
            Operands.Push(((double)c.Value).DegreesToRadians().Constant());
            ReadPast('°');
        }

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
                    case "dx":
                    case "dt":
                    case ",":
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
                    case "≯":
                    case "≤":
                    case "≥":
                    case "<=":
                    case ">=":
                    case "≰":
                    case "≱":
                    case "+":
                    case "-":
                    case "*":
                    case "×":
                    case "/":
                    case "⁄": // Unicode Fraction Slash (U+2044)
                    case "÷":
                    case "^":
                        ParseOperator(op);
                        switch (op)
                        {
                            case "dx":
                                IntegrateStacktop("x");
                                return;
                            case "dt":
                                IntegrateStacktop("t");
                                return;
                            case ")":
                                return;
                        }
                        break;
                    case "°": // Postfix degree symbol => convert to radians
                        ParseDegree();
                        goto nextOperator;
                    case "'": // Postfix apostrophe => differentiate
                        ParseTick();
                        goto nextOperator;
                    case "$" when Index == Formula.Length + 2: // End of input (normal)
                        return;
                    case "$" when Index < Formula.Length + 2: // End of input (unexpected)
                        throw new FormatException($"Unexpected end of text, input='{Formula}', index={Index}");
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
            ReadPast(function);
            if (PeekChar().IsSuperscript())
            {
                var token = PeekToken();
                ParseSuperscript(token);
                Operators.Push(Ops.SuperscriptPowerSwap);
            }
            Operators.Push(function);
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
                case 't' when token.Length == 1:
                    ParseParameter(token);
                    break;
                case char c when char.IsDigit(c):
                case '.':
                    ParseNumber(token);
                    break;
                case char c when c.IsSubscript():
                    ParseSubscript(token);
                    break;
                case char c when c.IsSuperscript():
                    ParseSuperscript(token);
                    break;
                case '(':
                case '∫':
                    Operators.Push(token);
                    ReadPast(token);
                    ParseExpression();
                    break;
                case '+':
                case '-':
                case '!':
                case '~':
                case '√':
                case '∛':
                case '∜':
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
                case char c when char.IsNumber(c):
                    ParseVulgarFraction(c);
                    break;
                default:
                    throw new FormatException(
                        $"Missing operand, input='{Formula}', index={Index}");
            }
        }

        private void ParseOperator(string newOp)
        {
            do
            {
                var ours = GetPrecedence(newOp);
                var oldOp = Operators.Peek();
                if (oldOp == "(" || oldOp == "∫")
                    break;
                var theirs = GetPrecedence(oldOp);
                if (ours < theirs || ours == theirs && newOp != "^" // Operator '^' is right associative: a^b^c = a^(b^c).
                    && oldOp != "?" && newOp != "?") // Conditional '?:' needs all 3 operands assembled before evaluation.
                {
                    Operators.Pop();
                    var operand = Operands.Pop();
                    // if (theirs == Precedence.Unary)
                    if (theirs == Precedence.Unary || oldOp == Ops.UnaryMinus)
                        switch (oldOp)
                        {
                            case Ops.UnaryPlus:
                                break;
                            case Ops.UnaryMinus:
                            case "!":
                            case "~":
                                operand = oldOp.MakeUnary(operand);
                                break;
                            case Ops.SquareRoot:
                                operand = "sqrt".MakeFunction(operand);
                                break;
                            case Ops.CubeRoot:
                                operand = operand.Power(1.0 / 3.0);
                                break;
                            case Ops.FourthRoot:
                                operand = operand.Power(0.25);
                                break;
                            default:
                                try
                                {
                                    operand = oldOp.MakeFunction(operand);
                                }
                                catch (ArgumentNullException)
                                {
                                    throw new FormatException(
                                        $"Unrecognised function '{oldOp}', input='{Formula}'");
                                };
                                break;
                        }
                    else
                    {
                        if (oldOp == Ops.ImpliedProduct)
                            oldOp = "*";
                        if (oldOp == ":") // End of a conditional
                        {
                            if (Operators.Peek() != "?") // Must be '?'
                                throw new FormatException($"Missing '?', input='{Formula}'");
                            Operators.Pop(); // Discard '?'
                            var then = Operands.Pop();
                            operand = Operands.Pop().MakeConditional(then, operand);
                        }
                        else
                        {
                            var left = Operands.Pop();
                            if (GetPrecedence(oldOp) == Precedence.Relational && left.IsRelational())
                                operand = "&".MakeBinary(left,
                                    oldOp.MakeBinary(left.GetRightmostRelation(), operand));
                            else if (oldOp == Ops.SuperscriptPowerSwap)
                                operand = Ops.SuperscriptPower.MakeBinary(operand, left);
                            else if (left is ConstantExpression ce && (double)ce.Value == Math.E && oldOp == "^")
                                operand = "exp".MakeFunction(operand);
                            else
                                operand = oldOp.MakeBinary(left, operand);
                        }
                    }
                    Operands.Push(operand);
                }
                else
                    break;
            }
            while (true);
            if (newOp == ")" || newOp == "dx" || newOp == "dt")
                Operators.Pop();
            else
                Operators.Push(newOp);
            if (newOp != Ops.ImpliedProduct && newOp != Ops.SuperscriptPower)
                ReadPast(newOp);
        }

        private void ParseParameter(string token)
        {
            switch (token)
            {
                case "x":
                    Operands.Push(Expressions.x);
                    break;
                case "t":
                    Operands.Push(Expressions.t);
                    break;
            }
            ReadPast(token);
        }

        private void ParseScript(string token)
        {
            Operands.Push(new Parser().Parse(token));
            ReadPast(token);
        }

        private void ParseSubscript(string token) => ParseScript(token.FromSubscript());
        private void ParseSuperscript(string token) => ParseScript(token.FromSuperscript());

        private void ParseTick()
        {
            Operands.Push(Operands.Pop().Differentiate());
            ReadPast('\'');
        }

        private void ParseUnary(string token)
        {
            Operators.Push($"({token})");
            ReadPast(token);
        }

        private void ParseVulgarFraction(char c)
        {
            Operands.Push(c.VulgarFractionToDouble().Constant());
            ReadPast(c);
        }

        #endregion

        #region Private Read methods

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
            if ("()?:≠≤≥≮≯≰≱+-*/⁄^~°√∛∜',∫".IndexOf(nextChar) >= 0)
                return nextChar.ToString();
            switch (nextChar)
            {
                case char c when char.IsDigit(c):
                case '.':
                    return MatchNumber();
                case char c when c.IsSubscript():
                    return MatchSubscript();
                case char c when c.IsSuperscript():
                    return MatchSuperscript();
                case char c when char.IsLetter(c):
                    if (c == 'd' && Index < Formula.Length - 2)
                    {
                        var lookahead = Formula.Substring(Index, 2);
                        switch (lookahead)
                        {
                            case "dx":
                            case "dt":
                                return lookahead;
                        }
                    }
                    return MatchFunction();
                case char c when char.IsNumber(c):
                    return nextChar.ToString();
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

        private void ReadPast(char c) => Index++;
        private void ReadPast(string token) => Index += token.Length;

        #endregion

        #region Private Utility methods

        public Precedence GetPrecedence(string op)
        {
            switch (op)
            {
                case ")":
                case "dx":
                case "dt":
                    return Precedence.Assignment;
                case ",":
                    return Precedence.Sequential;
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
                case "≯":
                case "≤":
                case "≥":
                case "<=":
                case ">=":
                case "≰":
                case "≱":
                    return Precedence.Relational;
                case "+":
                case "-":
                    return Precedence.Additive;
                case "*":
                case "×":
                case "/":
                case "÷":
                    return Precedence.Multiplicative;
                case "^":
                    return Precedence.Exponential;
                case Ops.ImpliedProduct:
                    return Precedence.Implied;
                case "'":
                    return Precedence.Postfix;
                case Ops.SuperscriptPower:
                case Ops.SuperscriptPowerSwap:
                    return Precedence.Superscript;
                case "⁄": // Unicode Fraction Slash (U+2044)
                    return Precedence.Fraction;
                case Ops.UnaryMinus:
                    switch (Language)
                    {
                        case Language.ToyGraf:
                            return Precedence.Unary;
                        default:
                            return Precedence.Additive;
                    }
            }
            return Precedence.Unary;
        }

        #endregion
    }
}
