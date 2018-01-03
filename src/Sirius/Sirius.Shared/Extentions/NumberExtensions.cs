using System;
using System.Globalization;

namespace Sirius.Shared.Extentions
{
    /// <summary>
    ///     Набор близких к нативным расширений для чисел.
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        ///     Привести <see cref="Int32"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this int n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Привести <see cref="Nullable{Int32}"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this int? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Привести <see cref="Int64"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this long n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Привести <see cref="Nullable{Int64}"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this long? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Привести <see cref="Decimal"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this decimal n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Привести <see cref="Nullable{Decimal}"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this decimal? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Привести <see cref="Single"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this float n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Привести <see cref="Nullable{Single}"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this float? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Привести <see cref="Double"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this double n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Привести <see cref="Nullable{Double}"/> к инвариантной строке.
        /// </summary>
        public static string ToInvariant(this double? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }
    }
}
