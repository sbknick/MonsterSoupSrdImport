using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterSoupSrdImport;
using System.Collections.Generic;
using System.Linq;

namespace MonsterSoupSrdImportTest
{
    using ExtractedArgs = Dictionary<string, string>;
    using TransformedArgs = Dictionary<string, Arg>;

    [TestClass]
    public class ArgExtractorTests
    {
        /// TODO: Remove tests for 
        /// extractor.GetArgsFromTemplate
        /// and
        /// extractor.TransformComplexMonsterTraits
        /// and turn those Private. We shouldn't be unit-testing the internals of the class.

        [DataTestMethod, DynamicData(nameof(TemplateArgs_TestCases), DynamicDataSourceType.Method)]
        public void Should_ExtractArgsFromTemplates(
            string template,
            string monsterTraitString,
            Dictionary<string, string> expectedResults)
        {
            var extractor = new ArgExtractor();

            var args = extractor.GetArgsFromTemplate(template, monsterTraitString);

            Assert.AreEqual(expectedResults.Count, args.Count);

            foreach (var traitName in expectedResults.Keys)
            {
                Assert.AreEqual(expectedResults[traitName], args[traitName]);
            }
        }

        public static IEnumerable<object[]> TemplateArgs_TestCases()
        {
            // Lol, Smurfs.
            yield return new object[]
            {
                "Hi, {shortName}!",
                "Hi, smurf!",
                new Dictionary<string, string>
                {
                    { "shortName", "smurf" },
                },
            };

            // Aboleth, Mucous Cloud
            yield return new object[]
            {
                "While underwater, {shortName} is surrounded by transformative mucus. " +
                "A creature that touches {shortName} or that hits it with a melee attack while " +
                "within 5 feet of it must make a {save:SavingThrow}. On a failure, " +
                "the creature is diseased for {diceRoll:DiceRoll} hours. The diseased creature can breathe only " +
                "underwater.",
                "While underwater, the aboleth is surrounded by transformative mucus. " +
                "A creature that touches the aboleth or that hits it with a melee attack while " +
                "within 5 feet of it must make a DC 14 Constitution saving throw. On a failure, " +
                "the creature is diseased for 1d4 hours. The diseased creature can breathe only " +
                "underwater.",
                new Dictionary<string, string>
                {
                    { "shortName", "the aboleth" },
                    { "diceRoll:DiceRoll", "1d4" },
                    { "save:SavingThrow", "DC 14 Constitution saving throw" },
                },
            };

            // Heated Body, but for remorhaz, salamander, and azer
            var template =
                "A creature that touches {shortName} or hits it with a melee attack while " +
                "within 5 feet of it takes {damage:Damage:Typed}.";

            yield return new object[]
            {
                template,
                "A creature that touches the remorhaz or hits it with a melee attack while " +
                "within 5 feet of it takes 10 (3d6) fire damage.",
                new Dictionary<string, string>
                {
                    { "shortName", "the remorhaz" },
                    { "damage:Damage:Typed", "10 (3d6) fire damage" },
                },
            };
            yield return new object[]
            {
                template,
                "A creature that touches the salamander or hits it with a melee attack while " +
                "within 5 feet of it takes 7 (2d6) fire damage.",
                new Dictionary<string, string>
                {
                    { "shortName", "the salamander" },
                    { "damage:Damage:Typed", "7 (2d6) fire damage" },
                },
            };
            yield return new object[]
            {
                template,
                "A creature that touches the azer or hits it with a melee attack while " +
                "within 5 feet of it takes 5 (1d10) fire damage.",
                new Dictionary<string, string>
                {
                    { "shortName", "the azer" },
                    { "damage:Damage:Typed", "5 (1d10) fire damage" },
                },
            };

            template =
                "If {shortName} surprises a creature and hits it with an attack during the first " +
                "round of combat, the target takes an extra {damage} from the attack.";

            // Surprise Attack, Bugbear and Doppelganger
            yield return new object[]
            {
                template,
                "If the bugbear surprises a creature and hits it with an attack during the first " +
                "round of combat, the target takes an extra 7 (2d6) damage from the attack.",
                new Dictionary<string, string>
                {
                    { "shortName", "the bugbear" },
                    { "damage", "7 (2d6) damage" },
                },
            };
            yield return new object[]
            {
                template,
                "If the doppelganger surprises a creature and hits it with an attack during the first " +
                "round of combat, the target takes an extra 10 (3d6) damage from the attack.",
                new Dictionary<string, string>
                {
                    { "shortName", "the doppelganger" },
                    { "damage", "10 (3d6) damage" },
                },
            };

            // Sunlight Sensitivity, drow, kobold, specter, etc.
            template =
                "While in sunlight, {shortName} has disadvantage on attack rolls, as well as on " +
                "{skillChecks} that rely on sight.";

            yield return new object[]
            {
                template,
                "While in sunlight, the drow has disadvantage on attack rolls, as well as on " +
                "Wisdom (Perception) checks that rely on sight.",
                new Dictionary<string, string>
                {
                    { "shortName", "the drow" },
                    { "skillChecks", "Wisdom (Perception) checks" },
                },
            };

            // Angelic Weapons ... deva, etc ... also test includes ()s
            template =
                "{ShortName}’s weapon attacks are magical. When {shortName} hits with any weapon, " +
                "the weapon deals an extra {damage:Damage:Typed:NoAverage} (included in the attack).";

            yield return new object[]
            {
                template,
                "The deva’s weapon attacks are magical. When the deva hits with any weapon, " +
                "the weapon deals an extra 4d8 radiant damage (included in the attack).",
                new Dictionary<string, string>
                {
                    { "ShortName", "The deva" },
                    { "shortName", "the deva" },
                    { "damage:Damage:Typed:NoAverage", "4d8 radiant damage" },
                },
            };
        }

