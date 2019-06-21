namespace ToyGraf.Expressions
{
    using System;

    /// <summary>
    /// Several math functions are either absent from System.Math (Asec, Sech, Asech,
    /// Hstep) or unsuitable (Sign, which has an integer output), so they are provided
    /// in this class. Might as well include the whole System.Math lot here too; then
    /// we never need perform two reflection operations to find any function by name.
    /// </summary>
    public static class Functions
    {
        #region Elementary Functions

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
        public static double Hstep(double x) => x < 0 ? 0 : 1;
        public static double Log(double x) => Math.Log(x);
        public static double Log10(double x) => Math.Log10(x);
        public static double Round(double x) => Math.Round(x);
        public static double Sec(double x) => 1 / Math.Cos(x);
        public static double Sech(double x) => 1 / Math.Cosh(x);
        public static double Sign(double x) => Math.Sign(x);
        public static double Sin(double x) => Math.Sin(x);
        public static double Sinh(double x) => Math.Sinh(x);
        public static double Sqrt(double x) => Math.Sqrt(x);
        public static double Tan(double x) => Math.Tan(x);
        public static double Tanh(double x) => Math.Tanh(x);

        #endregion

        #region Non-elementary Functions

        private static readonly double[] erf_a =
        {
            -1.26551223, +1.00002368, +0.37409196, +0.09678418, -0.18628806,
            +0.27886807, -1.13520398, +1.48851587, -0.82215223, +0.17087277
        };

        /// <summary>
        /// An approximation, with a worst case error of 1.2e-7, from the book
        /// "Numerical Recipes in Fortran 77: The Art of Scientific Computing"
        /// (ISBN 0-521-43064-X), 1992, page 214, Cambridge University Press.
        /// </summary>
        /// <param name="x">The input value to the function Erf(x).</param>
        /// <returns>An approximation to the value of erf(x).</returns>
        public static double Erf(double x)
        {
            double t = 1 / (1 + Math.Abs(x) / 2), u = 1, v = 0;
            for (var n = 0; n < 10; v += erf_a[n] * u, n++, u *= t) ;
            return (1 - t * Math.Exp(v - x * x)) * Math.Sign(x);
        }

        public static double Erfc(double x) => 1 - Erf(x);

        #endregion

        #region User Defined Functions

        /// <summary>
        /// Placeholder in expressions, to be replaced with another expression
        /// from somewhere in the graph at render time.
        /// </summary>
        /// <param name="index">The index of the referred expression.</param>
        /// <param name="ticks">The number of times to differentiate.</param>
        /// <param name="x">The usual input parameter.</param>
        /// <param name="t">The time input parameter.</param>
        /// <returns>Zero. However the replacement expression will instead
        /// return the value of the selected expression at specified x and t.</returns>
        public static double Xref(int index, int ticks, double x, double t) => 0;

        #endregion
    }
}
