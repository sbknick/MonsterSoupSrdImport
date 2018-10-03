using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public class TraitMDParser
    {
        private readonly IArgExtractor argExtractor;

        private static readonly Regex TraitsRegex = new Regex(@"\*{3}(?<traitName>[\s\S]+?)(?: \((?<traitRequires>.*?)\))?\.\*{3} (?<traitDescription>[\s\S]+?)(?=$|\*{3})");
        private static readonly Func<string, Regex> IndividualTraitRegex = (traitName) => new Regex($@"\*{{3}}{traitName}( \(.*?\))?\.\*{{3}} ([\s\S]+?)(?=$|\*{{3}})");

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
                    string traitName = match.Groups["traitName"].Value.Trim();
                    string monsterTraitString = match.Groups["traitDescription"].Value.Trim();
                    string traitRequires = match.Groups["traitRequires"].Value.Trim();
                    traitRequires = !string.IsNullOrWhiteSpace(traitRequires) ? traitRequires : null;

                    var allowedTraits = Traits.StandardTraits.Select(t => t.Key).ToList();

                    if (allowedTraits.Contains(traitName))
                    {
                        var traitTemplate = Traits.StandardTraits[traitName];
                        var monsterTrait = new MonsterTrait { Name = traitName, Requirements = traitRequires };

                        monsterTrait.Replaces = argExtractor.ExtractArgs(traitTemplate.Template, monsterTraitString);
                        
                        monsterTraits.Add(monsterTrait);
                    }


                    // Dev Bookkeeping //

                    // only process traits we've already defined in data
                    if (!processedTraits.Contains(traitName) && Traits.StandardTraits.ContainsKey(traitName))
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
    }
}
