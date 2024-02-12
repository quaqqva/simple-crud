using System.Text.RegularExpressions;

namespace backend.Utilities.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Converts camelCase string to PascalCase
        /// </summary>
        /// <param name="str">String in camelCase</param>
        /// <returns>The same string in PascalCase</returns>
        /// <summary>

        public static string ToPascalCase(this string str)
        {
            return Regex.Replace(str, @"\b\p{Ll}", match => match.Value.ToUpper());
        }

        /// <summary>
        /// Converts PascalCase string to camelCase
        /// </summary>
        /// <param name="str">String in PascalCase</param>
        /// <returns>The same string in camelCase</returns>
        public static string ToCamelCase(this string str)
        {
            return char.ToLowerInvariant(str[0]) + str[1..];
        }
    }
}