﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public static class StringExtensions
    {
        private static readonly Regex NewlineRegex = new Regex(@"([\r\n]+)|(\\r\\n)|(\\n)|(\\r)");
        
        public static string NormalizeNewlines(this string input)
            => NewlineRegex.Replace(input, "\n");

        public static string Escape(this string input, params char[] toEscape)
        {
            foreach (var c in toEscape.Select(c => Convert.ToString(c)))
                input = input.Replace(c, Regex.Escape(c));

            return input;
        }

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