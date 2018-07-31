using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterSoupSrdImport;
using System.Collections.Generic;

namespace MonsterSoupSrdImportTest
{
    [TestClass]
    public class TraitMDParserTests
    {

        private static readonly Dictionary<string, string>[] ExpectedResults = new[]
        {
            // idx 0
            new Dictionary<string, string>
            {
                { "shortName", "smurf" }
            },

            // idx 1
            new Dictionary<string, string>
            {
                { "shortName", "the aboleth" },
                { "diceRoll", "1d4" },
                { "savingThrow", "DC 14 Constitution saving throw" },
            }
        };

        [DataRow("Hi, {shortName}!", "Hi, smurf!", 0, DisplayName = "Simple Smurf Test")]
        [DataRow("While underwater, {shortName} is surrounded by transformative mucus. " +
                 "A creature that touches {shortName} or that hits it with a melee attack while " +
                 "within 5 feet of it must make a {savingThrow}. On a failure, " +
                 "the creature is diseased for {diceRoll} hours. The diseased creature can breathe only " +
                 "underwater.",
                 "While underwater, the aboleth is surrounded by transformative mucus. " +
                 "A creature that touches the aboleth or that hits it with a melee attack while " +
                 "within 5 feet of it must make a DC 14 Constitution saving throw. On a failure, " +
                 "the creature is diseased for 1d4 hours. The diseased creature can breathe only " +
                 "underwater.",
            1, DisplayName = "Aboleth, Mucous Cloud Test")]
        [DataTestMethod]
        public void Should_ExtractReplacesFromTemplates(string template, string monsterTraitString, int expectedResultIndex)
        {
            var expectedResults = ExpectedResults[expectedResultIndex];

            var extractor = new ReplaceExtractor();

            var replaces = extractor.GetReplacesFromTemplate(template, monsterTraitString);

            Assert.AreEqual(expectedResults.Count, replaces.Count);
            
            foreach (var traitName in expectedResults.Keys)
            {
                Assert.AreEqual(expectedResults[traitName], replaces[traitName]);
            }
        }
    }
}
