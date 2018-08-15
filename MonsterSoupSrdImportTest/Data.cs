using MonsterSoupSrdImport;
using System.Collections.Generic;

namespace MonsterSoupSrdImportTest
{
    /// <summary>
    /// Test Monsters
    /// </summary>
    public abstract class MonsterTestData
    {
        public abstract Dictionary<string, TraitTestData> Traits { get; }
    }


    public sealed class Aboleth : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Amphibious", new Aboleth_Amphibious() },
            { "Mucous Cloud", new Aboleth_MucousCloud() },
            { "Probing Telepathy", new Aboleth_ProbingTelepathy() },
        };
    }

    public sealed class Bugbear : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Brute", new Bugbear_Brute() },
            { "Surprise Attack", new Bugbear_SurpriseAttack() },
        };
    }

    public sealed class Bulette : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Standing Leap", new Bulette_StandingLeap() },
        };
    }

    public sealed class Centaur : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Charge", new Centaur_Charge() },
        };
    }

    public sealed class Minotaur : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Charge", new Minotaur_Charge() },
            { "Labyrinthine Recall", new Minotaur_LabyrinthineRecall() },
            { "Reckless", new Minotaur_Reckless() },
        };
    }






    public abstract class TraitTestData
    {
        public abstract string TraitTemplate { get; }
        public abstract string MonsterTraitString { get; }
        public abstract Dictionary<string, Arg> ExpectedArgsOutput { get; }
    }

    /// <summary>
    /// Concrete Test Monster Trait Data ///
    /// </summary>

    #region Aboleth
    
    public sealed class Aboleth_Amphibious : TraitTestData
    {
        public override string TraitTemplate =>
            TraitMDParser.StandardTraits["Amphibious"].Template;

        public override string MonsterTraitString =>
            "The aboleth can breathe air and water.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The aboleth" } },
        };
    }
    
    public sealed class Aboleth_MucousCloud : TraitTestData
    {
        public override string TraitTemplate =>
            TraitMDParser.StandardTraits["Mucous Cloud"].Template;

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

    public sealed class Aboleth_ProbingTelepathy : TraitTestData
    {
        public override string TraitTemplate =>
            TraitMDParser.StandardTraits["Probing Telepathy"].Template;

        public override string MonsterTraitString =>
            "If a creature communicates telepathically with the aboleth, the aboleth learns " +
            "the creature’s greatest desires if the aboleth can see the creature.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent",  value = "the aboleth" } },
        };
    }

    #endregion Aboleth

    #region Bugbear
    
    public sealed class Bugbear_Brute : TraitTestData
    {
        public override string TraitTemplate => 
            TraitMDParser.StandardTraits["Brute"].Template;

        public override string MonsterTraitString =>
            "A melee weapon deals one extra die of its damage when the bugbear hits " +
            "with it (included in the attack).";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the bugbear" } },
        };
    }

    public sealed class Bugbear_SurpriseAttack : TraitTestData
    {
        public override string TraitTemplate =>
            TraitMDParser.StandardTraits["Surprise Attack"].Template;

        public override string MonsterTraitString =>
            "If the bugbear surprises a creature and hits it with an attack during the first round " +
            "of combat, the target takes an extra 7 (2d6) damage from the attack.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the bugbear" } },
            { "damage:Damage", new Arg
                {
                    key = "damage",
                    argType = "Damage",
                    value = new DamageArgs { diceCount = 2, dieSize = 6 },
                } },
        };
    }

    #endregion Bugbear

    #region Bulette

    public sealed class Bulette_StandingLeap : TraitTestData
    {
        public override string TraitTemplate =>
            TraitMDParser.StandardTraits["Standing Leap"].Template;

        public override string MonsterTraitString =>
            "The bulette’s long jump is up to 30 feet and its high jump is up to 15 feet, " +
            "with or without a running start.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The bulette" } },
            { "longJump:Number", new Arg { key = "longJump", argType = "Number", value = 30 } },
            { "highJump:Number", new Arg { key = "highJump", argType = "Number", value = 15 } },
        }; 
    }

    #endregion Bulette

    #region Centaur

    public sealed class Centaur_Charge : TraitTestData
    {
        public override string TraitTemplate =>
            TraitMDParser.StandardTraits["Charge"].Template;
        
        public override string MonsterTraitString =>
            "If the centaur moves at least 30 feet straight toward a target and then hits it " +
            "with a pike attack on the same turn, the target takes an extra 10 (3d6) piercing damage.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the centaur" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 30 } },
            { "attack:Attack", new Arg { key = "attack", argType = "Attack", value = "pike" } },
            { "damage:Damage:Typed", new Arg
                {
                    key = "damage",
                    argType = "Damage",
                    flags = new[] { "Typed" },
                    value = new TypedDamageArgs
                    {
                        diceCount = 3,
                        dieSize = 6,
                        damageType = "piercing",
                    }
                } },
            { "hasSavingThrow:YesNo", new Arg { key = "hasSavingThrow", argType = "YesNo", value = "No" } },
        };
    }

    #endregion Centaur

    #region Minotaur

    public sealed class Minotaur_Charge : TraitTestData
    {
        public override string TraitTemplate =>
            TraitMDParser.StandardTraits["Charge"].Template;

        public override string MonsterTraitString =>
            "If the minotaur moves at least 10 feet straight toward a target and then hits it with " +
            "a gore attack on the same turn, the target takes an extra 9 (2d8) piercing damage. If " +
            "the target is a creature, it must succeed on a DC 14 Strength saving throw or be pushed " +
            "up to 10 feet away and knocked prone.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the minotaur" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 10 } },
            { "attack:Attack", new Arg { key = "attack", argType = "Attack", value = "gore" } },
            { "damage:Damage:Typed", new Arg
                {
                    key = "damage",
                    argType = "Damage",
                    flags = new[] { "Typed" },
                    value = new TypedDamageArgs
                    {
                        diceCount = 2,
                        dieSize = 8,
                        damageType = "piercing",
                    }
                } },
            { "hasSavingThrow:YesNo", new Arg { key = "hasSavingThrow", argType = "YesNo", value = "Yes" } },
            { "save:SavingThrow", new Arg
                {
                    key = "save",
                    argType = "SavingThrow",
                    value = new SavingThrowArgs { DC = 14, Attribute = "Strength" }
                } },
            { "affected:MultiOption", new Arg
                {
                    key = "affected",
                    argType = "MultiOption",
                    value = new[]
                    {
                        "pushed up to 10 feet away",
                        "knocked prone",
                    }
                } },
        };
    }

    public sealed class Minotaur_LabyrinthineRecall : TraitTestData
    {
        public override string TraitTemplate =>
            TraitMDParser.StandardTraits["Labyrinthine Recall"].Template;

        public override string MonsterTraitString =>
            "The minotaur can perfectly recall any path it has traveled.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The minotaur" } },
        };
    }

    public sealed class Minotaur_Reckless : TraitTestData
    {
        public override string TraitTemplate =>
            TraitMDParser.StandardTraits["Reckless"].Template;

        public override string MonsterTraitString =>
            "At the start of its turn, the minotaur can gain advantage on all melee weapon attack rolls " +
            "it makes during that turn, but attack rolls against it have advantage until the start of its next turn.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the minotaur" } },
        };
    }

    #endregion Minotaur
}
