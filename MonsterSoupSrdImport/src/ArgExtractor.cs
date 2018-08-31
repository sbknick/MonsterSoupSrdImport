using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public interface IArgExtractor
    {
        Dictionary<string, Arg> ExtractArgs(string traitTemplate, string monsterTraitString);
    }

    public class ArgExtractor : IArgExtractor
    {
        public Dictionary<string, Arg> ExtractArgs(string traitTemplate, string monsterTraitString)
        {
            traitTemplate = traitTemplate.NormalizeNewlines();
            monsterTraitString = monsterTraitString.NormalizeNewlines();

            var perm = GetTemplateConditionalPermutation(traitTemplate, monsterTraitString);
            
            var argsFromTemplate = GetArgsFromTemplate(perm.Template, monsterTraitString);

            var transformedArgs = TransformComplexMonsterTraits(argsFromTemplate);

            if (perm.Args?.Length > 0)
                foreach (var arg in perm.Args)
                    transformedArgs.Add(arg.ArgKey, arg.Arg);

            return transformedArgs;
        }

        private TemplateConditionalPermutation GetTemplateConditionalPermutation(string template, string monsterTraitString)
        {
            var perms = GetConditionalPermutations(template);

            if (perms == null)
            {
                return new TemplateConditionalPermutation
                {
                    Template = template,
                };
            }

            // TEST FOR WHICH PERMUTATION IS APPROPRIATE PARA ESTA MONSTERA //
            foreach (var perm in perms)
            {
                var captureString = GetCaptureString(perm.Template);
                var usesCondition = new Regex(captureString).Match(monsterTraitString).Success;

                if (usesCondition)
                {
                    return perm;
                }
            }

            throw new Exception();
        }

        private IList<TemplateConditionalPermutation> GetConditionalPermutations(string template)
        {
            var ConditionalsRegex = new Regex(@"\{(([^\{]+?):YesNo:?(.*?))\}\[(\S+?)=(\S+?) (.*?)\]");
            var conditionalMatches = ConditionalsRegex.Matches(template);

            if (conditionalMatches.Count == 0)
                return null;

            var permutationList = new List<(int specificity, TemplateConditionalPermutation tcp)>();

            int count = conditionalMatches.Count;
            int permutationCount = (int)Math.Pow(2, count);

            var permutation = new bool[count];

            for (int i = permutationCount - 1; i >= 0; i--)
            {
                for (int j = 0; j < count; j++)
                {
                    permutation[j] = (i & (1 << j)) != 0;
                }

                permutationList.Add((permutation.Count(p => p), BuildPermutation(permutation, conditionalMatches, template)));
            }

            return permutationList.Select(p => p.tcp).ToList();
        }

        private struct TemplateConditionalPermutation
        {
            public string Template;
            public (string ArgKey, Arg Arg)[] Args;
        }

        private TemplateConditionalPermutation BuildPermutation(bool[] permutation, MatchCollection conditionalMatches, string template)
        {
            string Replace(Match match, string insert = null) =>
                $"{template.Substring(0, match.Index)}{insert}{template.Substring(match.Index + match.Length)}";
            
            var matchArgs = new Stack<(string key, Arg arg)>();

            for (int i = permutation.Length - 1; i >= 0; i--)
            {
                var match = conditionalMatches[i];

                if (match.Groups[2].Value != match.Groups[4].Value)
                    throw new ArgumentException();

                var flags = match.Groups[3].Value.Split(':').Select(t => t.Trim()).Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();

                var arg = new Arg
                {
                    key = match.Groups[2].Value,
                    argType = "YesNo",
                    flags = flags.Length > 0 ? flags : null,
                };

                string condition() => match.Groups[5].Value;
                string notCondition() => condition() == "Yes" ? "No" : "Yes";

                if (!permutation[i])
                {
                    // Excise Match
                    template = Replace(match);
                    arg.value = notCondition();
                }
                else
                {
                    // Replace Match with appropriate template string.
                    template = Replace(match, match.Groups[6].Value);
                    arg.value = condition();
                }

                matchArgs.Push((match.Groups[1].Value, arg));
            }

            return new TemplateConditionalPermutation
            {
                Template = template,
                Args = matchArgs.ToArray(),
            };
        }


        private static readonly Regex SimpleArgsRegex = new Regex(@"{([\s\S]+?)}");

        public Dictionary<string, string> GetArgsFromTemplate(string traitTemplate, string monsterTraitString)
        {
            var argLookup = new Dictionary<string, string>();

            var matches = SimpleArgsRegex.Matches(traitTemplate);

            var captureString = GetCaptureString(traitTemplate);

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

        private string GetCaptureString(string template)
        {
            var escapedTemplate = template.Escape('.', '(', ')', '*');
            var captureString = SimpleArgsRegex.Replace(escapedTemplate, @"([\s\S]+?)");

            return $"^{captureString}$";
        }

        private static readonly Dictionary<string, Func<string, string[], object>> _typedArgParserLookup = new Dictionary<string, Func<string, string[], object>>
        {
            { "Attack", ArgParser.ParseAttackArgValues },
            { "Damage", ArgParser.ParseDamageArgValues },
            { "DiceRoll", ArgParser.ParseDiceRollArgValues },
            { "MultiOption", ArgParser.ParseMultiOptionArgValues },
            { "Number", ArgParser.ParseNumberArgValue },
            { "SavingThrow", ArgParser.ParseSavingThrowArgValues },
            { "Text", ArgParser.ParseTextArgValue },
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
            #region Attack

            private static readonly Regex AttackWithStringRegex = new Regex(@"attack with its (\S+)");
            private static readonly Regex AttackStringRegex = new Regex(@"(\S+) attack");

            public static object ParseAttackArgValues(string values, string[] flags)
            {
                var withMatch = AttackWithStringRegex.Match(values);

                if (withMatch.Success)
                    return new AttackRefArgs
                    {
                        attack = withMatch.Groups[1].Value,
                        withIts = true,
                    };

                var notWithMatch = AttackStringRegex.Match(values);

                return new AttackRefArgs
                {
                    attack = notWithMatch.Groups[1].Value,
                };
            }

            #endregion

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

            #region MultiOption

            private static readonly Regex MultiOptionRegex = new Regex(@"(.*?)(?:$| and (.*))");

            public static object ParseMultiOptionArgValues(string values, string[] flags)
            {
                var matches = MultiOptionRegex.Match(values);

                var options = new List<string> { matches.Groups[1].Value };

                if (matches.Groups.Count > 2)
                    options.Add(matches.Groups[2].Value);

                return options;
            }

            #endregion MultiOption

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

            public static object ParseTextArgValue(string values, string[] flags)
                => values;

            public static object ParseNumberArgValue(string values, string[] flags)
                => values.ToInt();
        }
    }
}
