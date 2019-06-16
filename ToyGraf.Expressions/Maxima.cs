namespace ToyGraf.Expressions
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public static class Maxima
    {
        #region Public Interface

        public static void DebugOff() => Debugging = false;
        public static void DebugOn() => Debugging = true;

        public static string Differentiate(this string s, string wrt = "x") => s.diff(wrt).Simplify();

        public static string Evaluate(this string s)
        {
            WriteLine($"grind({s.ToLower()});");
            var stringBuilder = new StringBuilder();
            do stringBuilder.Append(ReadLine().TrimStart());
            while (!stringBuilder.ToString().EndsWith("$"));
            ReadLine();
            var result = stringBuilder.ToString();
            if (result.EndsWith("$"))
                return result.TrimEnd('$');
            Process = NewProcess();
            throw new Exception(string.Format("Unexpected result: {0}", stringBuilder));
        }

        public static string Integrate(this string s, string wrt = "x") => s.integrate(wrt).Simplify();
        public static string Integrate(this string s, double from, double to, string wrt = "x") => s.integrate(wrt, from, to).Simplify();
        public static string Simplify(this string s) => s.simplify().Evaluate();

        public static string SimplificationFormat { get; set; } = "trigreduce(trigsimp(fullratsimp(factor({0}))))";

        #endregion

        #region Private Properties

        private const string
            PrefixName = "maxima_prefix",
            ProcessArgs = "-eval \"(cl-user::run)\" -f -- -very-quiet",
            Version = "5.30.0";

        private static readonly string
            PrefixValue = $@"..\..\Maxima-{Version}",
            ProcessPath = $@"Maxima-{Version}\lib\maxima\{Version}\binary-gcl\maxima.exe",
            WorkingDir = $@"Maxima-{Version}\bin\";

        private static bool Debugging;
        private static Process Process = NewProcess();

        #endregion

        #region Private Methods

        private static Process NewProcess()
        {
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
            return Process.Start(processStartInfo);
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
        /// Apply the current SimplificationFormat string to an expression.
        /// </summary>
        /// <param name="s">The input expression.</param>
        /// <returns>A formula which will cause the Maxima process to apply the given simplification
        /// functions in order to the result of evaluating the input expression.</returns>
        private static string simplify(this string s) => string.Format(SimplificationFormat, s);
            //s.trigsimp().trigreduce().fullratsimp().factor();

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
