using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public class TraitMDParser
    {
        private readonly IArgExtractor argExtractor;

        private static readonly Regex TraitsRegex = new Regex(@"\*{3}([\s\S]+?)\.\*{3} ([\s\S]+?)(?=$|\*{3})");
        private static readonly Func<string, Regex> IndividualTraitRegex = (traitName) => new Regex($@"\*{{3}}{traitName}\.\*{{3}} ([\s\S]+?)(?=$|\*{{3}})");

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
            // from Aboleth
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
            },

            // from Bugbear
            {
                "Brute",
                new Trait
                {
                    Name = "Brute",
                    Template =
                    "A melee weapon deals one extra die of its damage when {shortName} hits with it " +
                    "(included in the attack)."
                }
            },
            {
                "Surprise Attack",
                new Trait
                {
                    Name = "Surprise Attack",
                    Template =
                    "If {shortName} surprises a creature and hits it with an attack during the " +
                    "first round of combat, the target takes an extra {damage:Damage} from the attack."
                }
            },

            // from Bulette
            {
                "Standing Leap",
                new Trait
                {
                    Name = "Standing Leap",
                    Template =
                    "{ShortName}’s long jump is up to {longJump:Number} feet and its high jump is up " +
                    "to {highJump:Number} feet, with or without a running start."
                }
            },

            // from Minotaur
            {
                "Charge",
                new Trait
                {
                    Name = "Charge",
                    Template =
                    "If {shortName} moves at least {distance:Number} feet straight toward a target and " +
                    "then hits it with a {attack:Attack} attack on the same turn, the target takes " +
                    "an extra {damage:Damage:Typed}.{hasSavingThrow:YesNo}[hasSavingThrow=Yes If the target is a creature, it must succeed on " +
                    "a {save:SavingThrow} or be {affected:MultiOption}.]"
                }
            },
            {
                "Labyrinthine Recall",
                new Trait
                {
                    Name = "Labyrinthine Recall",
                    Template =
                    "{ShortName} can perfectly recall any path it has traveled."
                }
            },
            {
                "Reckless",
                new Trait
                {
                    Name = "Reckless",
                    Template =
                    "At the start of its turn, {shortName} can gain advantage on all " +
                    "melee weapon attack rolls it makes during that turn, but attack rolls against it " +
                    "have advantage until the start of its next turn."
                }
            },

            // from Chuul
            {
                "Sense Magic",
                new Trait
                {
                    Name = "Sense Magic",
                    Template =
                    "{ShortName} senses magic within {distance:Number} feet of it at will. This trait otherwise works like the " +
                    "*detect magic* spell but isn’t itself magical."
                }
            },

            // from Cloaker
            {
                "Damage Transfer - Cloaker",
                new Trait
                {
                    Name = "Damage Transfer - Cloaker",
                    Template =
                    "While attached to a creature, {shortName} takes only half the damage dealt to it " +
                    "(rounded down), and that creature takes the other half."
                }
            },
            {
                "False Appearance",
                new Trait
                {
                    Name = "False Appearance",
                    Template =
                    "While {shortName} remains motionless{more:YesNo}[more=Yes {moreRequirements:Text}], " +
                    "it is indistinguishable from {description:Text}."
                }
            },
            {
                "Light Sensitivity",
                new Trait
                {
                    Name = "Light Sensitivity",
                    Template =
                    "While in bright light, {shortName} has disadvantage on attack rolls " +
                    "and Wisdom (Perception) checks that rely on sight."
                }
            },

            // from Rug of Smothering
            {
                "Antimagic Susceptibility",
                new Trait
                {
                    Name = "Antimagic Susceptibility",
                    Template =
                    "{ShortName} is incapacitated while in the area of an *antimagic field.* " +
                    "If targeted by *dispel magic*, {shortName} must succeed on a Constitution saving throw " +
                    "against the caster’s spell save DC or fall unconscious for 1 minute."
                }
            },
            {
                "Damage Transfer - Rug of Smothering",
                new Trait
                {
                    Name = "Damage Transfer - Rug of Smothering",
                    Template =
                    "While it is grappling a creature, {shortName} takes only half the damage dealt to it, " +
                    "and the creature grappled by {shortName} takes the other half."
                }
            },

            // from Couatl
            {
                "Magic Weapons",
                new Trait
                {
                    Name = "Magic Weapons",
                    Template =
                    "{ShortName}’s weapon attacks are magical."
                }
            },
            {
                "Shielded Mind",
                new Trait
                {
                    Name = "Shielded Mind",
                    Template =
                    "{ShortName} is immune to scrying and to any effect that would sense its emotions, " +
                    "read its thoughts, or detect its location."
                }
            },

            // from Darkmantle
            {
                "Echolocation",
                new Trait
                {
                    Name = "Echolocation",
                    Template =
                    "{ShortName} can’t use its blindsight while deafened."
                }
            },
        };
    }
}
