// Note: This code has come from an external library.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Components
{
    public static class StringExtensions
    {
        public static string FormatInvariant(this string s, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, s, args);
        }

        public static string CoalesceString(this string original)
        {
            return original ?? string.Empty;
        }

        public static bool EqualsInsensitively(this string original, string otherString)
        {
            return string.Equals(original, otherString, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool StartsWithInsensitively(this string original, string otherString)
        {
            return original.CoalesceString().StartsWith(otherString.CoalesceString(), StringComparison.InvariantCultureIgnoreCase);
        }

        public static string[] SplitRemoveEmpty(this string source, params char[] separator)
        {
            return source.CoalesceString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ConcatStrings(this IEnumerable<string> source, string separator = null)
        {
            return string.Join(separator ?? string.Empty, source);
        }

        public static bool IsDecimal(this string source, NumberStyles allowedNumberStyle)
        {
            decimal result;
            return decimal.TryParse(source, allowedNumberStyle, null, out result);
        }

        public static bool IsDateTime(this string source, string format, DateTimeStyles styles)
        {
            DateTime result;
            return DateTime.TryParseExact(source, format, null, styles, out result);
        }

        public static string ToNormalizedLocation(this string location)
        {
            if (location != null)
            {
                return new string(location.Where((char c) => char.IsLetterOrDigit(c)).ToArray());
            }

            return null;
        }

        public static bool EqualsAsLocation(this string location1, string location2)
        {
            return location1.ToNormalizedLocation().EqualsInsensitively(location2.ToNormalizedLocation());
        }
    }
}