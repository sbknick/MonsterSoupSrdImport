using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public class ReplaceExtractor
    {
        public Dictionary<string, string> GetReplacesFromTemplate(string template, string monsterTraitString)
        {
            return new Dictionary<string, string>();
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
