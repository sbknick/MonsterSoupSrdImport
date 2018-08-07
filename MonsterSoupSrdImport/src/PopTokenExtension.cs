using System;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public static class PopTokenExtension
    {
        public static string PopToken(this string input, PopType popType, out string output)
        {
            var chars = new[] {
                popType == PopType.Space ? ' ' :
                popType == PopType.Comma ? ',' :
                popType == PopType.Newline ? '\n' :
                '`'
            };

            var tokens = input.Split(chars, 2);

            output = tokens[1].TrimStart();

            return tokens[0].Trim();
        }

        public static string Strip(this Regex regex, string input) => regex.Replace(input, string.Empty).Trim();

        public static int ToInt(this string input) => Convert.ToInt32(input);
    }

    public enum PopType
    {
        Space,
        Comma,
        Newline
    }
}
