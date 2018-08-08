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
        }
    }
}
