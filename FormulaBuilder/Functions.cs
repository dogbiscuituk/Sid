namespace FormulaBuilder
{
    using System;

    /// <summary>
    /// A couple of math functions were either unavailable (Step) or unsuitable (Sign,
    /// which has an integer output), so they are provided in this class. Might as well
    /// include the whole System.Math lot here too; then we never need perform two
    /// reflection operations to find any function by name.
    /// </summary>
    public class Functions
    {
        public static double Abs(double x) => Math.Abs(x);
        public static double Acos(double x) => Math.Acos(x);
        public static double Asin(double x) => Math.Asin(x);
        public static double Atan(double x) => Math.Atan(x);
        public static double Ceiling(double x) => Math.Ceiling(x);
        public static double Cos(double x) => Math.Cos(x);
        public static double Cosh(double x) => Math.Cosh(x);
        public static double Exp(double x) => Math.Exp(x);
        public static double Floor(double x) => Math.Floor(x);
        public static double Log(double x) => Math.Log(x);
        public static double Log10(double x) => Math.Log10(x);
        public static double Round(double x) => Math.Round(x);
        public static double Sign(double x) => Math.Sign(x);
        public static double Sin(double x) => Math.Sin(x);
        public static double Sinh(double x) => Math.Sinh(x);
        public static double Sqrt(double x) => Math.Sqrt(x);
        public static double Step(double x) => x < 0 ? 0 : 1;
        public static double Tan(double x) => Math.Tan(x);
        public static double Tanh(double x) => Math.Tanh(x);
    }
}
