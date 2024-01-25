namespace backend.Utilities.Extensions
{
    public static class StringExtension
    {
        public static string ToCapitalized(this string str)
        {
            return char.ToUpper(str[0]) + str[1..].ToLower();
        }
    }
}