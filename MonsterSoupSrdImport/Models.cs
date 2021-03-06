﻿using System.Collections.Generic;

namespace MonsterSoupSrdImport
{

    public class Monster
    {
        public string WhatsLeft { get; set; }

        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Alignment { get; set; }
        public string AC { get; set; }
        public string ACReason { get; set; }
        public int[] Attributes { get; set; }
        public string HP { get; set; }
        public string HDice { get; set; }
        public string HDiceBonus { get; set; }
        public Skill[] Speed { get; set; }
        public Skill[] Skills { get; set; }
        
        public Skill[] Senses { get; set; }
        public string[] Languages { get; set; }
        public Skill[] Saves { get; set; }
        public string[] DamageImmunities { get; set; }
        public string[] DamageResistances { get; set; }
        public string[] DamageVulnerabilities { get; set; }
        public string[] ConditionImmunities { get; set; }

        public string CR { get; set; }

        public InnateSpellcasting InnateSpellcasting { get; set; }
        public Spellcasting Spellcasting { get; set; }

        public NormalAction[] Actions { get; set; }
        public NormalAction[] Reactions { get; set; }

        public MonsterTrait[] Traits { get; set; }
        public Dictionary<string, string> StandardReplaces { get; set; }

        public int LegendaryResistances { get; set; }
        public int LegendaryActionCount { get; set; }
        public LegendaryAction[] LegendaryActions { get; set; }
    }

    public struct Skill
    {
        public string Name { get; set; }
        public string Modifier { get; set; }
    }

    public struct LegendaryAction
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
    }

    public struct NormalAction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Uses { get; set; }
        public string UseTimeframe { get; set; }
        public string RechargeRange { get; set; }
    }

    public class InnateSpellcasting
    {
        public string Ability { get; set; }
        public int? DC { get; set; }
        public int? AttackBonus { get; set; }
        public RequiredComponents RequiredComponents { get; set; }
        public string[] AtWill { get; set; }
        public string[] OncePerDay { get; set; }
        public string[] ThricePerDay { get; set; }
    }

    public enum RequiredComponents
    {
        None,
        All,
        NoMaterial,
        OnlyVerbal,
    }

    public class Spellcasting
    {
        public int? CasterLevel { get; set; }
        public string Ability { get; set; }
        public int? DC { get; set; }
        public int? AttackBonus { get; set; }
        public string ClassList { get; set; }
        public SpellsAndSlotsByLevel[] SpellsAndSlotsByLevel { get; set; }

    }

    public struct SpellsAndSlotsByLevel
    {
        public int Level { get; set; }
        public int Slots { get; set; }
        public string[] Spells { get; set; }
    }

    public class Trait
    {
        public string Name { get; set; }
        public string Template { get; set; }
    }

    public class MonsterTrait
    {
        public string Name { get; set; }
        public Dictionary<string, object> Replaces { get; set; }
    }
}
