using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MonsterSoupSrdImport
{
    public interface IArgExtractor
    {
        Dictionary<string, Arg> ExtractArgs(string traitTemplate, string monsterTraitString);
    }

    public class ArgExtractor : IArgExtractor
    {
        public Dictionary<string, Arg> ExtractArgs(string traitTemplate, string monsterTraitString)
        {
            traitTemplate = traitTemplate.NormalizeNewlines();
            monsterTraitString = monsterTraitString.NormalizeNewlines();

            var perm = GetTemplateConditionalPermutation(traitTemplate, monsterTraitString);
            var conditionalArgs = perm.Args?.ToDictionary(kvp => kvp.ArgKey, kvp => kvp.Arg) ?? new Dictionary<string, Arg>();
            
            var argsFromTemplate = GetArgsFromTemplate(perm.Template, monsterTraitString);
            var transformedArgs = TransformComplexMonsterTraits(argsFromTemplate);

            foreach (var cArg in conditionalArgs)
                transformedArgs[cArg.Key] = cArg.Value;

            return transformedArgs;
        }

        private TemplateConditionalPermutation GetTemplateConditionalPermutation(string template, string monsterTraitString)
        {
            //var perms = GetConditionalPermutations(template);
            var perms = GetConditionalPermutationsNew(template);

            if (perms == null)
            {
                return new TemplateConditionalPermutation
                {
                    Template = template,
                };
            }

            // TEST FOR WHICH PERMUTATION IS APPROPRIATE PARA ESTA MONSTERA //
            foreach (var perm in perms)
            {
                var captureString = GetCaptureString(perm.Template);
                var usesCondition = new Regex(captureString).Match(monsterTraitString).Success;

                if (usesCondition)
                    return perm;
            }

            throw new Exception();
        }


        private class PermutationNode
        {
            public (string key, string value, bool isEqual) Condition;
            public string[] NonConditionalText;
            public List<PermutationNode> Children = new List<PermutationNode>();
        }

        private IList<TemplateConditionalPermutation> GetConditionalPermutationsNew(string template)
        {
            var ToplevelConditionalsRegex = new Regex(@"\[([a-zA-Z]+?)(=|!=)(\S+?) (?:[^\[\]]|(?<counter>\[)|(?<-counter>\]))+(?(counter)(?!))\]");
            var ConditionsRegex = new Regex(@"^\[([a-zA-Z]+?)(=|!=)(\S+?) ([\s\S]+)\]$");

            PermutationNode buildNode((string key, string value, bool isEqual) condition, string templateSegment)
            {
                var conditionalMatches = ToplevelConditionalsRegex.Matches(templateSegment);

                IList<string> nonConditionalBits = new List<string>();

                if (conditionalMatches.Count == 0)
                    nonConditionalBits.Add(templateSegment);
                else
                {
                    for (int i = conditionalMatches.Count - 1; i >= 0; i--)
                    {
                        var match = conditionalMatches[i];
                        nonConditionalBits.Add(templateSegment.Substring(match.Index + match.Length));
                        templateSegment = templateSegment.Substring(0, match.Index);
                    }
                    nonConditionalBits.Add(templateSegment);
                }

                var rootNode = new PermutationNode
                {
                    Condition = condition,
                    NonConditionalText = nonConditionalBits.Reverse().ToArray(),
                    Children = conditionalMatches.Cast<Match>().Select(m =>
                    {
                        var conditionDetailMatch = ConditionsRegex.Match(m.Groups[0].Value);

                        if (conditionDetailMatch.Success)
                        {
                            return buildNode((
                                conditionDetailMatch.Groups[1].Value, 
                                conditionDetailMatch.Groups[3].Value,
                                conditionDetailMatch.Groups[2].Value == "="),
                                conditionDetailMatch.Groups[4].Value
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }).Where(n => n != null).ToList(),
                };

                return rootNode;
            }

            var permutationTree = buildNode(default((string, string, bool)), template);

            var YesNoArgsRegex = new Regex(@"\{(([^\{]+?):YesNo:?(.*?))\}");
            var DropdownArgsRegex = new Regex(@"\{(([^\{]+?):Dropdown:\[(.*?)\]:?(.*?))\}");
            var ConditionalArgsRegex = new Regex(@"\{(?<fullTag>(?<key>[^\{]+?):(?<argType>YesNo|Dropdown:\[(?<values>.*?)\]):?(?<flags>.*?))\}");

            List<List<(string, string)>> permutations = new List<List<(string, string)>>();
            Stack<(string, string)> tempPerm = new Stack<(string, string)>();

            void buildPermutationOptions(PermutationNode permutationNode)
            {
                if (permutationNode.Children.Count == 0)
                {

                }

                var conditionalArgMatches = permutationNode.NonConditionalText.SelectMany(t =>
                    ConditionalArgsRegex.Matches(t).Cast<Match>()).ToList();

                if (conditionalArgMatches.Count == 0)
                {
                    permutations.Add(new List<(string, string)>(tempPerm));
                    return;
                }



                foreach (Match match in conditionalArgMatches)
                {
                    var argType = match.Groups["argType"].Value.Split(':')[0];

                    
                }

                void addCondition(List<Match> conditionsLeft)
                {

                }
            }

            IEnumerable<List<(string key, string value)>> permutationOptions2(PermutationNode permutationNode)
            {
                var perms = new List<(string, string)>();

                var yesNoArgMatches = permutationNode.NonConditionalText.SelectMany(t =>
                    YesNoArgsRegex.Matches(t).Cast<Match>()).ToList();
                var dropdownArgMatches = permutationNode.NonConditionalText.SelectMany(t =>
                    DropdownArgsRegex.Matches(t).Cast<Match>()).ToList();

                if (yesNoArgMatches.Count == 0 && dropdownArgMatches.Count == 0)
                    return null;
                
                var ddOptionsLookup = new Dictionary<string, string[]>();

                return buildPerms(yesNoArgMatches, dropdownArgMatches);

                IEnumerable<List<(string, string)>> buildPerms(List<Match> yesNoMatches, List<Match> dropdownMatches)
                {
                    if (yesNoMatches.Count > 0)
                    {
                        var thisMatch = yesNoMatches[0];
                        yesNoMatches = yesNoMatches.Skip(1).ToList();

                        var argKey = thisMatch.Groups[2].Value;

                        var permses = buildPerms(yesNoMatches, dropdownMatches);
                        foreach (var p in permses)
                        {
                            var options = new[] { "Yes", "No" };
                            foreach (var opt in options)
                            {
                                var list = new List<(string, string)> { (argKey, opt) };
                                list.AddRange(p);
                                yield return list;
                            }
                        }

                        //list.AddRange(buildPerms(yesNoMatches, dropdownMatches));
                        //yield return list;
                    }
                    else if (dropdownMatches.Count > 0)
                    {
                        var thisMatch = dropdownMatches[0];
                        dropdownMatches = dropdownMatches.Skip(1).ToList();

                        var argKey = thisMatch.Groups[2].Value;

                        var permses = buildPerms(yesNoMatches, dropdownMatches);
                        
                        var options = thisMatch.Groups[3].Value.Split(',');
                        foreach (var opt in options)
                        {
                            var thisOpt = (argKey, opt);
                            if (permses.Any())
                                foreach (var p in permses)
                                {
                                    var list = new List<(string, string)> { thisOpt };
                                    list.AddRange(p);
                                    yield return list;
                                }
                            else
                                yield return new List<(string, string)> { thisOpt };
                        }

                        //list.AddRange(buildPerms(yesNoMatches, dropdownMatches));
                        //yield return list;
                    }
                }

                int CountDropDownOptionPermuations()
                {
                    var count = 1;
                    foreach (Match match in dropdownArgMatches)
                    {
                        var options = match.Groups[3].Value.Split(',');
                        count *= options.Length;
                        ddOptionsLookup[match.Groups[2].Value] = options;
                    }
                    return count;
                }
            }
            
            var validPermutations = permutationOptions2(permutationTree);

            string buildOutput(PermutationNode permutationNode)
            {
                var sb = new StringBuilder();

                int i = 0;
                for (; i < permutationNode.Children.Count; i++)
                {
                    var child = permutationNode.Children[i];
                    sb.Append(permutationNode.NonConditionalText[i]);
                    sb.Append(buildOutput(child));
                }
                sb.Append(permutationNode.NonConditionalText[i]);

                return sb.ToString();
            }

            var str = buildOutput(permutationTree);

            foreach (var permutation in validPermutations)
            {

            }

            return null;
        }


        private IList<TemplateConditionalPermutation> GetConditionalPermutations(string template)
        {
            var ddOptionsLookup = new Dictionary<string, string[]>();
            var YesNoArgsRegex = new Regex(@"\{(([^\{]+?):YesNo:?(.*?))\}");
            var DropdownArgsRegex = new Regex(@"\{(([^\{]+?):Dropdown:\[(.*?)\]:?(.*?))\}");

            var yesNoArgMatches = YesNoArgsRegex.Matches(template);
            var dropdownArgMatches = DropdownArgsRegex.Matches(template);

            if (yesNoArgMatches.Count == 0 && dropdownArgMatches.Count == 0)
                return null;

            var permutationList = new List<(int specificity, TemplateConditionalPermutation tcp)>();

            int yesNoCount = yesNoArgMatches.Count;
            int ddCount = dropdownArgMatches.Count;

            int yesNoPermCount = (int)Math.Pow(2, yesNoCount);
            int ddPermCount = CountDropDownOptionPermuations();

            var yesNoPermutation = new bool[yesNoCount];
            var ddPermutation = new int[ddCount];

            for (int i = yesNoPermCount - 1; i >= 0; i--)
            {
                for (int j = 0; j < yesNoCount; j++)
                    yesNoPermutation[j] = (i & (1 << j)) != 0;
                
                for (int k = 0; k < ddPermCount; k++)
                {
                    int m = k;
                    for (int l = 0; l < ddCount; l++)
                    {
                        var key = dropdownArgMatches[l].Groups[2].Value;
                        var optCount = ddOptionsLookup[key].Length;
                        ddPermutation[l] = m % optCount;
                        m /= optCount;
                    }

                    permutationList.Add((
                        yesNoPermutation.Count(p => p) + ddPermutation.Sum(p => p),
                        BuildPermutation(yesNoPermutation, yesNoArgMatches, ddPermutation, dropdownArgMatches, ddOptionsLookup, template)
                    ));
                }
            }

            return permutationList.Select(p => p.tcp).ToList();


            int CountDropDownOptionPermuations()
            {
                var count = 1;
                foreach (Match match in dropdownArgMatches)
                {
                    var options = match.Groups[3].Value.Split(',');
                    count *= options.Length;
                    ddOptionsLookup[match.Groups[2].Value] = options;
                }
                return count;
            }
        }

        private struct TemplateConditionalPermutation
        {
            public string Template;
            public (string ArgKey, Arg Arg)[] Args;
        }

        private TemplateConditionalPermutation BuildPermutation(
            bool[] yesNoPermutation, MatchCollection yesNoArgMatches,
            int[] ddPermutation, MatchCollection ddArgMatches, IDictionary<string, string[]> ddOptionsLookup,
            string template
        )
        {
            var ConditionalStringsRegex = new Regex(@"\[([a-zA-Z]+?)(=|!=)(\S+?) (?:[^\[\]]|(?<counter>\[)|(?<-counter>\]))+(?(counter)(?!))\]");
            // matches top-level [] braces, EG the outer of any nested sets and any have no parent or child []s

            //var ConditionalStringsRegex = new Regex(@"\[([a-zA-Z]+?)(=|!=)(\S+?) (.*?)\]");

            var matchArgs = new List<(string key, Arg arg)>();


            string Replace(Match match, string insert = null) =>
                $"{template.Substring(0, match.Index)}{insert}{template.Substring(match.Index + match.Length)}";

            IEnumerable<Match> GetConditionalsForArg(string argName) =>
                ConditionalStringsRegex.Matches(template).Cast<Match>()
                .Where(m => m.Groups[1].Value == argName).OrderByDescending(m => m.Index);

            void DoTheThing<T>(
                string argType,
                T[] permutation,
                MatchCollection matches,
                Func<string, T, string> valueToString,
                Func<string, string, T> stringToValue
            )
            {
                for (int i = 0; i < permutation.Length; i++)
                {
                    T assumeArgValue = permutation[i];
                    var argMatch = matches[i];

                    var argName = argMatch.Groups[2].Value;

                    var flagIdx = argMatch.Groups.Count - 1;
                    var flags = argMatch.Groups[flagIdx].Value.SplitFlags();

                    var arg = new Arg
                    {
                        key = argName,
                        argType = argType,
                        flags = flags.Length > 0 ? flags : null,
                        value = valueToString(argName, assumeArgValue),
                    };

                    var conditionalsForArg = GetConditionalsForArg(argName);

                    foreach (var conditional in conditionalsForArg)
                    {
                        T condition = stringToValue(argName, conditional.Groups[3].Value);
                        
                        var isEqualityCheck = conditional.Groups[2].Value == "=";

                        if (condition.Equals(assumeArgValue) == isEqualityCheck)
                        {
                            // Replace Match with appropriate template string.
                            template = Replace(conditional, conditional.Groups[4].Value);
                        }
                        else
                        {
                            // Excise Match
                            template = Replace(conditional);
                        }
                    }

                    matchArgs.Add((argMatch.Groups[1].Value, arg));
                }
            }

            DoTheThing("YesNo", yesNoPermutation, yesNoArgMatches,
                (argName, value) => value ? "Yes" : "No",
                (argName, str) => str == "Yes");
            DoTheThing("Dropdown", ddPermutation, ddArgMatches,
                (argName, value) => ddOptionsLookup[argName][value],
                (argName, str) => ddOptionsLookup[argName].IndexOf(str));
            
            return new TemplateConditionalPermutation
            {
                Template = template,
                Args = matchArgs.ToArray(),
            };
        }


        private static readonly Regex SimpleArgsRegex = new Regex(@"{([a-zA-Z:]+?)}");
        private static readonly Regex AnyConditionalArgRegex = new Regex(@"{[a-zA-Z]+?:(?=YesNo|Dropdown):?(.*?)}");

        public Dictionary<string, string> GetArgsFromTemplate(string traitTemplate, string monsterTraitString)
        {
            var argLookup = new Dictionary<string, string>();

            // create only required keys (to keep ordering for conditionals
            var matches = SimpleArgsRegex.Matches(traitTemplate);

            foreach (Match match in matches)
                argLookup[match.Groups[1].Value] = null;


            // grab matches for non-conditional args
            traitTemplate = AnyConditionalArgRegex.Strip(traitTemplate);

            matches = SimpleArgsRegex.Matches(traitTemplate);

            var captureString = GetCaptureString(traitTemplate);
            var captures = new Regex(captureString).Match(monsterTraitString);

            for (int i = 0; i < matches.Count; i++)
            {
                var argKey = matches[i].Groups[1].Value;
                var argValue = captures.Groups[i + 1].Value;
                argLookup[argKey] = argValue;
            }

            return argLookup;
        }

        private string GetCaptureString(string template)
        {
            var escapedTemplate = template.Escape('.', '(', ')', '*');
            escapedTemplate = AnyConditionalArgRegex.Strip(escapedTemplate);
            var captureString = SimpleArgsRegex.Replace(escapedTemplate, @"([\s\S]*?)");

            return $"^{captureString}$";
        }

        private static readonly Dictionary<string, Func<string, string[], object>> _typedArgParserLookup = new Dictionary<string, Func<string, string[], object>>
        {
            { "Attack", ArgParser.ParseAttackArgValues },
            { "Damage", ArgParser.ParseDamageArgValues },
            { "DiceRoll", ArgParser.ParseDiceRollArgValues },
            { "MultiOption", ArgParser.ParseMultiOptionArgValues },
            { "Number", ArgParser.ParseNumberArgValue },
            { "SavingThrow", ArgParser.ParseSavingThrowArgValues },
            { "Text", ArgParser.ParseTextArgValue },
            { "YesNo", ArgParser.ParseTextArgValue },
            { "Dropdown", ArgParser.ParseDropdownValue },
        };

        public Dictionary<string, Arg> TransformComplexMonsterTraits(Dictionary<string, string> argsLookup)
        {
            return argsLookup.ToDictionary(kvp => kvp.Key,
                kvp =>
                {
                    var argKeyTokens = kvp.Key.Split(':');
                    return argKeyTokens.Length == 1 ? InherentArg(argKeyTokens[0], kvp.Value) : TypedArg(argKeyTokens, kvp.Value);
                });

            // translators

            Arg InherentArg(string argKey, string argValue)
            {
                return new Arg
                {
                    key = argKey,
                    argType = "Inherent",
                    value = argValue,
                };
            }

            Arg TypedArg(string[] argKeyTokens, string argValue)
            {
                var arg = new Arg { key = argKeyTokens[0], argType = argKeyTokens[1] };

                if (argKeyTokens.Length > 2)
                    arg.flags = argKeyTokens.Skip(2).ToArray();

                arg.value = _typedArgParserLookup[arg.argType](argValue, arg.flags ?? new string[0]);

                return arg;
            }
        }
        
        private static class ArgParser
        {
            #region Attack

            private static readonly Regex AttackWithStringRegex = new Regex(@"attack with its (\S+)");
            private static readonly Regex AttackStringRegex = new Regex(@"(\S+) attack");

            public static object ParseAttackArgValues(string values, string[] flags)
            {
                var withMatch = AttackWithStringRegex.Match(values);

                if (withMatch.Success)
                    return new AttackRefArgs
                    {
                        attack = withMatch.Groups[1].Value,
                        withIts = true,
                    };

                var notWithMatch = AttackStringRegex.Match(values);

                return new AttackRefArgs
                {
                    attack = notWithMatch.Groups[1].Value,
                };
            }

            #endregion Attack

            #region Damage

            private static readonly Regex DamageStringRegex = new Regex(@"\((\d+)d(\d+)\) ?(\S*?)? damage");
            private static readonly Regex DamageStringNoAverageRegex = new Regex(@"(\d+)d(\d+) ?(\S*?)? damage");

            public static object ParseDamageArgValues(string values, string[] flags)
            {
                var isTyped = flags.Contains("Typed");
                var hasNoAverage = flags.Contains("NoAverage");

                var matches = (hasNoAverage ? DamageStringNoAverageRegex : DamageStringRegex).Match(values);

                if (isTyped)
                    return new TypedDamageArgs
                    {
                        diceCount = matches.Groups[1].Value.ToInt(),
                        dieSize = matches.Groups[2].Value.ToInt(),
                        damageType = matches.Groups[3].Value,
                    };

                return new DamageArgs
                {
                    diceCount = matches.Groups[1].Value.ToInt(),
                    dieSize = matches.Groups[2].Value.ToInt(),
                };
            }

            #endregion Damage

            #region DiceRoll

            private static readonly Regex DiceRollRegex = new Regex(@"(\d+)d(\d+)");

            public static object ParseDiceRollArgValues(string values, string[] flags)
            {
                var matches = DiceRollRegex.Match(values);

                return new DiceRollArgs
                {
                    diceCount = matches.Groups[1].Value.ToInt(),
                    dieSize = matches.Groups[2].Value.ToInt(),
                };
            }

            #endregion DiceRoll

            #region MultiOption

            private static readonly Regex MultiOptionRegex = new Regex(@"(.*?)(?:$| and (.*))");

            public static object ParseMultiOptionArgValues(string values, string[] flags)
            {
                var matches = MultiOptionRegex.Match(values);

                var options = new List<string> { matches.Groups[1].Value };

                if (matches.Groups.Count > 2)
                    options.Add(matches.Groups[2].Value);

                return options;
            }

            #endregion MultiOption

            #region SavingThrow

            private static readonly Regex SavingThrowRegex = new Regex(@"DC (\d+) (\S+) saving throw");

            public static object ParseSavingThrowArgValues(string values, string[] flags)
            {
                var matches = SavingThrowRegex.Match(values);

                return new SavingThrowArgs
                {
                    DC = matches.Groups[1].Value.ToInt(),
                    Attribute = matches.Groups[2].Value,
                };
            }

            #endregion SavingThrow

            #region Dropdown

            public static object ParseDropdownValue(string values, string[] flags)
                => values;

            #endregion Dropdown

            public static object ParseTextArgValue(string values, string[] flags)
                => values;

            public static object ParseNumberArgValue(string values, string[] flags)
                => values.ToInt();
        }
    }
}