        [DataTestMethod, DynamicData(nameof(ComplexArgs_TestCases), DynamicDataSourceType.Method)]
        public void Should_TransformComplexArgs(
            ExtractedArgs argsFromTemplate,
            TransformedArgs expectedTransformedArgs)
        {
            var extractor = new ArgExtractor();

            var transformedArgs = extractor.TransformComplexMonsterTraits(argsFromTemplate);

            Assert.AreEqual(expectedTransformedArgs.Count, transformedArgs.Count);

            foreach (var xform in expectedTransformedArgs)
            {
                var transformedArg = transformedArgs[xform.Key];

                Assert.AreEqual(xform.Value.key, transformedArg.key);
                Assert.AreEqual(xform.Value.argType, transformedArg.argType);
                Assert.That.ElementsAreEqual(xform.Value.flags, transformedArg.flags);
                Assert.That.FieldsAreEqual(xform.Value.value, transformedArg.value);
            }
        }

        public static IEnumerable<object[]> ComplexArgs_TestCases()
        {
            // Damage Args test cases...
            yield return new object[]
            {
                new ExtractedArgs
                {
                    { "shortName", "the bugbear" },
                    { "damage:Damage", "7 (2d6) damage" },
                },
                new TransformedArgs
                {
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the bugbear" } },
                    { "damage:Damage", new Arg
                        {
                            key = "damage",
                            argType = "Damage",
                            value = new DamageArgs { diceCount = 2, dieSize = 6 },
                        } },
                },
            };
            yield return new object[]
            {
                new ExtractedArgs
                {
                    { "shortName", "the remorhaz" },
                    { "damage:Damage:Typed", "10 (3d6) fire damage" },
                },
                new TransformedArgs
                {
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the remorhaz" } },
                    { "damage:Damage:Typed", new Arg
                        {
                            key = "damage",
                            argType = "Damage",
                            flags = new[] { "Typed" },
                            value = new TypedDamageArgs { diceCount = 3, dieSize = 6, damageType = "fire" },
                        } },
                },
            };
            yield return new object[]
            {
                new ExtractedArgs
                {
                    { "ShortName", "The deva" },
                    { "extraDamage:Damage:Typed:NoAverage", "4d8 radiant damage" },
                },
                new TransformedArgs
                {
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The deva" } },
                    { "extraDamage:Damage:Typed:NoAverage", new Arg
                        {
                            key = "extraDamage",
                            argType = "Damage",
                            flags = new[] { "Typed", "NoAverage" },
                            value = new TypedDamageArgs { diceCount = 4, dieSize = 8, damageType = "radiant" },
                        } },
                },
            };

            // DiceRoll Args test case
            yield return new object[]
            {
                new ExtractedArgs
                {
                    { "diceRoll:DiceRoll", "1d4" },
                },
                new TransformedArgs
                {
                    { "diceRoll:DiceRoll", new Arg
                        {
                            key = "diceRoll",
                            argType = "DiceRoll",
                            value = new DiceRollArgs { diceCount = 1, dieSize = 4 },
                        } },
                },
            };

            // SavingThrow Args test case
            yield return new object[]
            {
                new ExtractedArgs
                {
                    { "save:SavingThrow", "DC 10 Wisdom saving throw" },
                },
                new TransformedArgs
                {
                    { "save:SavingThrow", new Arg
                        {
                            key = "save",
                            argType = "SavingThrow",
                            value = new SavingThrowArgs { DC = 10, Attribute = "Wisdom" },
                        } },
                },
            };
        }

