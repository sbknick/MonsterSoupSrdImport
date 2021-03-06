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
        static void Main(string[] args)
        {
            var text = File.ReadAllText("./SRD_Monsters.json");


            var monsterQueue = ExtractMonsterDicts(text);
            var monsterSet = ConvertDictsToMonsters(monsterQueue);

            var traits = ConvertTraits(monsterSet);

            Write(monsterSet.OrderBy(m => m.Name).ToList(), "monster-list.json");

            foreach (var trait in traits)
                Console.WriteLine($"Processed trait: {trait.Name}");

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
            
            Write(grouped, "whatsleft.json");
        }


        private static Queue<KeyValuePair<string, Dict>> ExtractMonsterDicts(string text)
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

        private static HashSet<Monster> ConvertDictsToMonsters(Queue<KeyValuePair<string, Dict>> monsterQueue)
        {
            HashSet<Monster> monsterSet = new HashSet<Monster>();

            while (monsterQueue.Count > 0)
            {
                var monsterDict = monsterQueue.Dequeue();

                var monsterInput = ParseMonsterInput(monsterDict);

                var monster = new MonsterMDParser().ParseMDContent(monsterDict.Key, monsterInput.ContentMD);
                monster.Attributes = monsterInput.Attributes;

                monsterSet.Add(monster);
            }

            return monsterSet;
        }

        private static Trait[] ConvertTraits(HashSet<Monster> monsterSet)
        {
            var traits = monsterSet.SelectMany(TraitMDParser.ConvertTraits);
            return traits.OrderBy(t => t.Name).ToArray();
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
