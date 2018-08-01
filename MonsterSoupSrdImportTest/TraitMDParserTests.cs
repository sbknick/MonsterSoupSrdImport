using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterSoupSrdImport;
using System.Collections.Generic;
using static MonsterSoupSrdImport.ArgExtractor;

namespace MonsterSoupSrdImportTest
{
    using ExtractedArgs = Dictionary<string, string>;
    using TransformedArgs = Dictionary<string, Arg>;

    [TestClass]
    public class TraitMDParserTests
    {
        [DataTestMethod, DynamicData(nameof(TemplateArgs_TestCases), DynamicDataSourceType.Method)]
        public void Should_ExtractArgsFromTemplates(string template, string monsterTraitString, Dictionary<string, string> expectedResults)
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
                "within 5 feet of it must make a {savingThrow:SavingThrow}. On a failure, " +
                "the creature is diseased for {diceRoll} hours. The diseased creature can breathe only " +
                "underwater.",
                "While underwater, the aboleth is surrounded by transformative mucus. " +
                "A creature that touches the aboleth or that hits it with a melee attack while " +
                "within 5 feet of it must make a DC 14 Constitution saving throw. On a failure, " +
                "the creature is diseased for 1d4 hours. The diseased creature can breathe only " +
                "underwater.",
                new Dictionary<string, string>
                {
                    { "shortName", "the aboleth" },
                    { "diceRoll", "1d4" },
                    { "savingThrow:SavingThrow", "DC 14 Constitution saving throw" },
                },
            };

            // Heated Body, but for remorhaz, salamander, and azer
            var template =
                "A creature that touches {shortName} or hits it with a melee attack while " +
                "within 5 feet of it takes {damage:typed}.";

            yield return new object[]
            {
                template,
                "A creature that touches the remorhaz or hits it with a melee attack while " +
                "within 5 feet of it takes 10 (3d6) fire damage.",
                new Dictionary<string, string>
                {
                    { "shortName", "the remorhaz" },
                    { "damage:typed", "10 (3d6) fire damage" },
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
                    { "damage:typed", "7 (2d6) fire damage" },
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
                    { "damage:typed", "5 (1d10) fire damage" },
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
                "the weapon deals an extra {damage:typed:noAverage} (included in the attack).";

            yield return new object[]
            {
                template,
                "The deva’s weapon attacks are magical. When the deva hits with any weapon, " +
                "the weapon deals an extra 4d8 radiant damage (included in the attack).",
                new Dictionary<string, string>
                {
                    { "ShortName", "The deva" },
                    { "shortName", "the deva" },
                    { "damage:typed:noAverage", "4d8 radiant damage" },
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
                Assert.AreEqual(xform.Value.flags, transformedArg.flags);
                Assert.AreEqual(xform.Value.value, transformedArg.value);
            }
        }

        public static IEnumerable<object[]> ComplexArgs_TestCases()
        {
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
                    } },
                },
            };
        }
    }
}
