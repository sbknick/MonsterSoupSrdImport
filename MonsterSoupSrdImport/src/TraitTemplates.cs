using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterSoupSrdImport
{
    public class TraitTemplates
    {
        public static readonly Dictionary<string, TraitTemplate> StandardTemplates = new TraitTemplate[]
        {
            new TraitTemplate
            {
                Name = "Half-Dragon",
                Options = new[] { "Black", "Blue", "Bronze", "Brass", "Copper", "Gold", "Green", "Red", "Silver", "White" },
                Traits = new[]
                {

                }
            },
            new TraitTemplate
            {
                Name = "Shadow",
                Traits = new[]
                {
                    "Amorphous",
                    "Shadow Stealth",
                    "Sunlight Weakness"
                },
            },
        }.OrderBy(kvp => kvp.Name).ToDictionary(kvp => kvp.Name);
    }
}
