using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public class TraitMDParser
    {
        private readonly IArgExtractor argExtractor;

        private static readonly Regex TraitsRegex = new Regex(@"\*{3}([\s\S]+?)\.\*{3} ([\s\S]+?)(?=\*|$)");
        private static readonly Func<string, Regex> IndividualTraitRegex = (traitName) => new Regex($@"\*{{3}}{traitName}\.\*{{3}} ([\s\S]+?)(?=$|\*)");

        public TraitMDParser(IArgExtractor argExtractor)
        {
            this.argExtractor = argExtractor;
        }
        
        public MonsterTrait[] ConvertTraits(Monster monster)
        {
            // for dev bookkeeping only
            HashSet<string> processedTraits = new HashSet<string>();

            if (!string.IsNullOrWhiteSpace(monster.WhatsLeft))
            {
                var monsterTraits = new List<MonsterTrait>();
                var matches = TraitsRegex.Matches(monster.WhatsLeft);

                foreach (Match match in matches)
                {
                    string traitName = match.Groups[1].Value.Trim();
                    string monsterTraitString = match.Groups[2].Value.Trim();

                    var allowedTraits = StandardTraits.Select(t => t.Key).ToList();

                    if (allowedTraits.Contains(traitName))
                    {
                        var traitTemplate = StandardTraits[traitName];
                        var monsterTrait = new MonsterTrait { Name = traitName };

                        monsterTrait.Replaces = argExtractor.ExtractArgs(traitTemplate.Template, monsterTraitString);
                        
                        monsterTraits.Add(monsterTrait);
                    }


                    // Dev Bookkeeping //

                    // only process traits we've already defined in data
                    if (!processedTraits.Contains(traitName) && StandardTraits.ContainsKey(traitName))
                        processedTraits.Add(traitName);

                    // only remove from WhatsLeft the traits that we've processed.
                    if (processedTraits.Contains(traitName))
                        monster.WhatsLeft = IndividualTraitRegex(traitName).Strip(monster.WhatsLeft);

                    // /Dev Bookkeeping //
                    //monster.WhatsLeft = null;
                }
                
                return monsterTraits.OrderBy(t => t.Name).ToArray();
            }
            
            return new MonsterTrait[0];
        }

        public static readonly Dictionary<string, Trait> StandardTraits = new Dictionary<string, Trait>
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
                    "within 5 feet of it must make a {save:SavingThrow}. On a failure, " +
                    "the creature is diseased for {diceRoll:DiceRoll} hours. The diseased creature can breathe only " +
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
