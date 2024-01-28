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
    }
}