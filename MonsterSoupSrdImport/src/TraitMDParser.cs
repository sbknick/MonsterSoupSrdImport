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
                    "then hits it with a {attack:Attack} on the same turn, the target takes " +
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

            // from Drider
            {
                "Fey Ancestry",
                new Trait
                {
                    Name = "Fey Ancestry",
                    Template =
                    "{ShortName} has advantage on saving throws against being charmed, and " +
                    "magic can’t put {shortName} to sleep."
                }
            },
            {
                "Spider Climb",
                new Trait
                {
                    Name = "Spider Climb",
                    Template =
                    "{ShortName} can climb difficult surfaces, including upside down on ceilings, " +
                    "without needing to make an ability check."
                }
            },
            {
                "Sunlight Sensitivity",
                new Trait
                {
                    Name = "Sunlight Sensitivity",
                    Template =
                    "While in sunlight, {shortName} has disadvantage on attack rolls, as well as " +
                    "on Wisdom (Perception) checks that rely on sight."
                }
            },
            {
                "Web Walker",
                new Trait
                {
                    Name = "Web Walker",
                    Template =
                    "{ShortName} ignores movement restrictions caused by webbing."
                }
            },

            // from Ettercap
            {
                "Web Sense",
                new Trait
                {
                    Name = "Web Sense",
                    Template =
                    "While in contact with a web, {shortName} knows the exact location of " +
                    "any other creature in contact with the same web."
                }
            },

            // from Ettin
            {
                "Two Heads",
                new Trait
                {
                    Name = "Two Heads",
                    Template =
                    "{ShortName} has advantage on Wisdom (Perception) checks and on saving throws " +
                    "against being blinded, charmed, deafened, frightened, stunned, and knocked unconscious."
                }
            },
            {
                "Wakeful",
                new Trait
                {
                    Name = "Wakeful",
                    Template =
                    "When one of {shortName}’s heads is asleep, its other head is awake."
                }
            },

            // from Ghost
            {
                "Ethereal Sight",
                new Trait
                {
                    Name = "Ethereal Sight",
                    Template =
                    "{ShortName} can see 60 feet into the Ethereal Plane when it is " +
                    "on the Material Plane, and vice versa."
                }
            },
            {
                "Incorporeal Movement",
                new Trait
                {
                    Name = "Incorporeal Movement",
                    Template =
                    "{ShortName} can move through other creatures and objects as if they were difficult terrain. " +
                    "It takes {damage:Damage:Typed} if it ends its turn inside an object."
                }
            },

            // from Gibbering Mouther
            {
                "Aberrant Ground",
                new Trait
                {
                    Name = "Aberrant Ground",
                    Template =
                    "The ground in a {radius:Number}-foot radius around {shortName} is doughlike difficult terrain. " +
                    "Each creature that starts its turn in that area must succeed on a {save:SavingThrow} or " +
                    "have its speed reduced to 0 until the start of its next turn."
                }
            },
            {
                "Gibbering",
                new Trait
                {
                    Name = "Gibbering",
                    Template =
                    "{ShortName} babbles incoherently while it can see any creature and isn’t incapacitated. " +
                    "Each creature that starts its turn within {distance:Number} feet of {shortName} and " +
                    "can hear the gibbering must succeed on a {save:SavingThrow}. On a failure, the creature " +
                    "can’t take reactions until the start of its next turn and rolls a d8 to determine what it does " +
                    "during its turn. On a 1 to 4, the creature does nothing. On a 5 or 6, the creature takes " +
                    "no action or bonus action and uses all its movement to move in a randomly determined direction. " +
                    "On a 7 or 8, the creature makes a melee attack against a randomly determined creature within " +
                    "its reach or does nothing if it can’t make such an attack."
                }
            },

            // from Gnoll
            {
                "Rampage",
                new Trait
                {
                    Name = "Rampage",
                    Template =
                    "When {shortName} reduces a creature to 0 hit points with a melee attack on its turn, " +
                    "{shortName} can take a bonus action to move up to half its speed and make a {attack:Attack}."
                }
            },

            // from Deep Gnome
            {
                "Stone Camouflage",
                new Trait
                {
                    Name = "Stone Camouflage",
                    Template =
                    "{ShortName} has advantage on Dexterity (Stealth) checks made to hide in rocky terrain."
                }
            },
            {
                "Gnome Cunning",
                new Trait
                {
                    Name = "Gnome Cunning",
                    Template =
                    "{ShortName} has advantage on Intelligence, Wisdom, and Charisma saving throws against magic."
                }
            },

            // from Goblin
            {
                "Nimble Escape",
                new Trait
                {
                    Name = "Nimble Escape",
                    Template =
                    "{ShortName} can take the Disengage or Hide action as a bonus action on each of its turns."
                }
            },

            // from Gorgon
            {
                "Trampling Charge",
                new Trait
                {
                    Name = "Trampling Charge",
                    Template =
                    "If {shortName} moves at least {distance:Number} feet straight toward a creature and then " +
                    "hits it with a {attack:Attack} on the same turn, that target must succeed on a " +
                    "{save:SavingThrow} or be knocked prone. If the target is prone, {shortName} can make " +
                    "one {extraAttack:Attack} against it as a bonus action."
                }
            },

            // from Griffon
            {
                "Keen Sight",
                new Trait
                {
                    Name = "Keen Sight",
                    Template =
                    "{ShortName} has advantage on Wisdom (Perception) checks that rely on sight."
                }
            },

            // from Grimlock
            {
                "Blind Senses",
                new Trait
                {
                    Name = "Blind Senses",
                    Template =
                    "{ShortName} can’t use its blindsight while deafened and unable to smell."
                }
            },
            {
                "Keen Hearing and Smell",
                new Trait
                {
                    Name = "Keen Hearing and Smell",
                    Template =
                    "{ShortName} has advantage on Wisdom (Perception) checks that rely on hearing or smell."
                }
            },

            // from Hell Hound
            {
                "Pack Tactics",
                new Trait
                {
                    Name = "Pack Tactics",
                    Template =
                    "{ShortName} has advantage on an attack roll against a creature if at least one of " +
                    "{shortName}’s allies is within 5 feet of the creature and the ally isn’t incapacitated."
                }
            },
        };
    }
}
