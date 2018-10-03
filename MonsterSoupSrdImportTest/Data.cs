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

    /// <summary>
    /// Test Traits
    /// </summary>
    public abstract class TraitTestData
    {
        public string TraitTemplate => TraitTemplates.StandardTraits[Trait].Template;

        public abstract string Trait { get; }
        public virtual string Requirements { get; } = null;
        public abstract string MonsterTraitString { get; }
        public abstract Dictionary<string, Arg> ExpectedArgsOutput { get; }
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
            { "Damage Transfer", new Cloaker_DamageTransfer() },
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
            { "Wakeful", new Ettin_Wakeful() },
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

    public sealed class Hobgoblin : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Martial Advantage", new Hobgoblin_MartialAdvantage() },
        };
    }

    public sealed class Homunculus : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Telepathic Bond", new Homunculus_TelepathicBond() },
        };
    }

    public sealed class Hydra : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Hold Breath", new Hydra_HoldBreath() },
            { "Multiple Heads", new Hydra_MultipleHeads() },
            { "Reactive Heads", new Hydra_ReactiveHeads() },
            { "Wakeful", new Hydra_Wakeful() },
        };
    }

    public sealed class InvisibleStalker : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Invisibility", new InvisibleStalker_Invisibility() },
            { "Faultless Tracker", new InvisibleStalker_FaultlessTracker() },
        };
    }

    public sealed class Kraken : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Amphibious", new Kraken_Amphibious() },
            { "Freedom of Movement", new Kraken_FreedomOfMovement() },
            { "Siege Monster", new Kraken_SiegeMonster() },
        };
    }

    public sealed class Lich : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Rejuvenation", new Lich_Rejuvenation() },
            { "Turn Resistance", new Lich_TurnResistance() },
        };
    }

    public sealed class Magmin : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Death Burst", new Magmin_DeathBurst() },
            { "Ignited Illumination", new Magmin_IgnitedIllumination() },
        };
    }

    #region Mephits

    public sealed class DustMephit : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Death Burst", new DustMephit_DeathBurst() },
        };
    }

    public sealed class IceMephit : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Death Burst", new IceMephit_DeathBurst() },
            { "False Appearance", new IceMephit_FalseAppearance() },
        };
    }

    public sealed class SteamMephit : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Death Burst", new SteamMephit_DeathBurst() },
        };
    }

    #endregion Mephits

    public sealed class Minotaur : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Charge", new Minotaur_Charge() },
            { "Labyrinthine Recall", new Minotaur_LabyrinthineRecall() },
            { "Reckless", new Minotaur_Reckless() },
        };
    }

    public sealed class Mimic : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Shapechanger", new Mimic_Shapechanger() },
            { "Adhesive", new Mimic_Adhesive() },
            { "False Appearance", new Mimic_FalseAppearance() },
            { "Grappler", new Mimic_Grappler() },
        };
    }

    public sealed class MummyLord : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Magic Resistance", new MummyLord_MagicResistance() },
            { "Rejuvenation", new MummyLord_Rejuvenation() },
        };
    }

    public sealed class RugOfSmothering : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Antimagic Susceptibility", new RugOfSmothering_AntimagicSusceptibility() },
            { "Damage Transfer", new RugOfSmothering_DamageTransfer() },
            { "False Appearance", new RugOfSmothering_FalseAppearance() },
        };
    }

    public sealed class RustMonster : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Iron Scent", new RustMonster_IronScent() },
            { "Rust Metal", new RustMonster_RustMetal() },
        };
    }

    public sealed class Sahuagin : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Blood Frenzy", new Sahuagin_BloodFrenzy() },
            { "Limited Amphibiousness", new Sahuagin_LimitedAmphibiousness() },
            { "Shark Telepathy", new Sahuagin_SharkTelepathy() },
        };
    }

    public sealed class Shadow : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Amorphous", new Shadow_Amorphous() },
            { "Shadow Stealth", new Shadow_ShadowStealth() },
            { "Sunlight Weakness", new Shadow_SunlightWeakness() },
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
            { "Shapechanger", new Vampire_Shapechanger() },
            { "Misty Escape - Vampire", new Vampire_MistyEscape() },
            { "Regeneration", new Vampire_Regeneration() },
            { "Spider Climb", new Vampire_SpiderClimb() },
            { "Vampire Weaknesses", new Vampire_VampireWeaknesses() },
        };
    }

    public sealed class Wereboar : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Shapechanger", new Wereboar_Shapechanger() },
            { "Charge", new Wereboar_Charge() },
            { "Relentless", new Wereboar_Relentless() },
        };
    }

    public sealed class Werewolf : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Shapechanger", new Werewolf_Shapechanger() },
        };
    }

    public sealed class WillOWisp : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Consume Life", new WillOWisp_ConsumeLife() },
            { "Ephemeral", new WillOWisp_Ephemeral() },
            { "Incorporeal Movement", new WillOWisp_IncorporealMovement() },
            { "Variable Illumination", new WillOWisp_VariableIllumination() },
        };
    }

    public sealed class Xorn : MonsterTestData
    {
        public override Dictionary<string, TraitTestData> Traits => new Dictionary<string, TraitTestData>
        {
            { "Earth Glide", new Xorn_EarthGlide() },
            { "Stone Camouflage", new Xorn_StoneCamouflage() },
            { "Treasure Sense", new Xorn_TreasureSense() },
        };
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
            { "anAttack:Attack", new Arg { key = "anAttack", argType = "Attack", value = new AttackRefArgs { attack = "pike", article = "a", saysAttack = true } } },
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
            { "anAttack:Attack", new Arg { key = "anAttack", argType = "Attack", value = new AttackRefArgs { attack = "gore", article = "a", saysAttack = true } } },
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
        public override string Trait => "Damage Transfer";

        public override string MonsterTraitString =>
            "While attached to a creature, the cloaker takes only half the damage dealt to it " +
            "(rounded down), and that creature takes the other half.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the cloaker" } },
            { "template:Dropdown:[Cloaker,RugOfSmothering]", new Arg { key = "template", argType = "Dropdown", value = "Cloaker" } },
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
            { "moreRequirements:Text", new Arg { key = "moreRequirements", argType = "Text", value = "without its underside exposed" } },
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
        public override string Trait => "Damage Transfer";

        public override string MonsterTraitString =>
            "While it is grappling a creature, the rug takes only half the damage dealt to it, " +
            "and the creature grappled by the rug takes the other half.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the rug" } },
            { "template:Dropdown:[Cloaker,RugOfSmothering]", new Arg { key = "template", argType = "Dropdown", value = "RugOfSmothering" } },
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
        public override string Trait => "Wakeful";

        public override string MonsterTraitString =>
            "When one of the ettin’s heads is asleep, its other head is awake.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the ettin" } },
            { "template:Dropdown:[Ettin,Hydra]", new Arg { key = "template", argType = "Dropdown", value = "Ettin" } },
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
            { "anAttack:Attack", new Arg { key = "anAttack", argType = "Attack", value = new AttackRefArgs { attack = "bite", article = "a", saysAttack = true } } },
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
            "or be knocked prone.\r\n" +
            
            "If the target is prone, the gorgon can make one attack with its hooves " +
            "against it as a bonus action.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the gorgon" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 20 } },
            { "anAttack:Attack", new Arg { key = "anAttack", argType = "Attack", value = new AttackRefArgs { attack = "gore", article = "a", saysAttack = true } } },
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
                    value = new AttackRefArgs { attack = "hooves", attackWith = true, article = "its", saysAttack = false }
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
            "or be knocked prone.\r\n" +
            
            "If the target is prone, the triceratops can make one stomp attack " +
            "against it as a bonus action.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the triceratops" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 20 } },
            { "anAttack:Attack", new Arg { key = "anAttack", argType = "Attack", value = new AttackRefArgs { attack = "gore", article = "a", saysAttack = true } } },
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
                    value = new AttackRefArgs { attack = "stomp", article = string.Empty, saysAttack = true }
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
        public override string Trait => "Shapechanger";

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
            { "template:Dropdown:[Doppelganger,Fiend,Lycanthrope,Mimic,Succubus,Vampire]", new Arg { key = "template", argType = "Dropdown", value = "Vampire" } },
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
            { "ifWhen:Dropdown:[if,when]", new Arg { key = "ifWhen", argType = "Dropdown", value = "if" } },
            { "stakeEffect:Dropdown:[Destroys,Paralyzes]", new Arg { key = "stakeEffect", argType = "Dropdown", value = "Paralyzes" } },
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

    #region Hydra

    public sealed class Hydra_HoldBreath : TraitTestData
    {
        public override string Trait => "Hold Breath";

        public override string MonsterTraitString =>
            "The hydra can hold its breath for 1 hour.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The hydra" } },
            { "duration:Text", new Arg { key = "duration", argType = "Text", value = "1 hour" } },
        };
    }

    public sealed class Hydra_MultipleHeads : TraitTestData
    {
        public override string Trait => "Multiple Heads";

        public override string MonsterTraitString =>
@"The hydra has five heads. While it has more than one head, the hydra has advantage on saving throws against being blinded, charmed, deafened, frightened, stunned, and knocked unconscious.
Whenever the hydra takes 25 or more damage in a single turn, one of its heads dies. If all its heads die, the hydra dies.
At the end of its turn, it grows two heads for each of its heads that died since its last turn, unless it has taken fire damage since its last turn. The hydra regains 10 hit points for each head regrown in this way.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The hydra" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the hydra" } },
            { "headCount:Text", new Arg { key = "headCount", argType = "Text", value = "five" } },
            { "minimumDamage:Number", new Arg { key = "minimumDamage", argType = "Number", value = 25 } },
            { "damageType:Text", new Arg { key = "damageType", argType = "Text", value = "fire damage" } },
            { "healAmount:Number", new Arg { key = "healAmount", argType = "Number", value = 10 } },
        };
    }

    public sealed class Hydra_ReactiveHeads : TraitTestData
    {
        public override string Trait => "Reactive Heads";

        public override string MonsterTraitString =>
            "For each head the hydra has beyond one, it gets an extra reaction that " +
            "can be used only for opportunity attacks.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the hydra" } },
        };
    }

    public sealed class Hydra_Wakeful : TraitTestData
    {
        public override string Trait => "Wakeful";

        public override string MonsterTraitString =>
            "While the hydra sleeps, at least one of its heads is awake.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the hydra" } },
            { "template:Dropdown:[Ettin,Hydra]", new Arg { key = "template", argType = "Dropdown", value = "Hydra" } },
        };
    }

    #endregion Hydra

    #region Homunculus

    public sealed class Homunculus_TelepathicBond : TraitTestData
    {
        public override string Trait => "Telepathic Bond";

        public override string MonsterTraitString =>
            "While the homunculus is on the same plane of existence as its master, " +
            "it can magically convey what it senses to its master, and the two can " +
            "communicate telepathically.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "template:Dropdown:[Homunculus,Succubus]", new Arg { key = "template", argType = "Dropdown", value = "Homunculus" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the homunculus" } },
        };
    }

    #endregion Homunculus

    #region Invisible Stalker

    public sealed class InvisibleStalker_Invisibility : TraitTestData
    {
        public override string Trait => "Invisibility";

        public override string MonsterTraitString => "The stalker is invisible.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The stalker" } },
        };
    }

    public sealed class InvisibleStalker_FaultlessTracker : TraitTestData
    {
        public override string Trait => "Faultless Tracker";

        public override string MonsterTraitString =>
            "The stalker is given a quarry by its summoner. The stalker knows " +
            "the direction and distance to its quarry as long as the two of them " +
            "are on the same plane of existence. The stalker also knows the " +
            "location of its summoner.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The stalker" } },
        };
    }

    #endregion Invisible Stalker

    #region Kraken

    public sealed class Kraken_Amphibious : TraitTestData
    {
        public override string Trait => "Amphibious";

        public override string MonsterTraitString =>
            "The kraken can breathe air and water.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The kraken" } },
        };
    }

    public sealed class Kraken_FreedomOfMovement : TraitTestData
    {
        public override string Trait => "Freedom of Movement";

        public override string MonsterTraitString =>
            "The kraken ignores difficult terrain, and magical effects can’t " +
            "reduce its speed or cause it to be restrained. It can spend 5 feet " +
            "of movement to escape from nonmagical restraints or being grappled.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The kraken" } },
        };
    }

    public sealed class Kraken_SiegeMonster : TraitTestData
    {
        public override string Trait => "Siege Monster";

        public override string MonsterTraitString =>
            "The kraken deals double damage to objects and structures.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The kraken" } },
        };
    }

    #endregion Kraken

    #region Lich

    public sealed class Lich_Rejuvenation : TraitTestData
    {
        public override string Trait => "Rejuvenation";

        public override string MonsterTraitString =>
            "If it has a phylactery, a destroyed lich gains a new body in 1d10 days, " +
            "regaining all its hit points and becoming active again. The new body " +
            "appears within 5 feet of the phylactery.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "template:Dropdown:[Lich,MummyLord,Naga]", new Arg { key = "template", argType = "Dropdown", value = "Lich" } },
            { "shortShortName", new Arg { key = "shortShortName", argType = "Inherent", value = "lich" } },
            { "diceRoll:DiceRoll", new Arg { key = "diceRoll", argType = "DiceRoll", value = new DiceRollArgs { diceCount = 1, dieSize = 10 } } },
        };
    }

    public sealed class Lich_TurnResistance : TraitTestData
    {
        public override string Trait => "Turn Resistance";

        public override string MonsterTraitString =>
            "The lich has advantage on saving throws against any effect that turns undead.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The lich" } },
        };
    }

    #endregion Lich

    #region Mummy Lord

    public sealed class MummyLord_MagicResistance : TraitTestData
    {
        public override string Trait => "Magic Resistance";

        public override string MonsterTraitString =>
            "The mummy lord has advantage on saving throws against spells and " +
            "other magical effects.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The mummy lord" } },
        };
    }

    public sealed class MummyLord_Rejuvenation : TraitTestData
    {
        public override string Trait => "Rejuvenation";

        public override string MonsterTraitString =>
            "A destroyed mummy lord gains a new body in 24 hours if its heart is intact, " +
            "regaining all its hit points and becoming active again. The new body " +
            "appears within 5 feet of the mummy lord’s heart.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "template:Dropdown:[Lich,MummyLord,Naga]", new Arg { key = "template", argType = "Dropdown", value = "MummyLord" } },
            { "shortShortName", new Arg { key = "shortShortName", argType = "Inherent", value = "mummy lord" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the mummy lord" } },
        };
    }

    #endregion Mummy Lord

    #region Magmin

    public sealed class Magmin_DeathBurst : TraitTestData
    {
        public override string Trait => "Death Burst";

        public override string MonsterTraitString =>
            "When the magmin dies, it explodes in a burst of fire and magma. Each creature " +
            "within 10 feet of it must make a DC 11 Dexterity saving throw, taking " +
            "7 (2d6) fire damage on a failed save, or half as much damage on a successful one. " +
            "Flammable objects that aren’t being worn or carried in that area are ignited.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the magmin" } },
            { "anEffect:Text", new Arg { key = "anEffect", argType = "Text", value = "a burst of fire and magma" } },
            { "radius:Number", new Arg { key = "radius", argType = "Number", value = 10 } },
            { "save:SavingThrow", new Arg { key = "save", argType = "SavingThrow", value = new SavingThrowArgs {
                DC = 11,
                Attribute = "Dexterity",
            } } },
            { "saveType:Dropdown:[NoDamage,HalfDamage,Effect]", new Arg { key = "saveType", argType = "Dropdown", value = "HalfDamage" } },
            { "damage:Damage:Typed", new Arg { key = "damage", argType = "Damage", flags = new[] { "Typed" }, value = new TypedDamageArgs {
                diceCount = 2,
                dieSize = 6,
                damageType = "fire"
            } } },
            { "ignitesObjects:YesNo", new Arg { key = "ignitesObjects", argType = "YesNo", value = "Yes" } },
        };
    }

    public sealed class Magmin_IgnitedIllumination : TraitTestData
    {
        public override string Trait => "Ignited Illumination";

        public override string MonsterTraitString =>
            "As a bonus action, the magmin can set itself ablaze or extinguish its flames. " +
            "While ablaze, the magmin sheds bright light in a 10-foot radius and dim light " +
            "for an additional 10 feet.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the magmin" } },
            { "radius:Number", new Arg { key = "radius", argType = "Number", value = 10 } },
        };
    }

    #endregion

    #region Mephits

    public sealed class DustMephit_DeathBurst : TraitTestData
    {
        public override string Trait => "Death Burst";

        public override string MonsterTraitString =>
            "When the mephit dies, it explodes in a burst of dust. Each creature within " +
            "5 feet of it must then succeed on a DC 10 Constitution saving throw or be " +
            "blinded for 1 minute. A blinded creature can repeat the saving throw on each " +
            "of its turns, ending the effect on itself on a success.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the mephit" } },
            { "anEffect:Text", new Arg { key = "anEffect", argType = "Text", value = "a burst of dust" } },
            { "radius:Number", new Arg { key = "radius", argType = "Number", value = 5 } },
            { "save:SavingThrow", new Arg { key = "save", argType = "SavingThrow", value = new SavingThrowArgs {
                DC = 10,
                Attribute = "Constitution",
            } } },
            { "saveType:Dropdown:[NoDamage,HalfDamage,Effect]", new Arg { key = "saveType", argType = "Dropdown", value = "Effect" } },
            { "affected:Text", new Arg { key = "affected", argType = "Text", value = "blinded" } },
            { "ignitesObjects:YesNo", new Arg { key = "ignitesObjects", argType = "YesNo", value = "No" } },
        };
    }

    public sealed class IceMephit_DeathBurst : TraitTestData
    {
        public override string Trait => "Death Burst";

        public override string MonsterTraitString =>
            "When the mephit dies, it explodes in a burst of jagged ice. Each creature within " +
            "5 feet of it must make a DC 10 Dexterity saving throw, taking 4 (1d8) slashing damage " +
            "on a failed save, or half as much damage on a successful one.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the mephit" } },
            { "anEffect:Text", new Arg { key = "anEffect", argType = "Text", value = "a burst of jagged ice" } },
            { "radius:Number", new Arg { key = "radius", argType = "Number", value = 5 } },
            { "save:SavingThrow", new Arg { key = "save", argType = "SavingThrow", value = new SavingThrowArgs {
                DC = 10,
                Attribute = "Dexterity",
            } } },
            { "saveType:Dropdown:[NoDamage,HalfDamage,Effect]", new Arg { key = "saveType", argType = "Dropdown", value = "HalfDamage" } },
            { "damage:Damage:Typed", new Arg { key = "damage", argType = "Damage", flags = new[] { "Typed" }, value = new TypedDamageArgs {
                diceCount = 1,
                dieSize = 8,
                damageType = "slashing"
            } } },
            { "ignitesObjects:YesNo", new Arg { key = "ignitesObjects", argType = "YesNo", value = "No" } },
        };
    }

    public sealed class IceMephit_FalseAppearance : TraitTestData
    {
        public override string Trait => "False Appearance";

        public override string MonsterTraitString =>
            "While the mephit remains motionless, it is indistinguishable from " +
            "an ordinary shard of ice.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the mephit" } },
            { "more:YesNo", new Arg { key = "more", argType = "YesNo", value = "No" } },
            { "description:Text", new Arg { key = "description", argType = "Text", value = "an ordinary shard of ice" } },
        };
    }

    public sealed class SteamMephit_DeathBurst : TraitTestData
    {
        public override string Trait => "Death Burst";

        public override string MonsterTraitString =>
            "When the mephit dies, it explodes in a cloud of steam. Each creature within " +
            "5 feet of the mephit must succeed on a DC 10 Dexterity saving throw or take " +
            "4 (1d8) fire damage.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the mephit" } },
            { "anEffect:Text", new Arg { key = "anEffect", argType = "Text", value = "a cloud of steam" } },
            { "radius:Number", new Arg { key = "radius", argType = "Number", value = 5 } },
            { "save:SavingThrow", new Arg { key = "save", argType = "SavingThrow", value = new SavingThrowArgs {
                DC = 10,
                Attribute = "Dexterity",
            } } },
            { "saveType:Dropdown:[NoDamage,HalfDamage,Effect]", new Arg { key = "saveType", argType = "Dropdown", value = "NoDamage" } },
            { "damage:Damage:Typed", new Arg { key = "damage", argType = "Damage", flags = new[] { "Typed" }, value = new TypedDamageArgs {
                diceCount = 1,
                dieSize = 8,
                damageType = "fire"
            } } },
            { "ignitesObjects:YesNo", new Arg { key = "ignitesObjects", argType = "YesNo", value = "No" } },
        };
    }

    #endregion Mephits

    #region Mimic

    public sealed class Mimic_Shapechanger : TraitTestData
    {
        public override string Trait => "Shapechanger";

        public override string MonsterTraitString =>
            "The mimic can use its action to polymorph into an object or back into its true, " +
            "amorphous form. Its statistics are the same in each form. Any equipment it is wearing " +
            "or carrying isn’t transformed. It reverts to its true form if it dies.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "template:Dropdown:[Doppelganger,Fiend,Lycanthrope,Mimic,Succubus,Vampire]", new Arg { key = "template", argType = "Dropdown", value = "Mimic" } },
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The mimic" } },
        };
    }

    public sealed class Mimic_Adhesive : TraitTestData
    {
        public override string Trait => "Adhesive";
        public override string Requirements => "Object Form Only";

        public override string MonsterTraitString =>
            "The mimic adheres to anything that touches it. A Huge or smaller creature adhered to " +
            "the mimic is also grappled by it (escape DC 13). Ability checks made to escape this " +
            "grapple have disadvantage.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The mimic" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the mimic" } },
            { "dc:Number", new Arg { key = "dc", argType = "Number", value = 13 } },
        };
    }

    public sealed class Mimic_FalseAppearance : TraitTestData
    {
        public override string Trait => "False Appearance";
        public override string Requirements => "Object Form Only";

        public override string MonsterTraitString =>
            "While the mimic remains motionless, it is indistinguishable from an ordinary object.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the mimic" } },
            { "more:YesNo", new Arg { key = "more", argType = "YesNo", value = "No" } },
            { "description:Text", new Arg { key = "description", argType = "Text", value = "an ordinary object" } },
        };
    }

    public sealed class Mimic_Grappler : TraitTestData
    {
        public override string Trait => "Grappler";

        public override string MonsterTraitString =>
            "The mimic has advantage on attack rolls against any creature grappled by it.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The mimic" } },
        };
    }

    #endregion

    #region Lycanthropes

    public sealed class Wereboar_Shapechanger : TraitTestData
    {
        public override string Trait => "Shapechanger";

        public override string MonsterTraitString =>
            "The wereboar can use its action to polymorph into a boar-humanoid hybrid or into " +
            "a boar, or back into its true form, which is humanoid. Its statistics, other than " +
            "its AC, are the same in each form. Any equipment it is wearing or carrying isn’t " +
            "transformed. It reverts to its true form if it dies.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "template:Dropdown:[Doppelganger,Fiend,Lycanthrope,Mimic,Succubus,Vampire]", new Arg { key = "template", argType = "Dropdown", value = "Lycanthrope" } },
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The wereboar" } },
            { "hybridType:Text", new Arg { key = "hybridType", argType = "Text", value = "boar" } },
            { "animalType:Text", new Arg { key = "animalType", argType = "Text", value = "boar" } },
            { "statistics:MultiOption", new Arg { key = "statistics", argType = "MultiOption", value = new[] { "AC" } } },
        };
    }

    public sealed class Wereboar_Charge : TraitTestData
    {
        public override string Trait => "Charge";
        public override string Requirements => "Boar or Hybrid Form Only";

        public override string MonsterTraitString =>
            "If the wereboar moves at least 15 feet straight toward a target and then hits it with " +
            "its tusks on the same turn, the target takes an extra 7 (2d6) slashing damage. If the " +
            "target is a creature, it must succeed on a DC 13 Strength saving throw or be knocked prone.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the wereboar" } },
            { "distance:Number", new Arg { key = "distance", argType = "Number", value = 15 } },
            { "anAttack:Attack", new Arg { key = "anAttack", argType = "Attack", value = new AttackRefArgs { attack = "tusks", article = "its", saysAttack = false } } },
            { "damage:Damage:Typed", new Arg
                {
                    key = "damage",
                    argType = "Damage",
                    flags = new[] { "Typed" },
                    value = new TypedDamageArgs
                    {
                        diceCount = 2,
                        dieSize = 6,
                        damageType = "slashing",
                    }
                } },
            { "hasSavingThrow:YesNo", new Arg { key = "hasSavingThrow", argType = "YesNo", value = "Yes" } },
            { "save:SavingThrow", new Arg
                {
                    key = "save",
                    argType = "SavingThrow",
                    value = new SavingThrowArgs { DC = 13, Attribute = "Strength" }
                } },
            { "affected:MultiOption", new Arg
                {
                    key = "affected",
                    argType = "MultiOption",
                    value = new[]
                    {
                        "knocked prone",
                    }
                } },
        };
    }

    public sealed class Wereboar_Relentless : TraitTestData
    {
        public override string Trait => "Relentless";
        public override string Requirements => "Recharges after a Short or Long Rest";

        public override string MonsterTraitString =>
            "If the wereboar takes 14 damage or less that would reduce it to 0 hit points, " +
            "it is reduced to 1 hit point instead.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the wereboar" } },
            { "amount:Number", new Arg { key = "amount", argType = "Number", value = 14 } },
        };
    }

    public sealed class Werewolf_Shapechanger : TraitTestData
    {
        public override string Trait => "Shapechanger";

        public override string MonsterTraitString =>
            "The werewolf can use its action to polymorph into a wolf-humanoid hybrid or into a wolf, " +
            "or back into its true form, which is humanoid. Its statistics, other than its AC, are the " +
            "same in each form. Any equipment it is wearing or carrying isn’t transformed. It reverts to " +
            "its true form if it dies.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "template:Dropdown:[Doppelganger,Fiend,Lycanthrope,Mimic,Succubus,Vampire]", new Arg { key = "template", argType = "Dropdown", value = "Lycanthrope" } },
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The werewolf" } },
            { "hybridType:Text", new Arg { key = "hybridType", argType = "Text", value = "wolf" } },
            { "animalType:Text", new Arg { key = "animalType", argType = "Text", value = "wolf" } },
            { "statistics:MultiOption", new Arg { key = "statistics", argType = "MultiOption", value = new[] { "AC" } } },
        };
    }

    #endregion

    #region Rust Monster

    public sealed class RustMonster_IronScent : TraitTestData
    {
        public override string Trait => "Iron Scent";

        public override string MonsterTraitString =>
            "The rust monster can pinpoint, by scent, the location of ferrous metal within 30 feet of it.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The rust monster" } },
            { "range:Number", new Arg { key = "range", argType = "Number", value = 30 } },
        };
    }

    public sealed class RustMonster_RustMetal : TraitTestData
    {
        public override string Trait => "Rust Metal";

        public override string MonsterTraitString =>
            "Any nonmagical weapon made of metal that hits the rust monster corrodes. After dealing damage, " +
            "the weapon takes a permanent and cumulative −1 penalty to damage rolls. If its penalty drops " +
            "to −5, the weapon is destroyed. Nonmagical ammunition made of metal that hits the rust monster " +
            "is destroyed after dealing damage.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the rust monster" } },
        };
    }

    #endregion

    #region Sahuagin

    public sealed class Sahuagin_BloodFrenzy : TraitTestData
    {
        public override string Trait => "Blood Frenzy";

        public override string MonsterTraitString =>
            "The sahuagin has advantage on melee attack rolls against any creature that doesn’t " +
            "have all its hit points.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The sahuagin" } },
        };
    }

    public sealed class Sahuagin_LimitedAmphibiousness : TraitTestData
    {
        public override string Trait => "Limited Amphibiousness";

        public override string MonsterTraitString =>
            "The sahuagin can breathe air and water, but it needs to be submerged at least once " +
            "every 4 hours to avoid suffocating.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The sahuagin" } },
            { "hours:Number", new Arg { key = "hours", argType = "Number", value = 4 } },
        };
    }

    public sealed class Sahuagin_SharkTelepathy : TraitTestData
    {
        public override string Trait => "Shark Telepathy";

        public override string MonsterTraitString =>
            "The sahuagin can magically command any shark within 120 feet of it, using a limited telepathy.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The sahuagin" } },
            { "range:Number", new Arg { key = "range", argType = "Number", value = 120 } },
        };
    }

    #endregion

    #region Shadow

    public sealed class Shadow_Amorphous : TraitTestData
    {
        public override string Trait => "Amorphous";

        public override string MonsterTraitString =>
            "The shadow can move through a space as narrow as 1 inch wide without squeezing.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The shadow" } },
        };
    }

    public sealed class Shadow_ShadowStealth : TraitTestData
    {
        public override string Trait => "Shadow Stealth";

        public override string MonsterTraitString =>
            "While in dim light or darkness, the shadow can take the Hide action as a bonus action.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the shadow" } },
        };
    }

    public sealed class Shadow_SunlightWeakness : TraitTestData
    {
        public override string Trait => "Sunlight Weakness";

        public override string MonsterTraitString =>
            "While in sunlight, the shadow has disadvantage on attack rolls, ability checks, and saving throws.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the shadow" } },
        };
    }

    #endregion

    #region Will-o'-Wisp

    public sealed class WillOWisp_ConsumeLife : TraitTestData
    {
        public override string Trait => "Consume Life";

        public override string MonsterTraitString => 
            "As a bonus action, the will-o’-wisp can target one creature it can see within 5 feet of it " +
            "that has 0 hit points and is still alive. The target must succeed on a DC 10 Constitution " +
            "saving throw against this magic or die. If the target dies, the will-o’-wisp regains " +
            "10 (3d6) hit points.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the will-o’-wisp" } },
            { "save:SavingThrow", new Arg { key = "save", argType = "SavingThrow", value = new SavingThrowArgs
                {
                    DC = 10,
                    Attribute = "Constitution",
                } } },
            { "amount:DiceRoll", new Arg { key = "amount", argType = "DiceRoll", value = new DiceRollArgs
                {
                    diceCount = 3,
                    dieSize = 6,
                } } },
        };
    }

    public sealed class WillOWisp_Ephemeral : TraitTestData
    {
        public override string Trait => "Ephemeral";

        public override string MonsterTraitString =>
            "The will-o’-wisp can’t wear or carry anything.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The will-o’-wisp" } },
        };
    }

    public sealed class WillOWisp_IncorporealMovement : TraitTestData
    {
        public override string Trait => "Incorporeal Movement";

        public override string MonsterTraitString =>
            "The will-o’-wisp can move through other creatures and objects as if they were difficult " +
            "terrain. It takes 5 (1d10) force damage if it ends its turn inside an object.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The will-o’-wisp" } },
            { "damage:Damage:Typed", new Arg { key = "damage", argType = "Damage", flags = new[] { "Typed" }, value = new TypedDamageArgs
                {
                    diceCount = 1,
                    dieSize = 10,
                    damageType = "force",
                } } },
        };
    }

    public sealed class WillOWisp_VariableIllumination : TraitTestData
    {
        public override string Trait => "Variable Illumination";

        public override string MonsterTraitString =>
            "The will-o’-wisp sheds bright light in a 5- to 20-foot radius and dim light for an " +
            "additional number of feet equal to the chosen radius. The will-o’-wisp can alter the " +
            "radius as a bonus action.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The will-o’-wisp" } },
            { "min:Number", new Arg { key = "min", argType = "Number", value = 5 } },
            { "max:Number", new Arg { key = "max", argType = "Number", value = 20 } },
        };
    }

    #endregion

    #region Xorn

    public sealed class Xorn_EarthGlide : TraitTestData
    {
        public override string Trait => "Earth Glide";

        public override string MonsterTraitString =>
            "The xorn can burrow through nonmagical, unworked earth and stone. While doing so, the xorn " +
            "doesn’t disturb the material it moves through.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The xorn" } },
            { "shortName", new Arg { key = "shortName", argType = "Inherent", value = "the xorn" } },
        };
    }

    public sealed class Xorn_StoneCamouflage : TraitTestData
    {
        public override string Trait => "Stone Camouflage";

        public override string MonsterTraitString =>
            "The xorn has advantage on Dexterity (Stealth) checks made to hide in rocky terrain.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The xorn" } },
        };
    }

    public sealed class Xorn_TreasureSense : TraitTestData
    {
        public override string Trait => "Treasure Sense";

        public override string MonsterTraitString =>
            "The xorn can pinpoint, by scent, the location of precious metals and stones, such as " +
            "coins and gems, within 60 feet of it.";

        public override Dictionary<string, Arg> ExpectedArgsOutput => new Dictionary<string, Arg>
        {
            { "ShortName", new Arg { key = "ShortName", argType = "Inherent", value = "The xorn" } },
            { "range:Number", new Arg { key = "range", argType = "Number", value = 60 } },
        };
    }

    #endregion
}
