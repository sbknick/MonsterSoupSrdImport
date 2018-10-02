using System;
using System.Collections.Generic;
using System.Linq;
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
            var perms = GetConditionalPermutations(template);

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
            public (string name, string value, bool isEqual) Condition;
            public string[] NonConditionalText;
            public List<PermutationNode> Children = new List<PermutationNode>();
        }

        private IList<TemplateConditionalPermutation> GetConditionalPermutations(string template)
        {
            var ToplevelConditionalsRegex = new Regex(@"\[([a-zA-Z]+?)(=|!=)(\S+?) (?:[^\[\]]|(?<counter>\[)|(?<-counter>\]))+(?(counter)(?!))\]");
            var ConditionsRegex = new Regex(@"^\[([a-zA-Z]+?)(=|!=)(\S+?) ([\s\S]+)\]$");
            var ConditionalArgsRegex = new Regex(@"\{(?<fullTag>(?<argName>[^\{]+?):(?<argType>YesNo|Dropdown:\[(?<values>.*?)\]):?(?<flags>.*?))\}");

            var conditionalTree = BuildConditionalTree(default((string, string, bool)), template);
            
            var permutations = BuildPermutationOptions(conditionalTree);

            var templates = BuildTemplatesFromPermutations(conditionalTree, permutations);
            
            return templates;


            PermutationNode BuildConditionalTree((string name, string value, bool isEqual) condition, string templateSegment)
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
                            return BuildConditionalTree((
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
            
            IList<IList<PermutationOption>> BuildPermutationOptions(PermutationNode permutationTreeRoot)
            {
                var conditions = GetConditionalOptions(permutationTreeRoot);
                var permutationOptions = GetPermutations(permutationTreeRoot, conditions);

                return permutationOptions;


                IEnumerable<ConditionalOptions> GetConditionalOptions(PermutationNode permutationNode)
                {
                    var options = new List<ConditionalOptions>();

                    for (int i = 0; ; i++)
                    {
                        var conditionalArgs = ConditionalArgsRegex.Matches(permutationNode.NonConditionalText[i]).Cast<Match>().ToList();

                        foreach (var argMatch in conditionalArgs)
                            options.Add(GetConditional(argMatch));

                        if (i == permutationNode.Children.Count)
                            break;

                        var childConditionalArgs = GetConditionalOptions(permutationNode.Children[i]);
                        options.AddRange(childConditionalArgs);
                    }

                    return options;

                    ConditionalOptions GetConditional(Match conditionalMatch)
                    {
                        var argKey = conditionalMatch.Groups["fullTag"].Value;
                        var argName = conditionalMatch.Groups["argName"].Value;
                        var argType = conditionalMatch.Groups["argType"].Value.Split(':')[0];
                        var flags = conditionalMatch.Groups["flags"].Value.Split(':');

                        string[] vals = argType == "YesNo" ? new[] { "Yes", "No" } :
                                        argType == "Dropdown" ? conditionalMatch.Groups["values"].Value.Split(',').Select(v => v.Trim()).ToArray() :
                                        null;

                        return new ConditionalOptions(argKey, argName, argType, flags, vals);
                    }
                }

                IList<IList<PermutationOption>> GetPermutations(PermutationNode permutationNode, IEnumerable<ConditionalOptions> conditionOptions)
                {
                    var results = new List<IList<PermutationOption>>();
                    var stack = new Stack<PermutationOption>();

                    DoRecursive(new Queue<ConditionalOptions>(conditionOptions));

                    return results;


                    void DoRecursive(Queue<ConditionalOptions> queue)
                    {
                        if (queue.Count == 0)
                        {
                            results.Add(stack.Reverse().ToList());
                            return;
                        }

                        queue = new Queue<ConditionalOptions>(queue);

                        var condition = queue.Dequeue();
                        
                        foreach (var opt in condition.Options)
                        {
                            stack.Push(new PermutationOption(condition, opt));
                            DoRecursive(queue);
                            stack.Pop();
                        }
                    }
                }
            }

            IList<TemplateConditionalPermutation> BuildTemplatesFromPermutations(PermutationNode permutationTreeRoot, IList<IList<PermutationOption>> permutationOptions)
            {
                var results = new List<TemplateConditionalPermutation>();

                var argSet = new Dictionary<string, (string, Arg)>();
                var templateStack = new Stack<string>();

                foreach (var perm in permutationOptions)
                {
                    DoRecursive(permutationTreeRoot, perm);

                    results.Add(new TemplateConditionalPermutation
                    {
                        Args = argSet.Select(a => a.Value).ToArray(),
                        Template = string.Join(string.Empty, templateStack.Reverse()),
                    });

                    argSet.Clear();
                    templateStack.Clear();
                }

                return results.Distinct().ToList();

                void DoRecursive(PermutationNode permutationNode, IList<PermutationOption> permutatoes)
                {
                    // check if this node is valid given the current permutato
                    if (permutationNode.Condition.name != null)
                    {
                        var perm = permutatoes.SingleOrDefault(p => p.ArgName == permutationNode.Condition.name);
                        
                        // be sure we keep track of (and build Args for) the permutation args 
                        // that are actually hit in this permutation of the tree...
                        if (perm != null && !argSet.ContainsKey(perm.ArgName))
                        {
                                argSet.Add(perm.ArgName, (perm.Key, new Arg
                                {
                                    key = perm.ArgName,
                                    argType = perm.ArgType,
                                    flags = perm.Flags,
                                    value = perm.SelectedOption
                                }));
                        }

                        if (permutationNode.Condition.isEqual != (perm.SelectedOption == permutationNode.Condition.value))
                        {
                            // don't add anything from this node if it's not in the allowed permutato list
                            return;
                        }
                    }
                    
                    // Do the regular loop & recursing
                    for (int i = 0; ; i++)
                    {
                        templateStack.Push(permutationNode.NonConditionalText[i]);

                        if (i == permutationNode.Children.Count)
                            break;

                        DoRecursive(permutationNode.Children[i], permutatoes);
                    }
                }
            }
        }

        private struct TemplateConditionalPermutation
        {
            public string Template;
            public (string ArgKey, Arg Arg)[] Args;
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
            { "BeastShapes", ArgParser.ParseTextArgValue },
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
