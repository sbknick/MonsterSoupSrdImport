using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Dict = System.Collections.Generic.Dictionary<string, object>;

namespace MonsterSoupSrdImport
{
    class Program
    {
        private static readonly IArgExtractor argExtractor = new ArgExtractor();
        private static readonly TraitMDParser traitParser = new TraitMDParser(argExtractor);

        static void Main(string[] args)
        {
            // Read in Monsters //

            var text = File.ReadAllText("./SRD_Monsters.json");

            var monsterSet = ProcessMonsterFile(text);

            // Write output files //

            Write(monsterSet.OrderBy(m => m.Name).ToList(), "monster-list.json");


            // Dev Bookkeeping... NOT TO BE KEPT IN PRODUCTION //

            var traits = monsterSet.SelectMany(m => m.Traits.Select(t => t.Name)).Distinct().OrderBy(t => t);

            foreach (var trait in traits)
                Console.WriteLine($"Processed trait: {trait}");

            var whatsLeft = (
                from m in monsterSet
                where !string.IsNullOrWhiteSpace(m.WhatsLeft)
                select new
                {
                    m.Name,
                    WhatsLeft = m.WhatsLeft.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries),
                }
                ).ToList();

            var whatsLeftOut = whatsLeft.SelectMany(wl => wl.WhatsLeft).Select(split);

            Tuple<string, string> split(string input)
            {
                var match = new Regex(@"^\*{3}([\s\S]+)\.\*{3} ([\s\S]+)$").Match(input);
                return new Tuple<string, string>(match.Groups[1].Value, match.Groups[2].Value);
            }

            var grouped = from wlo in whatsLeftOut
                          group wlo by wlo.Item1 into wlog
                          where !string.IsNullOrWhiteSpace(wlog.Key)
                          select new
                          {
                              wlog.Key,
                              Descs = wlog.Select(wl => wl.Item2).ToArray()
                          };
            
            // Write Output for What's Left! //

            Write(grouped, "whatsleft.json");
        }

        private static IEnumerable<Monster> ProcessMonsterFile(string text)
        {
            var monsterOutputs = new HashSet<Monster>();
            var monsterInputs = ExtractMonsterDicts(text);

            foreach (var monsterDict in monsterInputs)
            {
                var monsterInput = ParseMonsterInput(monsterDict);

                var monster = new MonsterMDParser().ParseMDContent(monsterDict.Key, monsterInput.ContentMD);
                monster.Attributes = monsterInput.Attributes;

                monster.Traits = traitParser.ConvertTraits(monster);

                monsterOutputs.Add(monster);
            }

            return monsterOutputs;
        }
        
        private static IEnumerable<KeyValuePair<string, Dict>> ExtractMonsterDicts(string text)
        {
            Dict dataDict = JsonConvert.DeserializeObject<Dict>(text);

            Queue<Dict> dictProcessQueue = new Queue<Dict>();
            Queue<KeyValuePair<string, Dict>> monsterQueue = new Queue<KeyValuePair<string, Dict>>();

            dictProcessQueue.Enqueue(dataDict);

            while (dictProcessQueue.Count > 0)
            {
                var toProcess = dictProcessQueue.Dequeue();

                foreach (var kvp in toProcess)
                {
                    if (kvp.Key.Contains("Template"))
                        continue;

                    var valDict = JsonConvert.DeserializeObject<Dict>(JsonConvert.SerializeObject(kvp.Value));

                    var isMonster = valDict.ContainsKey("content");

                    if (isMonster)
                    {
                        monsterQueue.Enqueue(new KeyValuePair<string, Dict>(kvp.Key, valDict));
                    }
                    else
                    {
                        dictProcessQueue.Enqueue(valDict);
                    }
                }
            }

            return monsterQueue;
        }
        
        private static MonsterInput ParseMonsterInput(KeyValuePair<string, Dict> monsterDict)
        {
            var sb = new StringBuilder();

            int[] attributes = null;

            JContainer container = JArray.Parse(JsonConvert.SerializeObject(monsterDict.Value["content"]));

            foreach (var row in container)
            {
                if (row.Type == JTokenType.String)
                {
                    sb.AppendLine(row.ToString());
                }
                else
                {
                    var tableBit = row["table"];
                    var thing1 = tableBit.Select(kvp => kvp.First.First.ToString());
                    attributes = thing1.Select(t => t.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]).Select(t => Convert.ToInt32(t)).ToArray();
                }
            }

            return new MonsterInput
            {
                Attributes = attributes,
                ContentMD = sb.ToString(),
            };
        }
        
        private static void Write(object obj, string filename)
        {
            var output = JsonConvert.SerializeObject(obj);

            using (var streamWriter = new StreamWriter(filename))
            {
                streamWriter.Write(output);
                streamWriter.Close();
            }
        }
        
        public class MonsterInput
        {
            public string ContentMD { get; set; }
            public int[] Attributes { get; set; } 
        }
    }
}
