namespace FormulaBuilder
{
    using System;

    /// <summary>
    /// Several math functions are either absent from System.Math (Asec, Sech, Asech,
    /// Step) or unsuitable (Sign, which has an integer output), so they are provided
    /// in this class. Might as well include the whole System.Math lot here too; then
    /// we never need perform two reflection operations to find any function by name.
    /// </summary>
    public class Functions
    {
        public static double Abs(double x) => Math.Abs(x);
        public static double Acos(double x) => Math.Acos(x);
        public static double Acosh(double x) => Math.Log(x + Math.Sqrt(Math.Pow(x, 2) - 1));
        public static double Acot(double x) => Math.Atan(1 / x);
        public static double Acoth(double x) => Math.Log((x + 1) / (x - 1)) / 2;
        public static double Acsc(double x) => Math.Asin(1 / x);
        public static double Acsch(double x) => Math.Log((1 + Math.Sqrt(1 + Math.Pow(x, 2))) / x);
        public static double Asec(double x) => Math.Acos(1 / x);
        public static double Asech(double x) => Math.Log((1 + Math.Sqrt(1 - Math.Pow(x, 2))) / x);
        public static double Asin(double x) => Math.Asin(x);
        public static double Asinh(double x) => Math.Log(x + Math.Sqrt(Math.Pow(x, 2) + 1));
        public static double Atan(double x) => Math.Atan(x);
        public static double Atanh(double x) => Math.Log((1 + x) / (1 - x)) / 2;
        public static double Ceiling(double x) => Math.Ceiling(x);
        public static double Cos(double x) => Math.Cos(x);
        public static double Cosh(double x) => Math.Cosh(x);
        public static double Cot(double x) => 1 / Math.Tan(x);
        public static double Coth(double x) => 1 / Math.Tanh(x);
        public static double Csc(double x) => 1 / Math.Sin(x);
        public static double Csch(double x) => 1 / Math.Sinh(x);
        public static double Exp(double x) => Math.Exp(x);
        public static double Floor(double x) => Math.Floor(x);
        public static double Ln(double x) => Math.Log(x);
        public static double Log10(double x) => Math.Log10(x);
        public static double Round(double x) => Math.Round(x);
        public static double Sec(double x) => 1 / Math.Cos(x);
        public static double Sech(double x) => 1 / Math.Cosh(x);
        public static double Sign(double x) => Math.Sign(x);
        public static double Sin(double x) => Math.Sin(x);
        public static double Sinh(double x) => Math.Sinh(x);
        public static double Sqrt(double x) => Math.Sqrt(x);
        public static double Step(double x) => x < 0 ? 0 : 1;
        public static double Tan(double x) => Math.Tan(x);
        public static double Tanh(double x) => Math.Tanh(x);
    }
}
