using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterSoupSrdImport;
using System.Collections.Generic;

namespace MonsterSoupSrdImportTest
{
    [TestClass]
    public class TraitMDParserTests
    {
        [TestMethod]
        public void Should_ExtractReplacesFromTemplates()
        {
            string template = "Hi, {shortName}!";
            string monsterTraitString = "Hi, smurf!";

            Dictionary<string, string> expectedResults = new Dictionary<string, string>
            {
                { "shortName", "smurf" }
            };

            var extractor = new ReplaceExtractor();

            var replaces = extractor.GetReplacesFromTemplate(template, monsterTraitString);

            Assert.AreEqual(expectedResults.Count, replaces.Count);
        }
    }
}
