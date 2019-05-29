namespace ToyGraf.Expressions
{
    /// <summary>
    /// Encodings to avoid ambiguity while processing certain operators.
    /// </summary>
    public static class Ops
    {
        public const string
            UnaryPlus = "(+)",
            UnaryMinus = "(-)",
            SquareRoot = "(√)",
            CubeRoot = "(∛)",
            FourthRoot = "(∜)",
            ImpliedProduct = "*i",
            SuperscriptPower = "^s",
            SuperscriptPowerSwap = "s^";
    }
}
