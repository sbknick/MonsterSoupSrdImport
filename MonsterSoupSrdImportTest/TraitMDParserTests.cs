using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterSoupSrdImport;
using System.Collections.Generic;
using System.Linq;
using static MonsterSoupSrdImport.ArgExtractor;

namespace MonsterSoupSrdImportTest
{
    [TestClass]
    public class TraitMDParserTests
    {
        [DataTestMethod, DynamicData(nameof(TemplateArgs_TestCases), DynamicDataSourceType.Method)]
        public void Should_ParseTraitsProperly(Monster monster, MonsterTrait[] expectedTraits)
        {
            var traits = TraitMDParser.ConvertTraits(monster);

            var matches = from t in traits
                          join et in expectedTraits on t.Name equals et.Name
                          select new { t, et };

            Assert.AreEqual(expectedTraits.Length, traits.Length);
            Assert.AreEqual(expectedTraits.Length, matches.Count());
        }

        public static IEnumerable<object[]> TemplateArgs_TestCases()
        {
            // Aboleth
            var aboleth = new Aboleth();
            yield return new object[]
            {
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
