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

    // Monsters //

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

    public sealed class Chuul : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Amphibious", new Chuul_Amphibious() },
            { "Sense Magic", new Chuul_SenseMagic() },
        };
    }

    public sealed class Cloaker : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Damage Transfer - Cloaker", new Cloaker_DamageTransfer() },
            { "False Appearance", new Cloaker_FalseAppearance() },
            { "Light Sensitivity", new Cloaker_LightSensitivity() },
        };
    }

    public sealed class Couatl : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Magic Weapons", new Couatl_MagicWeapons() },
            { "Shielded Mind", new Couatl_ShieldedMind() },
        };
    }

    public sealed class Darkmantle : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Echolocation", new Darkmantle_Echolocation() },
            { "False Appearance", new Darkmantle_FalseAppearance() },
        };
    }

    public sealed class DeepGnome : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Stone Camouflage", new DeepGnome_StoneCamouflage() },
            { "Gnome Cunning", new DeepGnome_GnomeCunning() },
        };
    }

    public sealed class Drider : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Fey Ancestry", new Drider_FeyAncestry() },
            { "Spider Climb", new Drider_SpiderClimb() },
            { "Sunlight Sensitivity", new Drider_SunlightSensitivity() },
            { "Web Walker", new Drider_WebWalker() },
        };
    }

    public sealed class Ettercap : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Spider Climb", new Ettercap_SpiderClimb() },
            { "Web Sense", new Ettercap_WebSense() },
            { "Web Walker", new Ettercap_WebWalker() },
        };
    }

    public sealed class Ettin : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Two Heads", new Ettin_TwoHeads() },
            { "Wakeful - Ettin", new Ettin_Wakeful() },
        };
    }

    public sealed class Ghost : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Ethereal Sight", new Ghost_EtherealSight() },
            { "Incorporeal Movement", new Ghost_IncorporealMovement() },
        };
    }

    public sealed class GibberingMouther : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Aberrant Ground", new GibberingMouther_AberrantGround() },
            { "Gibbering", new GibberingMouther_Gibbering() },
        };
    }

    public sealed class Gnoll : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Rampage", new Gnoll_Rampage() },
        };
    }

    public sealed class Goblin : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Nimble Escape", new Goblin_NimbleEscape() },
        };
    }

    public sealed class Gorgon : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Trampling Charge", new Gorgon_TramplingCharge() },
        };
    }

    public sealed class Griffon : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Keen Sight", new Griffon_KeenSight() },
        };
    }

    public sealed class Grimlock : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Blind Senses", new Grimlock_BlindSenses() },
            { "Keen Hearing and Smell", new Grimlock_KeenHearingAndSmell() },
            { "Stone Camouflage", new Grimlock_StoneCamouflage() },
        };
    }

    public sealed class HellHound : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Keen Hearing and Smell", new HellHound_KeenHearingAndSmell() },
            { "Pack Tactics", new HellHound_PackTactics() },
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

    public sealed class Hobgoblin : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Martial Advantage", new Hobgoblin_MartialAdvantage() },
        };
    }

    public sealed class RugOfSmothering : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Antimagic Susceptibility", new RugOfSmothering_AntimagicSusceptibility() },
            { "Damage Transfer - Rug of Smothering", new RugOfSmothering_DamageTransfer() },
            { "False Appearance", new RugOfSmothering_FalseAppearance() },
        };
    }

    public sealed class Triceratops : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Trampling Charge", new Triceratops_TramplingCharge() },
        };
    }

    public sealed class Troll : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Keen Smell", new Troll_KeenSmell() },
            { "Regeneration", new Troll_Regeneration() },
        };
    }

    public sealed class Vampire : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Shapechanger - Vampire", new Vampire_Shapechanger() },
            { "Misty Escape - Vampire", new Vampire_MistyEscape() },
            { "Regeneration", new Vampire_Regeneration() },
            { "Spider Climb", new Vampire_SpiderClimb() },
            { "Vampire Weaknesses", new Vampire_VampireWeaknesses() },
        };
    }






    public abstract class TraitTestData
    {
        public string TraitTemplate => TraitTemplates.StandardTraits[Trait].Template;

        public abstract string Trait { get; }
        public abstract string MonsterTraitString { get; }
        public abstract Dictionary<string, Arg> ExpectedArgsOutput { get; }
    }

    /// <summary>
    /// Concrete Test Monster Trait Data ///
    /// </summary>

    #region Aboleth

    public sealed class Aboleth_Amphibious : TraitTestData
    {
        public override string Trait => "Amphibious";

        public override string MonsterTraitString =>
            "The aboleth can breathe air and water.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The aboleth" } },
        };
    }

    public sealed class Aboleth_MucousCloud : TraitTestData
    {
        public override string Trait => "Mucous Cloud";

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
        public override string Trait => "Probing Telepathy";

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
        public override string Trait => "Brute";

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
        public override string Trait => "Surprise Attack";

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
        public override string Trait => "Standing Leap";

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
        public override string Trait => "Charge";

        public override string MonsterTraitString =>
            "If the centaur moves at least 30 feet straight toward a target and then hits it " +
            "with a pike attack on the same turn, the target takes an extra 10 (3d6) piercing damage.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the centaur" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 30 } },
            { "attack:Attack", new Arg { key = "attack", argType = "Attack", value = new AttackRefArgs { attack = "pike" } } },
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
        public override string Trait => "Charge";

        public override string MonsterTraitString =>
            "If the minotaur moves at least 10 feet straight toward a target and then hits it with " +
            "a gore attack on the same turn, the target takes an extra 9 (2d8) piercing damage. If " +
            "the target is a creature, it must succeed on a DC 14 Strength saving throw or be pushed " +
            "up to 10 feet away and knocked prone.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the minotaur" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 10 } },
            { "attack:Attack", new Arg { key = "attack", argType = "Attack", value = new AttackRefArgs { attack = "gore" } } },
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
        public override string Trait => "Labyrinthine Recall";

        public override string MonsterTraitString =>
            "The minotaur can perfectly recall any path it has traveled.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The minotaur" } },
        };
    }

    public sealed class Minotaur_Reckless : TraitTestData
    {
        public override string Trait => "Reckless";

        public override string MonsterTraitString =>
            "At the start of its turn, the minotaur can gain advantage on all melee weapon attack rolls " +
            "it makes during that turn, but attack rolls against it have advantage until the start of its next turn.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the minotaur" } },
        };
    }

    #endregion Minotaur

    #region Chuul

    public sealed class Chuul_Amphibious : TraitTestData
    {
        public override string Trait => "Amphibious";

        public override string MonsterTraitString =>
            "The chuul can breathe air and water.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The chuul" } },
        };
    }

    public sealed class Chuul_SenseMagic : TraitTestData
    {
        public override string Trait => "Sense Magic";

        public override string MonsterTraitString =>
            "The chuul senses magic within 120 feet of it at will. This trait otherwise works like the " +
            "*detect magic* spell but isn’t itself magical.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The chuul" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 120 } },
        };
    }

    #endregion Chuul

    #region Cloaker

    public sealed class Cloaker_DamageTransfer : TraitTestData
    {
        public override string Trait => "Damage Transfer - Cloaker";

        public override string MonsterTraitString =>
            "While attached to a creature, the cloaker takes only half the damage dealt to it " +
            "(rounded down), and that creature takes the other half.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the cloaker" } },
        };
    }

    public sealed class Cloaker_FalseAppearance : TraitTestData
    {
        public override string Trait => "False Appearance";

        public override string MonsterTraitString =>
            "While the cloaker remains motionless without its underside exposed, " +
            "it is indistinguishable from a dark leather cloak.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the cloaker" } },
            { "more:YesNo", new Arg { key = "more", argType = "YesNo", value = "Yes" } },
            { "moreRequirements:Text", new Arg { key = "moreRequirements", argType = "Text", value = " without its underside exposed" } },
            { "description:Text", new Arg { key = "description", argType = "Text", value = "a dark leather cloak" } },
        };
    }

    public sealed class Cloaker_LightSensitivity : TraitTestData
    {
        public override string Trait => "Light Sensitivity";

        public override string MonsterTraitString =>
            "While in bright light, the cloaker has disadvantage on attack rolls and " +
            "Wisdom (Perception) checks that rely on sight.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the cloaker" } },
        };
    }

    #endregion Cloaker

    #region Rug of Smothering

    public sealed class RugOfSmothering_DamageTransfer : TraitTestData
    {
        public override string Trait => "Damage Transfer - Rug of Smothering";

        public override string MonsterTraitString =>
            "While it is grappling a creature, the rug takes only half the damage dealt to it, " +
            "and the creature grappled by the rug takes the other half.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the rug" } },
        };
    }

    public sealed class RugOfSmothering_AntimagicSusceptibility : TraitTestData
    {
        public override string Trait => "Antimagic Susceptibility";

        public override string MonsterTraitString =>
            "The rug is incapacitated while in the area of an *antimagic field.* If targeted by " +
            "*dispel magic*, the rug must succeed on a Constitution saving throw against the " +
            "caster’s spell save DC or fall unconscious for 1 minute.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The rug" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the rug" } },
        };
    }

    public sealed class RugOfSmothering_FalseAppearance : TraitTestData
    {
        public override string Trait => "False Appearance";

        public override string MonsterTraitString =>
            "While the rug remains motionless, it is indistinguishable from a normal rug.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the rug" } },
            { "more:YesNo", new Arg { key = "more", argType = "YesNo", value = "No" } },
            { "description:Text", new Arg { key = "description", argType = "Text", value = "a normal rug" } },
        };
    }

    #endregion Rug of Smothering

    #region Couatl

    public sealed class Couatl_MagicWeapons : TraitTestData
    {
        public override string Trait => "Magic Weapons";

        public override string MonsterTraitString =>
            "The couatl’s weapon attacks are magical.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The couatl" } },
        };
    }

    public sealed class Couatl_ShieldedMind : TraitTestData
    {
        public override string Trait => "Shielded Mind";

        public override string MonsterTraitString =>
            "The couatl is immune to scrying and to any effect that would sense its emotions, " +
            "read its thoughts, or detect its location.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The couatl" } },
        };
    }

    #endregion Couatl

    #region Darkmantle

    public sealed class Darkmantle_Echolocation : TraitTestData
    {
        public override string Trait => "Echolocation";

        public override string MonsterTraitString =>
            "The darkmantle can’t use its blindsight while deafened.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The darkmantle" } },
        };
    }

    public sealed class Darkmantle_FalseAppearance : TraitTestData
    {
        public override string Trait => "False Appearance";

        public override string MonsterTraitString =>
            "While the darkmantle remains motionless, it is indistinguishable from a cave formation " +
            "such as a stalactite or stalagmite.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the darkmantle" } },
            { "more:YesNo", new Arg { key = "more", argType = "YesNo", value = "No" } },
            { "description:Text", new Arg
                {
                    key = "description",
                    argType = "Text",
                    value = "a cave formation such as a stalactite or stalagmite"
                } },
        };
    }

    #endregion Darkmantle

    #region Drider

    public sealed class Drider_FeyAncestry : TraitTestData
    {
        public override string Trait => "Fey Ancestry";

        public override string MonsterTraitString =>
            "The drider has advantage on saving throws against being charmed, and " +
            "magic can’t put the drider to sleep.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The drider" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the drider" } },
        };
    }

    public sealed class Drider_SpiderClimb : TraitTestData
    {
        public override string Trait => "Spider Climb";

        public override string MonsterTraitString =>
            "The drider can climb difficult surfaces, including upside down on ceilings, " +
            "without needing to make an ability check.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The drider" } },
        };
    }

    public sealed class Drider_SunlightSensitivity : TraitTestData
    {
        public override string Trait => "Sunlight Sensitivity";

        public override string MonsterTraitString =>
            "While in sunlight, the drider has disadvantage on attack rolls, " +
            "as well as on Wisdom (Perception) checks that rely on sight.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the drider" } },
        };
    }

    public sealed class Drider_WebWalker : TraitTestData
    {
        public override string Trait => "Web Walker";

        public override string MonsterTraitString =>
            "The drider ignores movement restrictions caused by webbing.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The drider" } },
        };
    }

    #endregion Drider

    #region Ettercap

    public sealed class Ettercap_SpiderClimb : TraitTestData
    {
        public override string Trait => "Spider Climb";

        public override string MonsterTraitString =>
            "The ettercap can climb difficult surfaces, including upside down on ceilings, " +
            "without needing to make an ability check.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The ettercap" } },
        };
    }

    public sealed class Ettercap_WebSense : TraitTestData
    {
        public override string Trait => "Web Sense";

        public override string MonsterTraitString =>
            "While in contact with a web, the ettercap knows the exact location " +
            "of any other creature in contact with the same web.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the ettercap" } },
        };
    }

    public sealed class Ettercap_WebWalker : TraitTestData
    {
        public override string Trait => "Web Walker";

        public override string MonsterTraitString =>
            "The ettercap ignores movement restrictions caused by webbing.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The ettercap" } },
        };
    }

    #endregion Ettercap

    #region Ettin

    public sealed class Ettin_TwoHeads : TraitTestData
    {
        public override string Trait => "Two Heads";

        public override string MonsterTraitString =>
            "The ettin has advantage on Wisdom (Perception) checks and on saving throws against " +
            "being blinded, charmed, deafened, frightened, stunned, and knocked unconscious.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The ettin" } },
        };
    }

    public sealed class Ettin_Wakeful : TraitTestData
    {
        public override string Trait => "Wakeful - Ettin";

        public override string MonsterTraitString =>
            "When one of the ettin’s heads is asleep, its other head is awake.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the ettin" } },
        };
    }

    #endregion Ettin

    #region Ghost

    public sealed class Ghost_EtherealSight : TraitTestData
    {
        public override string Trait => "Ethereal Sight";

        public override string MonsterTraitString =>
            "The ghost can see 60 feet into the Ethereal Plane when it is " +
            "on the Material Plane, and vice versa.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The ghost" } },
        };
    }

    public sealed class Ghost_IncorporealMovement : TraitTestData
    {
        public override string Trait => "Incorporeal Movement";

        public override string MonsterTraitString =>
            "The ghost can move through other creatures and objects as if they were difficult terrain. " +
            "It takes 5 (1d10) force damage if it ends its turn inside an object.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The ghost" } },
            { "damage:Damage:Typed", new Arg
                {
                    key = "damage",
                    argType = "Damage",
                    flags = new[] { "Typed" },
                    value = new TypedDamageArgs
                    {
                        diceCount = 1,
                        dieSize = 10,
                        damageType = "force",
                    }
                } },
        };
    }

    #endregion Ghost

    #region Gibbering Mouther

    public sealed class GibberingMouther_AberrantGround : TraitTestData
    {
        public override string Trait => "Aberrant Ground";

        public override string MonsterTraitString =>
            "The ground in a 10-foot radius around the mouther is doughlike difficult terrain. " +
            "Each creature that starts its turn in that area must succeed on a DC 10 Strength saving throw " +
            "or have its speed reduced to 0 until the start of its next turn.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the mouther" } },
            { "radius:Number", new Arg { key = "radius", argType = "Number", value = 10 } },
            { "save:SavingThrow", new Arg
                {
                    key = "save",
                    argType = "SavingThrow",
                    value = new SavingThrowArgs
                    {
                        Attribute = "Strength",
                        DC = 10,
                    }
                } }
        };
    }

    public sealed class GibberingMouther_Gibbering : TraitTestData
    {
        public override string Trait => "Gibbering";

        public override string MonsterTraitString =>
            "The mouther babbles incoherently while it can see any creature and isn’t incapacitated. " +
            "Each creature that starts its turn within 20 feet of the mouther and can hear the gibbering " +
            "must succeed on a DC 10 Wisdom saving throw. On a failure, the creature can’t take reactions " +
            "until the start of its next turn and rolls a d8 to determine what it does during its turn. " +
            "On a 1 to 4, the creature does nothing. On a 5 or 6, the creature takes no action or bonus action " +
            "and uses all its movement to move in a randomly determined direction. On a 7 or 8, " +
            "the creature makes a melee attack against a randomly determined creature within its reach " +
            "or does nothing if it can’t make such an attack.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the mouther" } },
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The mouther" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 20 } },
            { "save:SavingThrow", new Arg
                {
                    key = "save",
                    argType = "SavingThrow",
                    value = new SavingThrowArgs
                    {
                        Attribute = "Wisdom",
                        DC = 10
                    }
                } }
        };
    }

    #endregion Gibbering Mouther

    #region Gnoll

    public sealed class Gnoll_Rampage : TraitTestData
    {
        public override string Trait => "Rampage";

        public override string MonsterTraitString =>
            "When the gnoll reduces a creature to 0 hit points with a melee attack on its turn, " +
            "the gnoll can take a bonus action to move up to half its speed and make a bite attack.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the gnoll" } },
            { "attack:Attack", new Arg { key = "attack", argType = "Attack", value = new AttackRefArgs { attack = "bite" } } },
        };
    }

    #endregion Gnoll

    #region Deep Gnome

    public sealed class DeepGnome_StoneCamouflage : TraitTestData
    {
        public override string Trait => "Stone Camouflage";

        public override string MonsterTraitString =>
            "The gnome has advantage on Dexterity (Stealth) checks made to hide in rocky terrain.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The gnome" } },
        };
    }

    public sealed class DeepGnome_GnomeCunning : TraitTestData
    {
        public override string Trait => "Gnome Cunning";

        public override string MonsterTraitString =>
            "The gnome has advantage on Intelligence, Wisdom, and Charisma saving throws against magic.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The gnome" } },
        };
    }

    #endregion Deep Gnome

    #region Goblin

    public sealed class Goblin_NimbleEscape : TraitTestData
    {
        public override string Trait => "Nimble Escape";

        public override string MonsterTraitString =>
            "The goblin can take the Disengage or Hide action as a bonus action on each of its turns.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The goblin" } },
        };
    }

    #endregion Goblin

    #region Gorgon

    public sealed class Gorgon_TramplingCharge : TraitTestData
    {
        public override string Trait => "Trampling Charge";

        public override string MonsterTraitString =>
            "If the gorgon moves at least 20 feet straight toward a creature and then hits it with " +
            "a gore attack on the same turn, that target must succeed on a DC 16 Strength saving throw " +
            "or be knocked prone. If the target is prone, the gorgon can make one attack with its hooves " +
            "against it as a bonus action.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the gorgon" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 20 } },
            { "attack:Attack", new Arg { key = "attack", argType = "Attack", value = new AttackRefArgs { attack = "gore" } } },
            { "save:SavingThrow", new Arg
                {
                    key = "save",
                    argType = "SavingThrow",
                    value = new SavingThrowArgs { Attribute = "Strength", DC = 16 }
                } },
            { "extraAttack:Attack", new Arg
            {
                key = "extraAttack",
                argType = "Attack",
                value = new AttackRefArgs { attack = "hooves", withIts = true }
            } },
        };
    }

    #endregion Gorgon

    #region Triceratops

    public sealed class Triceratops_TramplingCharge : TraitTestData
    {
        public override string Trait => "Trampling Charge";

        public override string MonsterTraitString =>
            "If the triceratops moves at least 20 feet straight toward a creature and then hits it with " +
            "a gore attack on the same turn, that target must succeed on a DC 13 Strength saving throw " +
            "or be knocked prone. If the target is prone, the triceratops can make one stomp attack " +
            "against it as a bonus action.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the triceratops" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 20 } },
            { "attack:Attack", new Arg { key = "attack", argType = "Attack", value = new AttackRefArgs { attack = "gore" } } },
            { "save:SavingThrow", new Arg
                {
                    key = "save",
                    argType = "SavingThrow",
                    value = new SavingThrowArgs { Attribute = "Strength", DC = 13 }
                } },
            { "extraAttack:Attack", new Arg
                {
                    key = "extraAttack",
                    argType = "Attack",
                    value = new AttackRefArgs { attack = "stomp" }
                } },
        };
    }

    #endregion Triceratops

    #region Griffon

    public sealed class Griffon_KeenSight : TraitTestData
    {
        public override string Trait => "Keen Sight";

        public override string MonsterTraitString =>
            "The griffon has advantage on Wisdom (Perception) checks that rely on sight.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The griffon" } },
        };
    }

    #endregion Griffon

    #region Grimlock

    public sealed class Grimlock_BlindSenses : TraitTestData
    {
        public override string Trait => "Blind Senses";

        public override string MonsterTraitString =>
            "The grimlock can’t use its blindsight while deafened and unable to smell.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The grimlock" } },
        };
    }

    public sealed class Grimlock_KeenHearingAndSmell : TraitTestData
    {
        public override string Trait => "Keen Hearing and Smell";

        public override string MonsterTraitString =>
            "The grimlock has advantage on Wisdom (Perception) checks that rely on hearing or smell.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The grimlock" } },
        };
    }

    public sealed class Grimlock_StoneCamouflage : TraitTestData
    {
        public override string Trait => "Stone Camouflage";

        public override string MonsterTraitString =>
            "The grimlock has advantage on Dexterity (Stealth) checks made to hide in rocky terrain.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The grimlock" } },
        };
    }

    #endregion Grimlock

    #region Hell Hound

    public sealed class HellHound_KeenHearingAndSmell : TraitTestData
    {
        public override string Trait => "Keen Hearing and Smell";

        public override string MonsterTraitString =>
            "The hound has advantage on Wisdom (Perception) checks that rely on hearing or smell.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The hound" } },
        };
    }

    public sealed class HellHound_PackTactics : TraitTestData
    {
        public override string Trait => "Pack Tactics";

        public override string MonsterTraitString =>
            "The hound has advantage on an attack roll against a creature if at least one of " +
            "the hound’s allies is within 5 feet of the creature and the ally isn’t incapacitated.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The hound" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the hound" } },
        };
    }

    #endregion Hell Hound

    #region Hobgoblin

    public sealed class Hobgoblin_MartialAdvantage : TraitTestData
    {
        public override string Trait => "Martial Advantage";

        public override string MonsterTraitString =>
            "Once per turn, the hobgoblin can deal an extra 7 (2d6) damage to a creature " +
            "it hits with a weapon attack if that creature is within 5 feet of an ally of " +
            "the hobgoblin that isn’t incapacitated.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the hobgoblin" } },
            { "damage:Damage", new Arg
                {
                    key = "damage",
                    argType = "Damage",
                    value = new DamageArgs { diceCount = 2, dieSize = 6 },
                } },
        };
    }

    #endregion Hobgoblin

    #region Vampire

    public sealed class Vampire_Shapechanger : TraitTestData
    {
        public override string Trait => "Shapechanger - Vampire";

        public override string MonsterTraitString =>
            "If the vampire isn’t in sunlight or running water, it can use its action to polymorph " +
            "into a Tiny bat or a Medium cloud of mist, or back into its true form.\\r\\n" +

            "While in bat form, the vampire can’t speak, its walking speed is 5 feet, and it has a " +
            "flying speed of 30 feet. Its statistics, other than its size and speed, are unchanged. " +
            "Anything it is wearing transforms with it, but nothing it is carrying does. It reverts " +
            "to its true form if it dies.\\r\\n" +

            "While in mist form, the vampire can’t take any actions, speak, or manipulate objects. It " +
            "is weightless, has a flying speed of 20 feet, can hover, and can enter a hostile creature’s " +
            "space and stop there. In addition, if air can pass through a space, the mist can do so " +
            "without squeezing, and it can’t pass through water. It has advantage on Strength, Dexterity, " +
            "and Constitution saving throws, and it is immune to all nonmagical damage, except the " +
            "damage it takes from sunlight.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the vampire" } },
        };
    }

    public sealed class Vampire_MistyEscape : TraitTestData
    {
        public override string Trait => "Misty Escape - Vampire";

        public override string MonsterTraitString =>
            "When it drops to 0 hit points outside its resting place, the vampire transforms into a " +
            "cloud of mist (as in the Shapechanger trait) instead of falling unconscious, provided that " +
            "it isn’t in sunlight or running water. If it can’t transform, it is destroyed.\\r\\n" +

            "While it has 0 hit points in mist form, it can’t revert to its vampire form, and it must " +
            "reach its resting place within 2 hours or be destroyed. Once in its resting place, it reverts " +
            "to its vampire form. It is then paralyzed until it regains at least 1 hit point. After " +
            "spending 1 hour in its resting place with 0 hit points, it regains 1 hit point.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the vampire" } },
            { "regularForm:Text", new Arg { key = "regularForm", argType = "Text", value = "vampire" } },
        };
    }

    public sealed class Vampire_Regeneration : TraitTestData
    {
        public override string Trait => "Regeneration";

        public override string MonsterTraitString =>
            "The vampire regains 20 hit points at the start of its turn if it has at least 1 hit point " +
            "and isn’t in sunlight or running water. If the vampire takes radiant damage or damage from " +
            "holy water, this trait doesn’t function at the start of the vampire’s next turn.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The vampire" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the vampire" } },
            { "amount:Number", new Arg { key = "amount", argType = "Number", value = 20 } },
            { "hasConditions:YesNo", new Arg { key = "hasConditions", argType = "YesNo", value = "Yes" } },
            { "conditions:MultiOption", new Arg
                {
                    key = "conditions",
                    argType = "MultiOption",
                    value = new[]
                    {
                        "has at least 1 hit point",
                        "isn’t in sunlight or running water",
                    }
                } },
            { "canBeShutOff:YesNo", new Arg { key = "canBeShutOff", argType = "YesNo", value = "Yes" } },
            { "damage:Text", new Arg { key = "damage", argType = "Text", value = "radiant damage or damage from holy water" } },
            { "hasExtraDeathCondition:YesNo", new Arg { key = "hasExtraDeathCondition", argType = "YesNo", value = "No" } },
        };
    }

    public sealed class Vampire_SpiderClimb : TraitTestData
    {
        public override string Trait => "Spider Climb";

        public override string MonsterTraitString =>
            "The vampire can climb difficult surfaces, including upside down on ceilings, " +
            "without needing to make an ability check.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The vampire" } },
        };
    }

    public sealed class Vampire_VampireWeaknesses : TraitTestData
    {
        public override string Trait => "Vampire Weaknesses";

        public override string MonsterTraitString =>
            "The vampire has the following flaws:\\r\\n" +

            "*Forbiddance.* The vampire can’t enter a residence without an invitation from one of the occupants.\\r\\n" +

            "*Harmed by Running Water.* The vampire takes 20 acid damage if it ends its turn in running water.\\r\\n" +

            "*Stake to the Heart.* If a piercing weapon made of wood is driven into the vampire’s heart while " +
            "the vampire is incapacitated in its resting place, the vampire is paralyzed until the stake is " +
            "removed.\\r\\n" +

            "*Sunlight Hypersensitivity.* The vampire takes 20 radiant damage when it starts its turn in " +
            "sunlight. While in sunlight, it has disadvantage on attack rolls and ability checks.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The vampire" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the vampire" } },
        };
    }

    #endregion Vampire

    #region Troll

    public sealed class Troll_KeenSmell : TraitTestData
    {
        public override string Trait => "Keen Smell";

        public override string MonsterTraitString =>
            "The troll has advantage on Wisdom (Perception) checks that rely on smell.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The troll" } },
        };
    }

    public sealed class Troll_Regeneration : TraitTestData
    {
        public override string Trait => "Regeneration";

        public override string MonsterTraitString =>
            "The troll regains 10 hit points at the start of its turn. If the troll " +
            "takes acid or fire damage, this trait doesn’t function at the start of the " +
            "troll’s next turn. The troll dies only if it starts its turn with 0 hit " +
            "points and doesn’t regenerate.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The troll" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the troll" } },
            { "amount:Number", new Arg { key = "amount", argType = "Number", value = 10 } },
            { "hasConditions:YesNo", new Arg { key = "hasConditions", argType = "YesNo", value = "No" } },
            { "canBeShutOff:YesNo", new Arg { key = "canBeShutOff", argType = "YesNo", value = "Yes" } },
            { "damage:Text", new Arg { key = "damage", argType = "Text", value = "acid or fire damage" } },
            { "hasExtraDeathCondition:YesNo", new Arg { key = "hasExtraDeathCondition", argType = "YesNo", value = "Yes" } },
        };
    }

    #endregion Troll
}
