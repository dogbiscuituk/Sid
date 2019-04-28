namespace Sid.Expressions
{
    /// <summary>
    /// Encodings to avoid ambiguity while processing certain operators.
    /// </summary>
    public static class Ops
    {
        public const string
            ImpliedProduct = "i*",
            SquareRoot = "(√)",
            SuperscriptPower = "s^",
            UnaryMinus = "(-)",
            UnaryPlus = "(+)";
    }
}
