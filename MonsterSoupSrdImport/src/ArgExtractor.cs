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
            var conditionalStrippedTraitTemplate = GetValidTemplateSansConditionals(traitTemplate, monsterTraitString);
            
            var argsFromTemplate = GetArgsFromTemplate(conditionalStrippedTraitTemplate.Template, monsterTraitString);

            var transformedArgs = TransformComplexMonsterTraits(argsFromTemplate);

            if (conditionalStrippedTraitTemplate.Arg != null)
                transformedArgs.Add(conditionalStrippedTraitTemplate.ArgKey, conditionalStrippedTraitTemplate.Arg);

            return transformedArgs;
        }

        private ConditionalStrippedTemplate GetValidTemplateSansConditionals(string template, string monsterTraitString)
        {
            var conditionalMatches = TemplateHasConditionalRegex.Match(template);

            if (!conditionalMatches.Success)
                return new ConditionalStrippedTemplate { Template = template };

            if (conditionalMatches.Groups[3].Value != conditionalMatches.Groups[5].Value)
                throw new ArgumentException("A conditional argument in a template has a name mismatch.\r\n" + template);


            var flags = conditionalMatches.Groups[4].Value.Split(':').Select(t => t.Trim()).Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();

            var arg = new Arg
            {
                key = conditionalMatches.Groups[3].Value,
                argType = "YesNo",
                flags = flags.Length > 0 ? flags : null,
            };


            var monsterUsesConditional = TryStripConditionalFromTemplate(conditionalMatches, monsterTraitString, out string newTemplate);

            if (monsterUsesConditional)
            {
                arg.value = conditionalMatches.Groups[6].Value;
            }
            else
            {
                arg.value = conditionalMatches.Groups[6].Value == "Yes" ? "No" : "Yes";
            }

            return new ConditionalStrippedTemplate
            {
                Template = newTemplate,
                Arg = arg,
                ArgKey = conditionalMatches.Groups[2].Value,
            };
        }

        private bool TryStripConditionalFromTemplate(Match conditionalMatches, string monsterTraitString, out string newTemplate)
        {
            var template = conditionalMatches.Groups[1].Value + " " + conditionalMatches.Groups[7].Value;
            if (!string.IsNullOrWhiteSpace(conditionalMatches.Groups[8].Value))
            {
                var str = conditionalMatches.Groups[8].Value;
                template += StartsWithPunctuation.Match(str).Success ? str : " " + str;
            }

            var captureString = GetCaptureString(template);

            var usesCondition = new Regex(captureString).Match(monsterTraitString);

            if (usesCondition.Success)
            {
                newTemplate = template;
                return true;
            }
            else
            {
                newTemplate = conditionalMatches.Groups[1].Value;
                return false;
            }
        }
        
        private static readonly Regex TemplateHasConditionalRegex = new Regex(@"(.*)\{(([a-zA-Z]+?):YesNo:?(.*)?)\}\[([a-zA-Z]+)=(\S+) (.*)\](.*)");
        private static readonly Regex SimpleArgsRegex = new Regex(@"{([\s\S]+?)}");
        private static readonly Regex StartsWithPunctuation = new Regex(@"^[^\s]");

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
            var escapedTemplate = Escape(template, '.', '(', ')', '*');
            var captureString = SimpleArgsRegex.Replace(escapedTemplate, @"([\s\S]+?)");

            return captureString;
        }

        private static readonly Dictionary<string, Func<string, string[], object>> _typedArgParserLookup = new Dictionary<string, Func<string, string[], object>>
        {
            { "Attack", ArgParser.ParseTextArgValue },
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

        private struct ConditionalStrippedTemplate
        {
            public string Template;
            public Arg Arg;
            public string ArgKey;
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

        private string Escape(string template, params char[] toEscape)
        {
            foreach (var c in toEscape.Select(c => Convert.ToString(c)))
                template = template.Replace(c, Regex.Escape(c));

            return template;
        }
    }
}
