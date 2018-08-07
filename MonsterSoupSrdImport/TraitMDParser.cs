using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public class ArgExtractor
    {
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

        public Dictionary<string, Arg> TransformComplexMonsterTraits(Dictionary<string, string> argsLookup)
        {
            var typedArgParserLookup = new Dictionary<string, Func<string, string[], object>>
            {
                { "Damage", ArgParser.ParseDamageArgValues }
            };

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

                arg.value = typedArgParserLookup[arg.argType](argValue, arg.flags);

                return arg;
            }
        }

        private static class ArgParser
        {
            private static readonly Regex DamageStringRegex = new Regex(@"\((\d+)d(\d+)\) ?(\S*?)? damage");
            private static readonly Regex DamageStringNoAverageRegex = new Regex(@"(\d+)d(\d+) ?(\S*?)? damage");

            public static object ParseDamageArgValues(string values, string[] flags)
            {
                flags = flags ?? new string[0];

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

        public class DamageArgs
        {
            public int diceCount;
            public int dieSize;
            public int bonus;
            public bool? usePrimaryStatBonus;
        }

        public class TypedDamageArgs : DamageArgs
        {
            public string damageType;
        }
    }

    public static class TraitMDParser
    {
        public static Trait[] ConvertTraits(Monster monster)
        {
            Dictionary<string, Trait> processedTraits = new Dictionary<string, Trait>();

            if (!string.IsNullOrWhiteSpace(monster.WhatsLeft))
            {
                var monsterTraits = new List<MonsterTrait>();
                var matches = TraitsRegex.Matches(monster.WhatsLeft);

                foreach (Match match in matches)
                {
                    string traitName = match.Groups[1].Value.Trim();
                    string monsterTraitString = match.Groups[2].Value.Trim();

                    var allowedTraits = standardTraits.Select(t => t.Key).ToList();

                    if (allowedTraits.Contains(traitName))
                    {
                        var traitTemplate = standardTraits[traitName];
                        var monsterTrait = new MonsterTrait { Name = traitName };

                        var replaces = GetReplacesFromTemplate(traitTemplate.Template, monsterTraitString);
                        
                        monsterTrait.Replaces = TransformComplexTraitReplaces(replaces);

                        monsterTraits.Add(monsterTrait);
                    }

                    if (!processedTraits.ContainsKey(traitName) && standardTraits.ContainsKey(traitName))
                        processedTraits.Add(traitName, standardTraits[traitName]);

                    if (processedTraits.ContainsKey(traitName))
                        monster.WhatsLeft = IndividualTraitRegex(traitName).Strip(monster.WhatsLeft);
                }

                monster.Traits = monsterTraits.ToArray();
            }

            return processedTraits.Select(t => t.Value).OrderBy(t => t.Name).ToArray();
        }

        private static readonly Regex TraitsRegex = new Regex(@"\*{3}([\s\S]+?)\.\*{3} ([\s\S]+?)(?=\*|$)");
        private static readonly Regex ReplacesRegex = new Regex(@"{(.*?)(:.*)*}");
        private static readonly Func<string, Regex> IndividualTraitRegex = (traitName) => new Regex($@"\*{{3}}{traitName}\.\*{{3}} ([\s\S]+?)(?=$|\*)");

        private static Dictionary<string, string> GetReplacesFromTemplate(string template, string monsterTraitString)
        {
            var replaceList = new Dictionary<string, string>();

            var matches = ReplacesRegex.Matches(template);

            var captureString = new Regex("{(.*?)}").Replace(template, "(.*?)");
            var captures = new Regex(captureString).Match(monsterTraitString);
            
            for (int i = 0; i < matches.Count; i++)
            {
                var replaceKey = matches[i].Groups[1].Value;
                if (replaceList.ContainsKey(replaceKey)) continue;

                var replaceValue = captures.Groups[i + 1].Value;
                replaceList[replaceKey] = replaceValue;
            }
            
            return replaceList;
        }

        private static Dictionary<string, object> TransformComplexTraitReplaces(Dictionary<string, string> replaceDict)
        {
            return replaceDict.ToDictionary(repl => repl.Key,
                repl =>
                {
                    switch (repl.Key)
                    {
                        case "savingThrow":
                            var matches = new Regex(@"DC (\d+) (\S+) saving throw").Match(repl.Value);
                            return new { DC = matches.Groups[1].Value, Save = matches.Groups[2].Value };

                        default:
                            return (object)repl.Value;
                    }
                });
        }

        private static readonly Dictionary<string, Trait> standardTraits = new Dictionary<string, Trait>
        {
            {
                "Amphibious",
                new Trait
                {
                    Name = "Amphibious",
                    Template = "{ShortName} can breathe air and water."
                }
            },
            {
                "Mucous Cloud",
                new Trait
                {
                    Name = "Mucous Cloud",
                    Template =
                    "While underwater, {shortName} is surrounded by transformative mucus. " +
                    "A creature that touches {shortName} or that hits it with a melee attack while " +
                    "within 5 feet of it must make a {savingThrow}. On a failure, " +
                    "the creature is diseased for {diceRoll} hours. The diseased creature can breathe only " +
                    "underwater."
                }
            },
            {
                "Probing Telepathy",
                new Trait
                {
                    Name = "Probing Telepathy",
                    Template =
                    "If a creature communicates telepathically with {shortName}, {shortName} learns " +
                    "the creature’s greatest desires if {shortName} can see the creature."
                }
            }
        };
    }
}
