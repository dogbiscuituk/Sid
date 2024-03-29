﻿namespace ToyGraf.Expressions
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    public static class Maxima
    {
        #region Public Interface

        //static Maxima() => NewProcess();
        public static void DebugOff() => Debugging = false;
        public static void DebugOn() => Debugging = true;

        public static string Differentiate(this string s, string wrt = "x") =>
            s.diff(wrt).Shorten();

        public static string Evaluate(this string s)
        {
            WriteLine($"grind({s.ToLower()});");
            var stringBuilder = new StringBuilder();
            do
            {
                var line = ReadLine().TrimStart();
                if (line == "done")
                    break;
                stringBuilder.Append(line);
            }
            while (true);
            var result = stringBuilder.ToString();
            if (result.EndsWith("$"))
                return result.TrimEnd('$');
            NewProcess();
            throw new Exception(string.Format("Unexpected result: {0}", stringBuilder));
        }

        public static string Integrate(this string s, string wrt = "x") =>
            s.integrate(wrt).Shorten();
        public static string Integrate(this string s, double from, double to, string wrt = "x") =>
            s.integrate(wrt, from, to).Evaluate();
        public static bool IsEquivalentTo(this string s, string t) => s.equal(t).@is().True();
        public static bool True(this string s) => s.Evaluate() == "true";
        public static string Shorten(this string s) => $"{s},{simplify1(s)},{simplify2(s)}"
            .Evaluate()
            .Split('$')
            .OrderBy(p => p.Length)
            .FirstOrDefault();

        /// <summary>
        /// The Maxima function(s) applied to a formula before returning its value to the user.
        /// Examples:
        /// 
        /// [1] "{0}" - default - no additional simplification beyond Maxoima's internal processing.
        /// 
        /// [2] "factor(fullratsimp(trigsimp({0})))" - the default used by MaximaSharp.
        /// 
        /// [3] "trigreduce(trigsimp(fullratsimp(factor({0}))))"
        /// 
        /// First place for Expression Size, and 21st for Execution Time, when a complete survey of
        /// all 326 possible permutations of five functions, and none, was performed. The five were:
        /// 
        /// factor       Factors an expression, containing any number of variables or functions,
        ///              into factors irreducible over the integers.
        /// fullratsimp  Repeatedly applies "ratsimp" followed by non-rational simplification to an
        ///              expression until no further change occurs, and returns the result.
        /// radcan       Simplifies expr, which can contain logs, exponentials, and radicals, by
        ///              converting it into a form which is canonical over a large class of
        ///              expressions and a given ordering of variables.
        /// trigreduce   Combines products and powers of trigonometric and hyperbolic sin's and
        ///              cos's of x into those of multiples of x.It also tries to eliminate these
        ///              functions when they occur in denominators.
        /// trigsimp     Employs identities sin(x)^2 + cos(x)^2 = 1 and cosh(x)^2 - sinh(x)^2 = 1
        ///              to simplify expressions containing tan, sec, etc., to sin, cos, sinh, cosh.
        ///              "trigreduce", "ratsimp" and "radcan" may be able to further simplify the
        ///              result.
        /// 
        /// A further analysis of just three functions was later performed on an extended dataset,
        /// after dropping the functions "radcan" (because it can be quite slow) and "trigreduce"
        /// (which introduces multiple angle formulae). The full results of this survey, in which
        /// the MaximaSharp default came joint 1st (ignoring no-function) for Size, but 2nd last for
        /// Time, were:
        /// 
        ///       Simplification Analysis: unsorted results
        ///       *no time recorded due to startup overhead
        ///     ---------------------------------------------
        ///     Size  Time Function
        ///     ---------------------------------------------
        ///     2305    *  {0}
        ///     2488  142  factor({0})
        ///     2538  216  fullratsimp(factor({0}))
        ///     2419  833  trigsimp(fullratsimp(factor({0})))
        ///     2419  528  trigsimp(factor({0}))
        ///     2419  513  fullratsimp(trigsimp(factor({0})))
        ///     2538   76  fullratsimp({0})
        ///     2488  147  factor(fullratsimp({0}))
        ///     2419  469  trigsimp(factor(fullratsimp({0})))
        ///     2419  346  trigsimp(fullratsimp({0}))
        ///     2357  522  factor(trigsimp(fullratsimp({0})))
        ///     2419  432  trigsimp({0})
        ///     2357  433  factor(trigsimp({0}))
        ///     2419  524  fullratsimp(factor(trigsimp({0})))
        ///     2419  428  fullratsimp(trigsimp({0}))
        ///     2357  568  factor(fullratsimp(trigsimp({0})))
        ///     
        /// Surprisingly the "empty" default value of "{0}" came out top in the Size category. Since
        /// obviously it also wins on Time, by virtue of being essentially a NOP, it was promoted to
        /// ToyGraf default. Then, a number of additional integration-differentiation round-trip
        /// tests were added to the suite, and soon, the MaximaSharp default was quietly reinstated.
        /// For example, it allows "x³*cos(2x)" to round-trip back to itself, instead of an entirely
        /// unweildy "((24x²-12)*sin(2x)-2(12x²-6)*sin(2x)+2(8x³-12x)*cos(2x)+24x*cos(2x))/16". Then
        /// finally the general Simplify() function was superseded by the Shorten() function, which
        /// tries a selection of simplification strategies and selects the shortest.
        /// 
        /// </summary>
        public static string SimpFormat1 { get; set; } = "factor(fullratsimp(trigsimp({0})))";
        public static string SimpFormat2 { get; set; } = "trigreduce(trigsimp(fullratsimp(factor({0}))))";

        #endregion

        #region Private Properties

        private static bool Debugging;
        private static Process Process = NewProcess();

        #endregion

        #region Private Methods

        private static Process NewProcess()
        {
            const string
                PrefixName = "maxima_prefix",
                ProcessArgs = "-eval \"(cl-user::run)\" -f -- -very-quiet",
                Version = "5.30.0";
            string
                PrefixValue = $@"..\..\Maxima-{Version}",
                ProcessPath = $@"Maxima-{Version}\lib\maxima\{Version}\binary-gcl\maxima.exe",
                WorkingDir = $@"Maxima-{Version}\bin\";
            if (Process != null)
                Process.Dispose();
            var processStartInfo = new ProcessStartInfo(ProcessPath, ProcessArgs)
            {
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = WorkingDir
            };
            processStartInfo.EnvironmentVariables.Add(PrefixName, PrefixValue);
            Process = Process.Start(processStartInfo);
            DebugOn();
            return Process;
        }

        private static string ReadLine()
        {
            var s = Process.StandardOutput.ReadLine();
            if (Debugging) Debug.WriteLine(s);
            return s;
        }

        private static void WriteLine(string s)
        {
            if (Debugging) Debug.WriteLine(s);
            Process.StandardInput.WriteLine(s);
        }

        #endregion

        #region Private Maxima Primitives

#pragma warning disable IDE1006 // Naming Styles - for Maxima compatibility

        /// <summary>
        /// Returns the derivative or differential of expr with respect to some or all variables in
        /// expr.
        /// diff(expr, x, n) returns the n'th derivative of expr with respect to x.
        /// diff(expr, x_1, n_1, ..., x_m, n_m) returns the mixed partial derivative of expr with
        /// respect to x_1, …, x_m.
        /// It is equivalent to diff(... (diff (expr, x_m, n_m) ...), x_1, n_1).
        /// diff(expr, x) returns the first derivative of expr with respect to the variable x.
        /// diff(expr) returns the total differential of expr, that is, the sum of the derivatives
        /// of expr with respect to each its variables times the differential del of each variable.
        /// No further simplification of del is offered.
        /// The noun form of diff is required in some contexts, such as stating a differential
        /// equation. In these cases, diff may be quoted (as 'diff) to yield the noun form instead
        /// of carrying out the differentiation.
        /// When derivabbrev is true, derivatives are displayed as subscripts. Otherwise,
        /// derivatives are displayed in the Leibniz notation, dy/dx.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <param name="wrt"></param>
        /// <returns></returns>
        private static string diff(this string s, string wrt = "x") =>
            string.Format("diff({0}, {1})", s, wrt);

        /// <summary>
        /// Attempts to determine whether the two input expressions are equivalent to one another.
        /// </summary>
        /// <param name="s">The first input expression.</param>
        /// <param name="t">The second input expression.</param>
        /// <returns>true/false, acccording as the input expressions are equivalent.</returns>
        private static string equal(this string s, string t) => $"equal({s}, {t})";

        /// <summary>
        /// Factors the expression expr, containing any number of variables or functions, into
        /// factors irreducible over the integers. factor (expr, p) factors expr over the field of
        /// rationals with an element adjoined whose minimum polynomial is p.
        /// factor uses ifactors function for factoring integers.
        /// factorflag if false suppresses the factoring of integer factors of rational expressions.
        /// dontfactor may be set to a list of variables with respect to which factoring is not to
        /// occur. (It is initially empty). Factoring also will not take place with respect to any
        /// variables which are less important(using the variable ordering assumed for CRE form)
        /// than those on the dontfactor list.
        /// savefactors if true causes the factors of an expression which is a product of factors to
        /// be saved by certain functions in order to speed up later factorizations of expressions
        /// containing some of the same factors.
        /// berlefact if false then the Kronecker factoring algorithm will be used otherwise the
        /// Berlekamp algorithm, which is the default, will be used.
        /// intfaclim if true maxima will give up factorization of integers if no factor is found
        /// after trial divisions and Pollard's rho method. If set to false (this is the case when
        /// the user calls factor explicitly), complete factorization of the integer will be
        /// attempted. The user's setting of intfaclim is used for internal calls to factor. Thus,
        /// intfaclim may be reset to prevent Maxima from taking an inordinately long time factoring
        /// large integers.
        /// factor_max_degree if set to a positive integer n will prevent certain polynomials from
        /// being factored if their degree in any variable exceeds n.
        /// See also collectterms.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <returns>A formula which will cause the Maxima process to apply "factor" to the result
        /// of evaluating the input expression.</returns>
        private static string factor(this string s) => $"factor({s})";

        /// <summary>
        /// fullratsimp repeatedly applies ratsimp followed by non-rational simplification to an
        /// expression until no further change occurs, and returns the result.
        /// When non-rational expressions are involved, one call to ratsimp followed as is usual by
        /// non-rational("general") simplification may not be sufficient to return a simplified
        /// result. Sometimes, more than one such call may be necessary.fullratsimp makes this
        /// process convenient.
        /// fullratsimp (expr, x_1, ..., x_n) takes one or more arguments similar to ratsimp and rat.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <returns>A formula which will cause the Maxima process to apply "fullratsimp" to the
        /// result of evaluating the input expression.</returns>
        private static string fullratsimp(this string s) => $"fullratsimp({s})";

        /// <summary>
        /// Attempts to symbolically compute the integral of expr with respect to x.
        /// integrate (expr, x) is an indefinite integral, while integrate (expr, x, a, b) is a
        /// definite integral, with limits of integration a and b. The limits should not contain x,
        /// although integrate does not enforce this restriction. a need not be less than b. If b is
        /// equal to a, integrate returns zero.
        /// See quad_qag and related functions for numerical approximation of definite integrals.
        /// See residue for computation of residues(complex integration).
        /// See antid for an alternative means of computing indefinite integrals.
        /// The integral(an expression free of integrate) is returned if integrate succeeds.
        /// Otherwise the return value is the noun form of the integral (the quoted operator
        /// 'integrate) or an expression containing one or more noun forms. The noun form of
        /// integrate is displayed with an integral sign.
        /// In some circumstances it is useful to construct a noun form by hand, by quoting
        /// integrate with a single quote, e.g., 'integrate (expr, x). For example, the integral may
        /// depend on some parameters which are not yet computed. The noun may be applied to its
        /// arguments by ev (i, nouns) where i is the noun form of interest.
        /// integrate handles definite integrals separately from indefinite, and employs a range of
        /// heuristics to handle each case. Special cases of definite integrals include limits of
        /// integration equal to zero or infinity (inf or minf), trigonometric functions with limits
        /// of integration equal to zero and %pi or 2 %pi, rational functions, integrals related to
        /// the definitions of the beta and psi functions, and some logarithmic and trigonometric
        /// integrals. Processing rational functions may include computation of residues. If an
        /// applicable special case is not found, an attempt will be made to compute the indefinite
        /// integral and evaluate it at the limits of integration. This may include taking a limit
        /// as a limit of integration goes to infinity or negative infinity; see also ldefint.
        /// Special cases of indefinite integrals include trigonometric functions, exponential and
        /// logarithmic functions, and rational functions. integrate may also make use of a short
        /// table of elementary integrals.
        /// integrate may carry out a change of variable if the integrand has the form
        /// f(g(x)) * diff(g(x), x). integrate attempts to find a subexpression g(x) such that the
        /// derivative of g(x) divides the integrand. This search may make use of derivatives
        /// defined by the gradef function.See also changevar and antid.
        /// If none of the preceding heuristics find the indefinite integral, the Risch algorithm is
        /// executed. The flag risch may be set as an evflag, in a call to ev or on the command
        /// line, e.g., ev (integrate (expr, x), risch) or integrate(expr, x), risch. If risch is
        /// present, integrate calls the risch function without attempting heuristics first. See
        /// also risch.
        /// integrate works only with functional relations represented explicitly with the f(x)
        /// notation. integrate does not respect implicit dependencies established by the depends
        /// function.
        /// integrate may need to know some property of a parameter in the integrand. integrate will
        /// first consult the assume database, and, if the variable of interest is not there,
        /// integrate will ask the user. Depending on the question, suitable responses are yes; or
        /// no;, or pos;, zero;, or neg;.
        /// integrate is not, by default, declared to be linear. See declare and linear.
        /// integrate attempts integration by parts only in a few special cases.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <param name="wrt"></param>
        /// <returns></returns>
        private static string integrate(this string s, string wrt) => $"integrate({s}, {wrt})";
        private static string integrate(this string s, string wrt, double from, double to) =>
            $"integrate({s}, {wrt}, {from}, {to})";

        /// <summary>
        /// Attempts to determine whether its predicate is provable from the facts in the "assume"
        /// database. 
        /// </summary>
        /// <param name="s">The input predicate.</param>
        /// <returns>true/false, acccording as the input expression is provable.</returns>
        private static string @is(this string s) => $"is({s})";

        /// <summary>
        /// Simplifies expr, which can contain logs, exponentials, and radicals, by converting it
        /// into a form which is canonical over a large class of expressions and a given ordering of
        /// variables; that is, all functionally equivalent forms are mapped into a unique form. For
        /// a somewhat larger class of expressions, radcan produces a regular form. Two equivalent
        /// expressions in this class do not necessarily have the same appearance, but their
        /// difference can be simplified by radcan to zero.
        /// For some expressions radcan is quite time consuming. This is the cost of exploring
        /// certain relationships among the components of the expression for simplifications based
        /// on factoring and partial-fraction expansions of exponents.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <returns>A formula which will cause the Maxima process to apply "radcan" to the result
        /// of evaluating the input expression.</returns>
        private static string radcan(this string s) => $"radcan({s})";

        /// <summary>
        /// Apply simplification format #1 string to an expression.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <returns>A formula which will cause the Maxima process to apply the given simplification
        /// functions in order to the result of evaluating the input expression.</returns>
        private static string simplify1(this string s) => string.Format(SimpFormat1, s);

        /// <summary>
        /// Apply simplification format #2 string to an expression.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <returns>A formula which will cause the Maxima process to apply the given simplification
        /// functions in order to the result of evaluating the input expression.</returns>
        private static string simplify2(this string s) => string.Format(SimpFormat2, s);

        /// <summary>
        /// Combines products and powers of trigonometric and hyperbolic sin's and cos's of x into
        /// those of multiples of x. It also tries to eliminate these functions when they occur in
        /// denominators.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <returns>A formula which will cause the Maxima process to apply "trigreduce" to the
        /// result of evaluating the input expression.</returns>
        private static string trigreduce(this string s) => $"trigreduce({s})";

        /// <summary>
        /// Employs the identities sin(x)^2 + cos(x)^2 = 1 and cosh(x)^2 - sinh(x)^2 = 1 to simplify
        /// expressions containing tan, sec, etc., to sin, cos, sinh, cosh.
        /// trigreduce, ratsimp, and radcan may be able to further simplify the result.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <returns>A formula which will cause the Maxima process to apply "trigsimp" to the result
        /// of evaluating the input expression.</returns>
        private static string trigsimp(this string s) => $"trigsimp({s})";

#pragma warning restore IDE1006 // Naming Styles

        #endregion
    }
}
