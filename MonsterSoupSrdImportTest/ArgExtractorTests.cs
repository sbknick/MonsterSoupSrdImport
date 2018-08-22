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

            yield return TestTraitFromMonster<Aboleth>("Mucous Cloud");
            yield return TestTraitFromMonster<Bugbear>("Surprise Attack");
            yield return TestTraitFromMonster<Bulette>("Standing Leap");
            yield return TestTraitFromMonster<Centaur>("Charge"); // Simple Charge
            yield return TestTraitFromMonster<Minotaur>("Charge"); // Complex Charge
            yield return TestTraitFromMonster<Chuul>("Sense Magic");
            yield return TestTraitFromMonster<Cloaker>("Damage Transfer - Cloaker");
            yield return TestTraitFromMonster<Cloaker>("False Appearance");
            yield return TestTraitFromMonster<Cloaker>("Light Sensitivity");
            yield return TestTraitFromMonster<RugOfSmothering>("Antimagic Susceptibility");
            yield return TestTraitFromMonster<RugOfSmothering>("Damage Transfer - Rug of Smothering");
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
        }
    }
}
