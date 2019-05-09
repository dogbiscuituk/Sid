﻿namespace ToyGraf.Expressions
{
    /// <summary>
    /// The precedence levels attributed to operators.
    /// </summary>
    public enum Precedence
    {
        Assignment,     // End of expression
        Sequential,     // f1(x,t)
        Ternary,        // x<0 ? -x : +x
        LogicalOr,      // x<1 || x>2
        LogicalAnd,     // x>1 && x<2
        BitwiseOr,      // x<1 | x>2
        BitwiseAnd,     // x>1 & x<2
        Equality,       // x=2, x!=2
        Relational,     // x>2, x<=2
        Additive,       // x+2, x-2
        Multiplicative, // x*2, x/2
        Exponential,    // x^2, e^x
        Unary,          // +x, -x, √x, sin x
        Implied,        // 2x
        Postfix,        // (sin x)'
        Superscript     // x²
    }
}
