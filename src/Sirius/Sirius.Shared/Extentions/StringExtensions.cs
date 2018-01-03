using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Sirius.Shared.Extentions
{
    public static class StringExtensions
    {
        [NotNull]
        public static string ToQuoted([CanBeNull] this string s, [NotNull] string nullValue = "null")
        {
            return s == null ? nullValue : $"\"{s}\"";
        }

        [NotNull]
        public static string ToQuoted([CanBeNull] this IEnumerable<string> ss, [NotNull] string nullValue = "null")
        {
            return ss == null
                ? nullValue
                : string.Join(", ", ss.Select(p => p.ToQuoted(nullValue)));
        }

        [NotNull]
        public static string ToQuoted([CanBeNull] this object o, [NotNull] string nullValue = "null")
        {
            return o == null ? nullValue : $"\"{o}\"";
        }

        public static bool IsNullOrEmpty([CanBeNull] this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace([CanBeNull] this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
    }
}
