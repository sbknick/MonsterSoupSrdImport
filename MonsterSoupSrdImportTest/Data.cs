using System.Collections.Generic;
using static MonsterSoupSrdImport.ArgExtractor;

namespace MonsterSoupSrdImportTest
{
    public sealed class Aboleth : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Mucous Cloud", new AbolethMucousCloud() },
        };
    }

    public abstract class MonsterTestData
    {
        //public abstract string Template { get; }

        public abstract Dictionary<string, TraitTestData> Traits { get; }
    }






    public abstract class TraitTestData
    {
        public abstract string TraitTemplate { get; }
        public abstract string MonsterTraitString { get; }
        public abstract Dictionary<string, Arg> ExpectedArgsOutput { get; }
    }

    public abstract class MucousCloud : TraitTestData
    {
        public override string TraitTemplate =>
            "While underwater, {shortName} is surrounded by transformative mucus. " +
            "A creature that touches {shortName} or that hits it with a melee attack while " +
            "within 5 feet of it must make a {save:SavingThrow}. On a failure, " +
            "the creature is diseased for {diceRoll:DiceRoll} hours. The diseased creature can breathe only " +
            "underwater.";


    }

    public sealed class AbolethMucousCloud : MucousCloud
    {
        public override string MonsterTraitString =>
            "While underwater, the aboleth is surrounded by transformative mucus. " +
            "A creature that touches the aboleth or that hits it with a melee attack while " +
            "within 5 feet of it must make a DC 14 Constitution saving throw. On a failure, " +
            "the creature is diseased for 1d4 hours. The diseased creature can breathe only " +
            "underwater.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the aboleth" } },
            { "diceRoll:DiceRoll", new Arg
                {
                    key = "diceRoll",
                    argType = "DiceRoll",
                    value = new DiceRollArgs { diceCount = 1, dieSize = 4 },
                } },
            { "save:SavingThrow", new Arg
                {
                    key = "save",
                    argType = "SavingThrow",
                    value = new SavingThrowArgs { DC = 14, Attribute = "Constitution" },
                } },
        };
    }
}