        [DataTestMethod, DynamicData(nameof(ComplexArgs_FromTemplates_TestCases), DynamicDataSourceType.Method)]
        public void Should_ProduceComplexArgsFromTemplates(
            string template,
            string monsterTraitString,
            TransformedArgs expectedTransformedArgs)
        {
            var extractor = new ArgExtractor();

            var transformedArgs = extractor.ExtractArgs(template, monsterTraitString);

            Assert.AreEqual(expectedTransformedArgs.Count, transformedArgs.Count());

            foreach (var xform in expectedTransformedArgs)
            {
                var transformedArg = transformedArgs[xform.Key];

                Assert.AreEqual(xform.Value.key, transformedArg.key);
                Assert.AreEqual(xform.Value.argType, transformedArg.argType);
                Assert.That.ElementsAreEqual(xform.Value.flags, transformedArg.flags);
                Assert.That.ValueObjectsAreEqual(xform.Value.value, transformedArg.value);
            }
        }

        public static IEnumerable<object[]> ComplexArgs_FromTemplates_TestCases()
        {
            object[] TestTraitFromMonster<T>(string traitName)
                where T : MonsterTestData, new()
            {
                var monster = new T();
                var trait = monster.Traits[traitName];

                return new object[] { trait.TraitTemplate, trait.MonsterTraitString, trait.ExpectedArgsOutput };
            }

            object[] TestTraitFromTemplate(string traitName, string monsterTraitString, TransformedArgs expectedArgsOutput)
            {
                return new object[] { TraitTemplates.StandardTraits[traitName].Template, monsterTraitString, expectedArgsOutput };
            }

            yield return TestTraitFromMonster<Aboleth>("Mucous Cloud");
            yield return TestTraitFromMonster<Bugbear>("Surprise Attack");
            yield return TestTraitFromMonster<Bulette>("Standing Leap");
            yield return TestTraitFromMonster<Centaur>("Charge"); // Simple Charge
            yield return TestTraitFromMonster<Minotaur>("Charge"); // Complex Charge
            yield return TestTraitFromMonster<Chuul>("Sense Magic");
            yield return TestTraitFromMonster<Cloaker>("Damage Transfer");
            yield return TestTraitFromMonster<Cloaker>("False Appearance");
            yield return TestTraitFromMonster<Cloaker>("Light Sensitivity");
            yield return TestTraitFromMonster<RugOfSmothering>("Antimagic Susceptibility");
            yield return TestTraitFromMonster<RugOfSmothering>("Damage Transfer");
            yield return TestTraitFromMonster<RugOfSmothering>("False Appearance");
            yield return TestTraitFromMonster<Couatl>("Magic Weapons");
            yield return TestTraitFromMonster<Couatl>("Shielded Mind");
            yield return TestTraitFromMonster<Darkmantle>("Echolocation");
            yield return TestTraitFromMonster<Darkmantle>("False Appearance");
            yield return TestTraitFromMonster<Drider>("Fey Ancestry");
            yield return TestTraitFromMonster<Drider>("Spider Climb");
            yield return TestTraitFromMonster<Drider>("Sunlight Sensitivity");
            yield return TestTraitFromMonster<Drider>("Web Walker");
            yield return TestTraitFromMonster<Ettercap>("Spider Climb");
            yield return TestTraitFromMonster<Ettercap>("Web Sense");
            yield return TestTraitFromMonster<Ettercap>("Web Walker");
            yield return TestTraitFromMonster<Ettin>("Two Heads");
            yield return TestTraitFromMonster<Ettin>("Wakeful");
            yield return TestTraitFromMonster<Ghost>("Ethereal Sight");
            yield return TestTraitFromMonster<Ghost>("Incorporeal Movement");
            yield return TestTraitFromMonster<GibberingMouther>("Aberrant Ground");
            yield return TestTraitFromMonster<GibberingMouther>("Gibbering");
            yield return TestTraitFromMonster<Gnoll>("Rampage");
            yield return TestTraitFromMonster<DeepGnome>("Stone Camouflage");
            yield return TestTraitFromMonster<DeepGnome>("Gnome Cunning");
            yield return TestTraitFromMonster<Goblin>("Nimble Escape");
            yield return TestTraitFromMonster<Gorgon>("Trampling Charge");
            yield return TestTraitFromMonster<Triceratops>("Trampling Charge");
            yield return TestTraitFromMonster<Griffon>("Keen Sight");
            yield return TestTraitFromMonster<Grimlock>("Blind Senses");
            yield return TestTraitFromMonster<Grimlock>("Keen Hearing and Smell");
            yield return TestTraitFromMonster<Grimlock>("Stone Camouflage");
            yield return TestTraitFromMonster<HellHound>("Pack Tactics");
            yield return TestTraitFromMonster<Hobgoblin>("Martial Advantage");
            yield return TestTraitFromMonster<Vampire>("Shapechanger");
            yield return TestTraitFromMonster<Vampire>("Misty Escape - Vampire");
            yield return TestTraitFromMonster<Vampire>("Regeneration");
            yield return TestTraitFromMonster<Vampire>("Vampire Weaknesses");
            yield return TestTraitFromMonster<Troll>("Keen Smell");
            yield return TestTraitFromMonster<Troll>("Regeneration");
            yield return TestTraitFromMonster<Hydra>("Hold Breath");
            yield return TestTraitFromMonster<Hydra>("Multiple Heads");
            yield return TestTraitFromMonster<Hydra>("Reactive Heads");
            yield return TestTraitFromMonster<Hydra>("Wakeful");
            yield return TestTraitFromMonster<Homunculus>("Telepathic Bond");
            yield return TestTraitFromMonster<InvisibleStalker>("Invisibility");
            yield return TestTraitFromMonster<InvisibleStalker>("Faultless Tracker");
            yield return TestTraitFromMonster<Kraken>("Freedom of Movement");
            yield return TestTraitFromMonster<Kraken>("Siege Monster");
            yield return TestTraitFromMonster<Lich>("Rejuvenation");
            yield return TestTraitFromMonster<Lich>("Turn Resistance");
            yield return TestTraitFromMonster<MummyLord>("Rejuvenation");
            yield return TestTraitFromMonster<MummyLord>("Magic Resistance");
            yield return TestTraitFromMonster<Magmin>("Ignited Illumination");
            yield return TestTraitFromMonster<Magmin>("Death Burst");
            yield return TestTraitFromMonster<DustMephit>("Death Burst");
            yield return TestTraitFromMonster<IceMephit>("Death Burst");
            yield return TestTraitFromMonster<IceMephit>("False Appearance");
            yield return TestTraitFromMonster<SteamMephit>("Death Burst");
            yield return TestTraitFromMonster<Mimic>("Shapechanger");
            yield return TestTraitFromMonster<Mimic>("Adhesive");
            yield return TestTraitFromMonster<Mimic>("False Appearance");
            yield return TestTraitFromMonster<Mimic>("Grappler");
            yield return TestTraitFromMonster<Werewolf>("Shapechanger");
            yield return TestTraitFromMonster<Wereboar>("Shapechanger");
            yield return TestTraitFromMonster<Wereboar>("Charge");
            yield return TestTraitFromMonster<Wereboar>("Relentless");
            yield return TestTraitFromMonster<RustMonster>("Iron Scent");
            yield return TestTraitFromMonster<RustMonster>("Rust Metal");
            yield return TestTraitFromMonster<Sahuagin>("Blood Frenzy");
            yield return TestTraitFromMonster<Sahuagin>("Limited Amphibiousness");
            yield return TestTraitFromMonster<Sahuagin>("Shark Telepathy");
            yield return TestTraitFromMonster<Shadow>("Amorphous");
            yield return TestTraitFromMonster<Shadow>("Shadow Stealth");
            yield return TestTraitFromMonster<Shadow>("Sunlight Weakness");

            yield return TestTraitFromTemplate(
                "Tail Spike Regrowth",
                "The manticore has twenty-four tail spikes. Used spikes regrow when the manticore finishes a long rest.",
                new TransformedArgs
                {
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The manticore" } },
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the manticore" } },
                    { "amount:Text", new Arg { key = "amount", argType = "Text", value = "twenty-four" } },
                });

            yield return TestTraitFromTemplate(
                "Petrifying Gaze",
@"When a creature that can see the medusa’s eyes starts its turn within 30 feet of the medusa, the medusa can force it to make a DC 14 Constitution saving throw if the medusa isn’t incapacitated and can see the creature. If the saving throw fails by 5 or more, the creature is instantly petrified. Otherwise, a creature that fails the save begins to turn to stone and is restrained. The restrained creature must repeat the saving throw at the end of its next turn, becoming petrified on a failure or ending the effect on a success. The petrification lasts until the creature is freed by the *greater restoration* spell or other magic.
Unless surprised, a creature can avert its eyes to avoid the saving throw at the start of its turn. If the creature does so, it can’t see the medusa until the start of its next turn, when it can avert its eyes again. If the creature looks at the medusa in the meantime, it must immediately make the save.
If the medusa sees itself reflected on a polished surface within 30 feet of it and in an area of bright light, the medusa is, due to its curse, affected by its own gaze.",
                new TransformedArgs
                {
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the medusa" } },
                    { "save:SavingThrow", new Arg
                        {
                            key = "save",
                            argType = "SavingThrow",
                            value = new SavingThrowArgs
                            {
                                DC = 14,
                                Attribute = "Constitution"
                            }
                        }
                    },
                });

            yield return TestTraitFromTemplate(
                "Rejuvenation",
                "If it dies, the naga returns to life in 1d6 days and regains all its hit points. Only a *wish* spell can prevent this trait from functioning.",
                new TransformedArgs
                {
                    { "template:Dropdown:[Lich,MummyLord,Naga]", new Arg { key = "template", argType = "Dropdown", value = "Naga" } },
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the naga" } },
                    { "diceRoll:DiceRoll", new Arg { key = "diceRoll", argType = "DiceRoll", value = new DiceRollArgs { diceCount = 1, dieSize = 6 } } },
                });

            yield return TestTraitFromTemplate(
                "Pounce",
@"If the weretiger moves at least 15 feet straight toward a creature and then hits it with a claw attack on the same turn, that target must succeed on a DC 14 Strength saving throw or be knocked prone.
If the target is prone, the weretiger can make one bite attack against it as a bonus action.",
                new TransformedArgs
                {
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the weretiger" } },
                    { "distance:Number", new Arg { key = "distance", argType = "Number", value = 15 } },
                    { "anAttack:Attack", new Arg { key = "anAttack", argType = "Attack", value = new AttackRefArgs
                        {
                            attack = "claw",
                            article = "a",
                            saysAttack = true,
                        } } },
                    { "save:SavingThrow", new Arg { key = "save", argType = "SavingThrow", value = new SavingThrowArgs
                        {
                            DC = 14,
                            Attribute = "Strength",
                        } } },
                    { "extraAttack:Attack", new Arg
                        {
                            key = "extraAttack",
                            argType = "Attack",
                            value = new AttackRefArgs { attack = "bite", article = string.Empty, saysAttack = true }
                        } },
                });

            yield return TestTraitFromTemplate(
                "Confer Fire Resistance",
                "The nightmare can grant resistance to fire damage to anyone riding it.",
                new TransformedArgs
                {
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The nightmare" } },
                });

            yield return TestTraitFromTemplate(
                "Illumination",
                "The nightmare sheds bright light in a 10-foot radius and dim light for an additional 10 feet.",
                new TransformedArgs
                {
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The nightmare" } },
                    { "forIn:Dropdown:[for,in]", new Arg { key = "forIn", argType = "Dropdown", value = "for" } },
                    { "radius:Number", new Arg { key = "radius", argType = "Number", value = 10 } },
                });

            yield return TestTraitFromTemplate(
                "Aggressive",
                "As a bonus action, the orc can move up to its speed toward a hostile creature that it can see.",
                new TransformedArgs
                {
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the orc" } },
                });

            yield return TestTraitFromTemplate(
                "Keen Sight and Smell",
                "The owlbear has advantage on Wisdom (Perception) checks that rely on sight or smell.",
                new TransformedArgs
                {
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The owlbear" } },
                });

            yield return TestTraitFromTemplate(
                "Keen Senses",
                "The pseudodragon has advantage on Wisdom (Perception) checks that rely on sight, hearing, or smell.",
                new TransformedArgs
                {
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The pseudodragon" } },
                });

            yield return TestTraitFromTemplate(
                "Limited Telepathy",
                "The otyugh can magically transmit simple messages and images to any creature within 120 feet " +
                "of it that can understand a language. This form of telepathy doesn’t allow the receiving " +
                "creature to telepathically respond.",
                new TransformedArgs
                {
                    { "template:Dropdown:[Otyugh,Pseudodragon]", new Arg { key = "template", argType = "Dropdown", value = "Otyugh" } },
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The otyugh" } },
                    { "range:Number", new Arg { key = "range", argType = "Number", value = 120 } },
                });

            yield return TestTraitFromTemplate(
                "Limited Telepathy",
                "The pseudodragon can magically communicate simple ideas, emotions, and images telepathically " +
                "with any creature within 100 feet of it that can understand a language.",
                new TransformedArgs
                {
                    { "template:Dropdown:[Otyugh,Pseudodragon]", new Arg { key = "template", argType = "Dropdown", value = "Pseudodragon" } },
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The pseudodragon" } },
                    { "range:Number", new Arg { key = "range", argType = "Number", value = 100 } },
                });

            yield return TestTraitFromTemplate(
                "Limited Magic Immunity",
                "The rakshasa can’t be affected or detected by spells of 6th level or lower unless it wishes " +
                "to be. It has advantage on saving throws against all other spells and magical effects.",
                new TransformedArgs
                {
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The rakshasa" } },
                    { "level:Text", new Arg { key = "level", argType = "Text", value = "6th" } },
                });

            yield return TestTraitFromTemplate(
                "Heated Body",
                "A creature that touches the remorhaz or hits it with a melee attack while within 5 feet of " +
                "it takes 10 (3d6) fire damage.",
                new TransformedArgs
                {
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the remorhaz" } },
                    { "damage:Damage:Typed", new Arg
                        {
                            key = "damage",
                            argType = "Damage",
                            flags = new[] { "Typed" },
                            value = new TypedDamageArgs
                            {
                                diceCount = 3,
                                dieSize = 6,
                                damageType = "fire",
                            }
                        } },
                });

            yield return TestTraitFromTemplate(
                "Grasping Tendrils",
                "The roper can have up to six tendrils at a time. Each tendril can be attacked " +
                "(AC 20; 10 hit points; immunity to poison and psychic damage). Destroying a tendril deals " +
                "no damage to the roper, which can extrude a replacement tendril on its next turn. A tendril " +
                "can also be broken if a creature takes an action and succeeds on a DC 15 Strength check " +
                "against it.",
                new TransformedArgs
                {
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The roper" } },
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the roper" } },
                    { "count:Text", new Arg { key = "count", argType = "Text", value = "six" } },
                    { "stats:ShortStats", new Arg
                        {
                            key = "stats",
                            argType = "ShortStats",
                            value = new ShortStatsArgs
                            {
                                AC = 20,
                                HP = 10,
                                Immunities = new[] { "poison", "psychic" }
                            }
                        } },
                    { "dc:Number", new Arg { key = "dc", argType = "Number", value = 15 } },
                });

            yield return TestTraitFromTemplate(
                "Heated Weapons",
                "Any metal melee weapon the salamander wields deals an extra 3 (1d6) fire damage on a hit " +
                "(included in the attack).",
                new TransformedArgs
                {
                    { "variant:Dropdown:[Azer,Salamander]", new Arg { key = "variant", argType = "Dropdown", value = "Salamander" } },
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the salamander" } },
                    { "damage:Damage:Typed", new Arg
                        {
                            key = "damage",
                            argType = "Damage",
                            flags = new[] { "Typed" },
                            value = new TypedDamageArgs
                            {
                                diceCount = 1,
                                dieSize = 6,
                                damageType = "fire",
                            }
                        } },
                });

            yield return TestTraitFromTemplate(
                "Heated Weapons",
                "When the azer hits with a metal melee weapon, it deals an extra 3 (1d6) fire damage " +
                "(included in the attack).",
                new TransformedArgs
                {
                    { "variant:Dropdown:[Azer,Salamander]", new Arg { key = "variant", argType = "Dropdown", value = "Azer" } },
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the azer" } },
                    { "damage:Damage:Typed", new Arg
                        {
                            key = "damage",
                            argType = "Damage",
                            flags = new[] { "Typed" },
                            value = new TypedDamageArgs
                            {
                                diceCount = 1,
                                dieSize = 6,
                                damageType = "fire",
                            }
                        } },
                });

            yield return TestTraitFromTemplate(
                "Bound",
                "The shield guardian is magically bound to an amulet. As long as the guardian and its amulet " +
                "are on the same plane of existence, the amulet’s wearer can telepathically call the guardian " +
                "to travel to it, and the guardian knows the distance and direction to the amulet. If the " +
                "guardian is within 60 feet of the amulet’s wearer, half of any damage the wearer takes " +
                "(rounded up) is transferred to the guardian.",
                new TransformedArgs
                {
                    { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The shield guardian" } },
                    { "shorterName:Text", new Arg { key = "shorterName", argType = "Text", value = "the guardian" } },
                    { "range:Number", new Arg { key = "range", argType = "Number", value = 60 } },
                });

            yield return TestTraitFromTemplate(
                "Reflective Carapace",
                "Any time the tarrasque is targeted by a *magic missile* spell, a line spell, or a spell that " +
                "requires a ranged attack roll, roll a d6. On a 1 to 5, the tarrasque is unaffected. On a 6, " +
                "the tarrasque is unaffected, and the effect is reflected back at the caster as though it " +
                "originated from the tarrasque, turning the caster into the target.",
                new TransformedArgs
                {
                    { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the tarrasque" } },
                });
        }
    }
}
