using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterSoupSrdImport;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace MonsterSoupSrdImportTest
{
    [TestClass]
    public class TraitMDParserTests
    {
        [DataTestMethod, DynamicData(nameof(TemplateArgs_TestCases), DynamicDataSourceType.Method)]
        public void Should_CallArgExtractorExtractArgs_ForEachTrait(MonsterTestData monsterTestData, Monster monster, MonsterTrait[] expectedTraits)
        {
            var argExtractor = new Mock<IArgExtractor>();

            var traitParser = new TraitMDParser(argExtractor.Object);

            traitParser.ConvertTraits(monster);

            argExtractor
                .Verify(ae =>
                    ae.ExtractArgs(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Exactly(monsterTestData.Traits.Count)
                );

            foreach (var trait in monsterTestData.Traits)
            {
                argExtractor
                    .Verify(ae =>
                        ae.ExtractArgs(trait.Value.TraitTemplate, trait.Value.MonsterTraitString),
                        Times.Once
                    );
            }
        }

        [DataTestMethod, DynamicData(nameof(TemplateArgs_TestCases), DynamicDataSourceType.Method)]
        public void Should_ParseTraitsProperly(MonsterTestData monsterTestData, Monster monster, MonsterTrait[] expectedTraits)
        {
            var traits = new TraitMDParser(new ArgExtractor()).ConvertTraits(monster);

            var matches = from t in traits
                          join et in expectedTraits on t.Name equals et.Name
                          select new { t, et };

            Assert.AreEqual(expectedTraits.Length, traits.Length);
            Assert.AreEqual(expectedTraits.Length, matches.Count());

            foreach (var match in matches)
            {
                var expectedTrait = match.et;
                var trait = match.t;

                var replaceMatches = from et in expectedTrait.Replaces
                                     join t in trait.Replaces on et.Key equals t.Key
                                     select new { et = et.Value, t = t.Value };

                Assert.AreEqual(expectedTrait.Replaces.Count, trait.Replaces.Count);
                Assert.AreEqual(expectedTrait.Replaces.Count, replaceMatches.Count());

                foreach (var replMatch in replaceMatches)
                {
                    var expectedReplace = replMatch.et;
                    var replace = replMatch.t;

                    Assert.AreEqual(expectedReplace.key, replace.key);
                    Assert.AreEqual(expectedReplace.argType, replace.argType);
                    Assert.That.ElementsAreEqual(expectedReplace.flags, replace.flags);
                    Assert.That.FieldsAreEqual(expectedReplace.value, replace.value);
                }
            }
        }
        
        public static IEnumerable<object[]> TemplateArgs_TestCases()
        {
            // Aboleth
            var aboleth = new Aboleth();
            yield return new object[]
            {
                aboleth,
                new Monster
                {
                    Name = "Aboleth",
                    WhatsLeft = @"
                        ***Amphibious.*** The aboleth can breathe air and water.
                        ***Mucous Cloud.*** While underwater, the aboleth is surrounded by transformative mucus. A creature that touches the aboleth or that hits it with a melee attack while within 5 feet of it must make a DC 14 Constitution saving throw. On a failure, the creature is diseased for 1d4 hours. The diseased creature can breathe only underwater.
                        ***Probing Telepathy.*** If a creature communicates telepathically with the aboleth, the aboleth learns the creature’s greatest desires if the aboleth can see the creature.",
                },
                new MonsterTrait[]
                {
                    new MonsterTrait
                    {
                        Name = "Amphibious",
                        Replaces = aboleth.Traits["Amphibious"].ExpectedArgsOutput,
                    },
                    new MonsterTrait
                    {
                        Name = "Mucous Cloud",
                        Replaces = aboleth.Traits["Mucous Cloud"].ExpectedArgsOutput,
                    },
                    new MonsterTrait
                    {
                        Name = "Probing Telepathy",
                        Replaces = aboleth.Traits["Probing Telepathy"].ExpectedArgsOutput,
                    },
                },
            };

            // Bugbear
            var bugbear = new Bugbear();
            yield return new object[]
            {
                bugbear,
                new Monster
                {
                    Name = "Bugbear",
                    WhatsLeft = @"
                        ***Brute.*** A melee weapon deals one extra die of its damage when the bugbear hits with it (included in the attack).
                        ***Surprise Attack.*** If the bugbear surprises a creature and hits it with an attack during the first round of combat, the target takes an extra 7 (2d6) damage from the attack.",
                },
                new MonsterTrait[]
                {
                    new MonsterTrait
                    {
                        Name = "Brute",
                        Replaces = bugbear.Traits["Brute"].ExpectedArgsOutput,
                    },
                    new MonsterTrait
                    {
                        Name = "Surprise Attack",
                        Replaces = bugbear.Traits["Surprise Attack"].ExpectedArgsOutput,
                    },
                }
            };

            // Bulette
            var bulette = new Bulette();
            yield return new object[]
            {
                bulette,
                new Monster
                {
                    Name = "Bulette",
                    WhatsLeft = @"
                        ***Standing Leap.*** The bulette’s long jump is up to 30 feet and its high jump is up to 15 feet, with or without a running start.",
                },
                new MonsterTrait[]
                {
                    new MonsterTrait
                    {
                        Name = "Standing Leap",
                        Replaces = bulette.Traits["Standing Leap"].ExpectedArgsOutput,
                    },
                }
            };

            // Centaur
            var centaur = new Centaur();
            yield return new object[]
            {
                centaur,
                new Monster
                {
                    Name = "Centaur",
                    WhatsLeft = @"
                        ***Charge.*** If the centaur moves at least 30 feet straight toward a target and then hits it with a pike attack on the same turn, the target takes an extra 10 (3d6) piercing damage.",
                },
                new MonsterTrait[]
                {
                    new MonsterTrait
                    {
                        Name = "Charge",
                        Replaces = centaur.Traits["Charge"].ExpectedArgsOutput,
                    },
                }
            };

            //// Minotaur
            //var minotaur = new Minotaur();
            //yield return new object[]
            //{
            //    minotaur,
            //    new Monster
            //    {
            //        Name = "Minotaur",
            //        WhatsLeft = @"
            //            ***Charge.*** If the minotaur moves at least 10 feet straight toward a target and then hits it with a gore attack on the same turn, the target takes an extra 9 (2d8) piercing damage. If the target is a creature, it must succeed on a DC 14 Strength saving throw or be pushed up to 10 feet away and knocked prone.
            //            ***Labyrinthine Recall.*** The minotaur can perfectly recall any path it has traveled.
            //            ***Reckless.*** At the start of its turn, the minotaur can gain advantage on all melee weapon attack rolls it makes during that turn, but attack rolls against it have advantage until the start of its next turn.",
            //    },
            //    new MonsterTrait[]
            //    {
            //        new MonsterTrait
            //        {
            //            Name = "Charge",
            //            Replaces = minotaur.Traits["Charge"].ExpectedArgsOutput,
            //        },
            //    }
            //};
        }
    }
}
