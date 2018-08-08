using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public class ArgExtractor
    {
        public Dictionary<string, Arg> ExtractArgs(string template, string monsterTraitString)
        {
            var argsFromTemplate = GetArgsFromTemplate(template, monsterTraitString);

            var transformedArgs = TransformComplexMonsterTraits(argsFromTemplate);

            return transformedArgs;
        }

        private static readonly Regex SimpleArgsRegex = new Regex(@"{([\s\S]+?)}");

        public Dictionary<string, string> GetArgsFromTemplate(string template, string monsterTraitString)
        {
            var argLookup = new Dictionary<string, string>();

            var matches = SimpleArgsRegex.Matches(template);

            var escapedTemplate = Escape(template, '.', '(', ')', '*');
            var captureString = SimpleArgsRegex.Replace(escapedTemplate, @"([\s\S]+?)");

            var captures = new Regex(captureString).Match(monsterTraitString);

            for (int i = 0; i < matches.Count; i++)
            {
                var argKey = matches[i].Groups[1].Value;
                if (argLookup.ContainsKey(argKey)) continue;

                var argValue = captures.Groups[i + 1].Value;
                argLookup[argKey] = argValue;
            }

            return argLookup;
        }

        private static readonly Dictionary<string, Func<string, string[], object>> _typedArgParserLookup = new Dictionary<string, Func<string, string[], object>>
        {
            { "Damage", ArgParser.ParseDamageArgValues },
            { "DiceRoll", ArgParser.ParseDiceRollArgValues },
            { "SavingThrow", ArgParser.ParseSavingThrowArgValues },
        };

        public Dictionary<string, Arg> TransformComplexMonsterTraits(Dictionary<string, string> argsLookup)
        {
            return argsLookup.ToDictionary(kvp => kvp.Key,
                kvp =>
                {
                    var argKeyTokens = kvp.Key.Split(':');
                    return argKeyTokens.Length == 1 ? InherentArg(argKeyTokens[0], kvp.Value) : TypedArg(argKeyTokens, kvp.Value);
                });

            // translators

            Arg InherentArg(string argKey, string argValue)
            {
                return new Arg
                {
                    key = argKey,
                    argType = "Inherent",
                    value = argValue,
                };
            }

            Arg TypedArg(string[] argKeyTokens, string argValue)
            {
                var arg = new Arg { key = argKeyTokens[0], argType = argKeyTokens[1] };

                if (argKeyTokens.Length > 2)
                    arg.flags = argKeyTokens.Skip(2).ToArray();

                arg.value = _typedArgParserLookup[arg.argType](argValue, arg.flags ?? new string[0]);

                return arg;
            }
        }

        private static class ArgParser
        {
            #region Damage

            private static readonly Regex DamageStringRegex = new Regex(@"\((\d+)d(\d+)\) ?(\S*?)? damage");
            private static readonly Regex DamageStringNoAverageRegex = new Regex(@"(\d+)d(\d+) ?(\S*?)? damage");

            public static object ParseDamageArgValues(string values, string[] flags)
            {
                var isTyped = flags.Contains("Typed");
                var hasNoAverage = flags.Contains("NoAverage");

                var matches = (hasNoAverage ? DamageStringNoAverageRegex : DamageStringRegex).Match(values);

                if (isTyped)
                    return new TypedDamageArgs
                    {
                        diceCount = matches.Groups[1].Value.ToInt(),
                        dieSize = matches.Groups[2].Value.ToInt(),
                        damageType = matches.Groups[3].Value,
                    };

                return new DamageArgs
                {
                    diceCount = matches.Groups[1].Value.ToInt(),
                    dieSize = matches.Groups[2].Value.ToInt(),
                };
            }

            #endregion

            #region DiceRoll

            private static readonly Regex DiceRollRegex = new Regex(@"(\d+)d(\d+)");

            public static object ParseDiceRollArgValues(string values, string[] flags)
            {
                var matches = DiceRollRegex.Match(values);

                return new DiceRollArgs
                {
                    diceCount = matches.Groups[1].Value.ToInt(),
                    dieSize = matches.Groups[2].Value.ToInt(),
                };
            }

            #endregion

            #region SavingThrow

            private static readonly Regex SavingThrowRegex = new Regex(@"DC (\d+) (\S+) saving throw");

            public static object ParseSavingThrowArgValues(string values, string[] flags)
            {
                var matches = SavingThrowRegex.Match(values);

                return new SavingThrowArgs
                {
                    DC = matches.Groups[1].Value.ToInt(),
                    Attribute = matches.Groups[2].Value,
                };
            }

            #endregion
        }

        private string Escape(string template, params char[] toEscape)
        {
            foreach (var c in toEscape.Select(c => Convert.ToString(c)))
                template = template.Replace(c, Regex.Escape(c));

            return template;
        }

        public class Arg
        {
            public string key;
            public string argType;
            public string[] flags;
            public object value;
        }

        public class DiceRollArgs
        {
            public int diceCount;
            public int dieSize;
        }

        public class DamageArgs : DiceRollArgs
        {
            public int bonus;
            public bool? usePrimaryStatBonus;
        }

        public class TypedDamageArgs : DamageArgs
        {
            public string damageType;
        }

        public class SavingThrowArgs
        {
            public int DC;
            public string Attribute;
        }
    }

}
