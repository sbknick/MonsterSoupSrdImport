using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    class MonsterMDParser
    {
        public Monster ParseMDContent(string name, string content) => ParseMonsterMatch(name, content);

        private Monster ParseMonsterMatch(string name, string statBlock)
        {
            statBlock = statBlock.Trim();

            var firstLine = statBlock.PopToken(PopType.Newline, out statBlock).Replace("*", string.Empty);

            var monster = new Monster
            {
                Name = name.Trim(),
                Size = firstLine.PopToken(PopType.Space, out firstLine),
                Type = firstLine.PopToken(PopType.Comma, out firstLine),
                Alignment = firstLine.Trim(),
            };

            Extract.AC(monster, ref statBlock);
            Extract.HPAndDice(monster, ref statBlock);
            Extract.Speed(monster, ref statBlock);
            Extract.Skills(monster, ref statBlock);
            Extract.Senses(monster, ref statBlock);
            Extract.Languages(monster, ref statBlock);
            Extract.Saves(monster, ref statBlock);
            Extract.DamageImmunities(monster, ref statBlock);
            Extract.DamageResistances(monster, ref statBlock);
            Extract.DamageVulnerabilities(monster, ref statBlock);
            Extract.ConditionImmunities(monster, ref statBlock);

            Extract.Challenge(monster, ref statBlock);
            Extract.LegendaryResistances(monster, ref statBlock);

            Extract.Attributes(monster, ref statBlock);

            Extract.LegendaryActions(monster, ref statBlock);
            Extract.Reactions(monster, ref statBlock);
            Extract.Actions(monster, ref statBlock);

            Extract.InnateSpellcasting(monster, ref statBlock);
            Extract.Spellcasting(monster, ref statBlock);

            monster.WhatsLeft = statBlock;
            return monster;
        }

        private static class Extract
        {
            // Simple Extracts

            public static void AC(Monster monster, ref string statBlock) => ExtractLine(monster, AC_Regex, ParseAC, ref statBlock);
            public static void HPAndDice(Monster monster, ref string statBlock) => ExtractLine(monster, HP_Regex, ParseHPAndDice, ref statBlock);
            public static void Speed(Monster monster, ref string statBlock) => ExtractLine(monster, Spd_Regex, ParseSpeed, ref statBlock);
            public static void Skills(Monster monster, ref string statBlock) => ExtractLine(monster, Skl_Regex, ParseSkills, ref statBlock);
            public static void Senses(Monster monster, ref string statBlock) => ExtractLine(monster, Sns_Regex, ParseSenses, ref statBlock);
            public static void Languages(Monster monster, ref string statBlock) => ExtractLine(monster, Lng_Regex, ParseLanguages, ref statBlock);
            public static void Saves(Monster monster, ref string statBlock) => ExtractLine(monster, Svs_Regex, ParseSaves, ref statBlock);
            public static void DamageImmunities(Monster monster, ref string statBlock) => ExtractLine(monster, DmgI_Regex, ParseDamageImmunities, ref statBlock);
            public static void DamageResistances(Monster monster, ref string statBlock) => ExtractLine(monster, DmgR_Regex, ParseDamageResistances, ref statBlock);
            public static void DamageVulnerabilities(Monster monster, ref string statBlock) => ExtractLine(monster, DmgV_Regex, ParseDamageVulnerabilities, ref statBlock);
            public static void ConditionImmunities(Monster monster, ref string statBlock) => ExtractLine(monster, CndI_Regex, ParseConditionImmunities, ref statBlock);

            public static void Challenge(Monster monster, ref string statBlock) => ExtractLine(monster, CR_Regex, ParseCR, ref statBlock);
            public static void LegendaryResistances(Monster monster, ref string statBlock) => ExtractFullLine(monster, LegRes_Regex, ParseLegRes, ref statBlock);

            // Bespoke Extracts

            public static void Attributes(Monster monster, ref string statBlock)
            {
                var tokz = Attr_Regex.Match(statBlock);

                if (tokz.Success)
                {
                    var attrsMatches = new Regex(@"(\d+) \(.*?\)").Matches(tokz.Groups[0].Value);

                    monster.Attributes = new int[6];
                    int i = 0;
                    foreach (Match attrMatch in attrsMatches)
                    {
                        monster.Attributes[i++] = Convert.ToInt32(attrMatch.Groups[1].Value);
                    }

                    statBlock = Attr_Regex.Strip(statBlock);
                }
            }

            // Compound/Section Extracts

            public static void LegendaryActions(Monster monster, ref string statBlock)
            {
                var match = LegAct_Section_Regex.Match(statBlock);

                if (match.Success)
                {
                    var nActionsMatch = new Regex(@"take ([\d]) legendary actions").Match(match.Groups[1].Value);

                    monster.LegendaryActionCount = Convert.ToInt32(nActionsMatch.Groups[1].Value);

                    var actionsMatch = actionSection_Regex.Matches(match.Groups[1].Value);

                    var legActs = new List<LegendaryAction>();

                    foreach (Match action in actionsMatch)
                    {
                        var legAct = new LegendaryAction
                        {
                            Name = action.Groups[1].Value,
                            Cost = 1,
                            Description = action.Groups[2].Value,
                        };

                        var costMatch = new Regex(@"(.*?) \(Costs (.*) Action(?:s)?\)").Match(action.Groups[1].Value);

                        if (costMatch.Success)
                        {
                            legAct.Name = costMatch.Groups[1].Value;
                            legAct.Cost = Convert.ToInt32(costMatch.Groups[2].Value);
                        }

                        legActs.Add(legAct);
                    }
                    monster.LegendaryActions = legActs.ToArray();

                    statBlock = LegAct_Section_Regex.Strip(statBlock);
                }
            }

            public static void Reactions(Monster monster, ref string statBlock)
            {
                DoMatch(React_Section_Regex, ref statBlock, match =>
                {
                    var reactionsMatch = actionSection_Regex.Matches(match.Groups[1].Value);

                    var reacts = new List<NormalAction>();

                    foreach (Match reaction in reactionsMatch)
                    {
                        var react = new NormalAction
                        {
                            Name = reaction.Groups[1].Value,
                            Description = reaction.Groups[2].Value,
                        };

                        reacts.Add(react);
                    }

                    monster.Reactions = reacts.ToArray();
                });
            }

            public static void Actions(Monster monster, ref string statBlock)
            {
                var match = Act_Section_Regex.Match(statBlock);

                if (match.Success)
                {
                    var actionsMatch = actionSection_Regex.Matches(match.Groups[1].Value);

                    var acts = new List<NormalAction>();

                    foreach (Match action in actionsMatch)
                    {
                        var act = new NormalAction
                        {
                            Name = action.Groups[1].Value,
                            Description = action.Groups[2].Value,
                        };

                        var xPerDayMatch = new Regex(@"([\s\S]+) \((\d+)/Day\)").Match(act.Name);
                        if (xPerDayMatch.Success)
                        {
                            act.Name = xPerDayMatch.Groups[1].Value;
                            act.Uses = Convert.ToInt32(xPerDayMatch.Groups[2].Value);
                            act.UseTimeframe = "Day";
                        }

                        var rechargeMatch = new Regex(@"([\s\S]+) \(Recharge ([\d-–]+)\)").Match(act.Name);
                        if (rechargeMatch.Success)
                        {
                            act.Name = rechargeMatch.Groups[1].Value;
                            act.RechargeRange = rechargeMatch.Groups[2].Value;
                        }

                        acts.Add(act);
                    }
                    monster.Actions = acts.ToArray();

                    statBlock = Act_Section_Regex.Strip(statBlock);
                }
            }

            public static void InnateSpellcasting(Monster monster, ref string statBlock)
            {
                var matchSuccess = DoMatch(InnSpell_Section_Regex, ref statBlock, match =>
                {
                    var spellsString = match.Groups[1].Value;
                    var abilityAndDC = new Regex(@".*spellcasting ability is (\S+) \(spell save DC (\d+)(?:\)|(?:, (\S+)?))", RegexOptions.IgnoreCase).Match(spellsString);

                    var noComponents = new Regex(@"requiring no components").Match(spellsString);
                    var onlyVerbalComponents = new Regex(@"requiring only verbal components").Match(spellsString);
                    var noMaterialComponenents = new Regex(@"requiring no material components").Match(spellsString);

                    if (!abilityAndDC.Success)
                    {
                        monster.InnateSpellcasting = new InnateSpellcasting
                        {
                            Ability = new Regex(@"spellcasting ability is (\S+)\.").Match(spellsString).Groups[1].Value,
                        };
                    }
                    else
                    {

                        monster.InnateSpellcasting = new InnateSpellcasting
                        {
                            Ability = abilityAndDC.Groups[1].Value,
                            DC = abilityAndDC.Groups[2].Value.ToInt(),
                            AttackBonus = string.IsNullOrEmpty(abilityAndDC.Groups[3].Value) ? (int?)null : abilityAndDC.Groups[3].Value.ToInt(),
                        };
                    }

                    monster.InnateSpellcasting.RequiredComponents =
                        noComponents.Success ? RequiredComponents.None :
                        onlyVerbalComponents.Success ? RequiredComponents.OnlyVerbal :
                        noMaterialComponenents.Success ? RequiredComponents.NoMaterial :
                                                            RequiredComponents.All;


                    monster.InnateSpellcasting.OncePerDay = SpellMatch(@"1/day each: ([\s\S]+)", ref spellsString);
                    monster.InnateSpellcasting.ThricePerDay = SpellMatch(@"3/day each: ([\s\S]+)", ref spellsString);
                    monster.InnateSpellcasting.AtWill = SpellMatch(@"at will: ([\s\S]+)", ref spellsString);
                });

                if (matchSuccess) return;

                DoMatch(InnSpellMephit_Regex, ref statBlock, match =>
                {
                    var spellAndAbilityMatch = new Regex(@".* \*([\s\S]+?)\*.*ability is (\S+)\.").Match(match.Groups[2].Value);
                    var dcMatch = new Regex(@"\(spell save DC (\d+)\)").Match(match.Groups[2].Value);
                    var noMaterialComponents = new Regex(@"requiring no material components").Match(match.Groups[2].Value);

                    monster.InnateSpellcasting = new InnateSpellcasting
                    {
                        Ability = spellAndAbilityMatch.Groups[2].Value,
                        DC = dcMatch.Success ? dcMatch.Groups[1].Value.ToInt() : (int?)null,
                    };
                    monster.InnateSpellcasting.RequiredComponents =
                        noMaterialComponents.Success ? RequiredComponents.NoMaterial :
                                                       RequiredComponents.All;

                    var spellArray = new[] { spellAndAbilityMatch.Groups[1].Value };
                    var countPerDay = match.Groups[1].Value.ToInt();

                    switch (countPerDay)
                    {
                        case 1:
                            monster.InnateSpellcasting.OncePerDay = spellArray;
                            break;

                        case 3:
                            monster.InnateSpellcasting.ThricePerDay = spellArray;
                            break;

                        default:
                            throw new Exception("WHAAAA");
                    }
                });
            }

            private static string[] SpellMatch(string regexString, ref string spellString)
            {
                var regex = new Regex(regexString, RegexOptions.IgnoreCase);
                var spellMatch = regex.Match(spellString);
                if (spellMatch.Success)
                {
                    var spells = spellMatch.Groups[1].Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Replace("*", string.Empty).Trim());

                    spellString = regex.Strip(spellString);

                    return spells.ToArray();
                }

                return null;
            }

            public static void Spellcasting(Monster monster, ref string statBlock)
            {
                var match = Spell_Section_Regex.Match(statBlock);

                if (match.Success)
                {
                    var spellsString = match.Groups[1].Value;
                    var deetsReg = new Regex(@".* is an? (\d+).*spellcasting ability is (\S+) \(spell save DC (\d+), (\S+) to hit .* following (\S+) spells.*", RegexOptions.IgnoreCase);
                    var deets = deetsReg.Match(spellsString);
                    spellsString = deetsReg.Strip(spellsString);

                    monster.Spellcasting = new Spellcasting
                    {
                        CasterLevel = deets.Groups[1].Value.ToInt(),
                        Ability = deets.Groups[2].Value,
                        DC = deets.Groups[3].Value.ToInt(),
                        AttackBonus = deets.Groups[4].Value.ToInt(),
                        ClassList = deets.Groups[5].Value,
                    };

                    var spellLists = new Regex(@"(?:[^\*]).*?: .*?(?=$|[\d\r\n])").Matches(spellsString);

                    monster.Spellcasting.SpellsAndSlotsByLevel = new SpellsAndSlotsByLevel[spellLists.Count];

                    foreach (Match list in spellLists)
                    {
                        var levelMatch = new Regex(@"(\d).*\((\d) slots?\):").Match(list.Value);
                        //var spellsMatch = new Regex(@": ([\s\S]+)").Match(list.Value);

                        var spellStr = list.Value;
                        var levelItem = new SpellsAndSlotsByLevel
                        {
                            Level = levelMatch.Success ? levelMatch.Groups[1].Value.ToInt() : 0,
                            Slots = levelMatch.Success ? levelMatch.Groups[2].Value.ToInt() : 0,
                            Spells = SpellMatch(@": ([\s\S]+)", ref spellStr),
                        };

                        monster.Spellcasting.SpellsAndSlotsByLevel[levelItem.Level] = levelItem;
                    }

                    statBlock = Spell_Section_Regex.Strip(statBlock);
                }
            }

            #region Private Extracts

            private static string PropertyLine(string propertyName) => $@"\*\*{propertyName}\*\* (.*)";

            private static Regex actionSection_Regex = new Regex(@"\*+(.*?)\.\*+ (.*?)(?=[\r\n]+|$)");

            private static readonly Regex AC_Regex = new Regex(PropertyLine("Armor Class"));
            private static readonly Regex HP_Regex = new Regex(PropertyLine("Hit Points"));
            private static readonly Regex Spd_Regex = new Regex(PropertyLine("Speed"));
            private static readonly Regex Skl_Regex = new Regex(PropertyLine("Skills"));
            private static readonly Regex Sns_Regex = new Regex(PropertyLine("Senses"));
            private static readonly Regex Lng_Regex = new Regex(PropertyLine("Languages"));
            private static readonly Regex Svs_Regex = new Regex(PropertyLine("Saving Throws"));
            private static readonly Regex DmgI_Regex = new Regex(PropertyLine("Damage Immunities"));
            private static readonly Regex DmgR_Regex = new Regex(PropertyLine("Damage Resistances"));
            private static readonly Regex DmgV_Regex = new Regex(PropertyLine("Damage Vulnerabilities"));
            private static readonly Regex CndI_Regex = new Regex(PropertyLine("Condition Immunities"));
            private static readonly Regex CR_Regex = new Regex(PropertyLine("Challenge"));
            private static readonly Regex LegRes_Regex = new Regex(PropertyLine("Legendary Resistance.*?"));
            private static readonly Regex Attr_Regex = new Regex(@"STR *\| DEX *\| CON[\s\S]*?(?=\*|Actions)");
            private static readonly Regex LegAct_Section_Regex = new Regex(@"\*+Legendary Actions\*+([\s\S]*)");
            private static readonly Regex React_Section_Regex = new Regex(@"\*+Reactions\*+([\s\S]*)");
            private static readonly Regex Act_Section_Regex = new Regex(@"\*+Actions\*+([\s\S]*)");
            private static readonly Regex InnSpellMephit_Regex = new Regex(@"\*+Innate Spellcasting \((\d+)/Day\)\.\*+ (.*)");
            private static readonly Regex InnSpell_Section_Regex = new Regex(@"\*+Innate Spellcasting\.\*+ ([\s\S]+?)(?=\*\*|$)");
            private static readonly Regex Spell_Section_Regex = new Regex(@"\*+Spellcasting\.\*+ ([\s\S]+?)(?=\*\*|$)");

            private static bool DoMatch(Regex regex, ref string statBlock, Action<Match> action)
            {
                var match = regex.Match(statBlock);

                if (match.Success)
                {
                    action(match);

                    statBlock = regex.Strip(statBlock);
                }

                return match.Success;
            }

            private static void ExtractLine(Monster monster, Regex regex, Action<Monster, string> parser, ref string input)
            {
                var match = regex.Match(input);

                if (match.Success)
                {
                    parser(monster, match.Groups[1].Value);

                    input = regex.Strip(input);
                }
            }

            private static void ExtractFullLine(Monster monster, Regex regex, Action<Monster, string> parser, ref string input)
            {
                var match = regex.Match(input);

                if (match.Success)
                {
                    parser(monster, match.Groups[0].Value);

                    input = regex.Strip(input);
                }
            }

            #endregion

            #region Parse

            private static readonly char[] lineParse1 =
            {
                '*', '(', ')'
            };

            private static void ParseAC(Monster monster, string statBlockLine)
            {
                var tokz = statBlockLine.Trim().Split(lineParse1, StringSplitOptions.RemoveEmptyEntries);

                monster.AC = tokz[0].Trim();
                monster.ACReason = tokz.Length > 1 ? tokz[1].Trim() : null;
            }

            private static void ParseHPAndDice(Monster monster, string statBlockLine)
            {
                var tokz = statBlockLine.Split(lineParse1, StringSplitOptions.RemoveEmptyEntries);

                monster.HP = tokz[0].Trim();

                var tok2 = tokz[1].Split(new[] { ' ', '+' }, StringSplitOptions.RemoveEmptyEntries);

                monster.HDice = tok2[0].Trim();
                monster.HDiceBonus = tok2.Length > 1 ? tok2[1].Trim() : null;
            }

            private static void ParseSpeed(Monster monster, string statBlockLine)
            {
                var tokz = statBlockLine.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

                monster.Speed = tokz.Select(t => t.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    .Select(ts =>
                    {
                        if (ts.Length == 2)
                            return new Skill
                            {
                                Name = "walk",
                                Modifier = ts[0],
                            };
                        else
                            return new Skill
                            {
                                Name = ts[0],
                                Modifier = ts[1],
                            };
                    }).ToArray();
            }

            private static void ParseSkills(Monster monster, string statBlockLine)
                => monster.Skills = DoParseModList(statBlockLine);

            private static void ParseSenses(Monster Monster, string statBlockLine)
            {
                var tokz = statBlockLine.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

                Monster.Senses = tokz.Where(t => !t.Contains("passive"))
                    .Select(t => t.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    .Select(ts => new Skill
                    {
                        Name = ts[0],
                        Modifier = ts[1],
                    })
                    .Union(tokz.Where(t => t.Contains("passive"))
                        .Select(t => t.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        .Select(ts => new Skill
                        {
                            Name = "passive Perception",
                            Modifier = ts[2]
                        })
                    ).ToArray();
            }

            private static void ParseLanguages(Monster monster, string statBlockLine)
                => monster.Languages = DoParseListSeparatedBy(',', statBlockLine);

            private static void ParseSaves(Monster monster, string statBlockLine)
                => monster.Saves = DoParseModList(statBlockLine);

            private static void ParseDamageImmunities(Monster monster, string statBlockLine)
                => monster.DamageImmunities = DoParseDamTypeList_NonmagicalAttacks(statBlockLine);

            private static void ParseDamageResistances(Monster monster, string statBlockLine)
                => monster.DamageResistances = DoParseDamTypeList_NonmagicalAttacks(statBlockLine);

            private static void ParseDamageVulnerabilities(Monster monster, string statBlockLine)
                => monster.DamageVulnerabilities = DoParseDamTypeList_MagicWeapons(statBlockLine);

            private static void ParseConditionImmunities(Monster monster, string statBlockLine)
                => monster.ConditionImmunities = DoParseListSeparatedBy(',', statBlockLine);



            private static void ParseCR(Monster monster, string statBlockLine)
            {
                var tokz = statBlockLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                monster.CR = tokz.First();
            }

            private static void ParseLegRes(Monster monster, string statBlockLine)
            {
                var legResMatch = new Regex(@"\*+Legendary Resistances? \((\d)/Day\)\.\*+").Match(statBlockLine);

                monster.LegendaryResistances = legResMatch.Groups[1].Value.ToInt();
            }

            private static Skill[] DoParseModList(string statBlockLine)
            {
                var tokz = statBlockLine.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim());

                return tokz.Select(t => t.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    .Select(ts => new Skill
                    {
                        Name = ts[0],
                        Modifier = ts[1],
                    }).ToArray();
            }

            private static string[] DoParseListSeparatedBy(char @char, string statBlockLine)
            {
                return statBlockLine.Trim().Split(new[] { @char }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();
            }

            private static string[] DoParseDamTypeList_NonmagicalAttacks(string statBlockLine)
                => DoTheDamTypeThing("nonmagical attacks", statBlockLine);

            private static string[] DoParseDamTypeList_MagicWeapons(string statBlockLine)
                => DoTheDamTypeThing("magic weapons", statBlockLine);

            private static string[] DoTheDamTypeThing(string determineStringThing, string statBlockLine)
            {
                var tokz1 = statBlockLine.Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

                if (tokz1.Length == 1)
                {
                    if (tokz1.Contains(determineStringThing))
                        return tokz1;
                    else
                        return tokz1[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();
                }

                var tokz2 = tokz1[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();

                tokz2.Add(tokz1[1]);

                return tokz2.ToArray();
            }

            #endregion
        }
    }
}
