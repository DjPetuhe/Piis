using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace FourthLab.Structure
{
    internal static class Parser
    {
        public static (List<string>, string) ParseStrings(string dir)
        {
            List<string> values = File.ReadAllText(dir).Split("\"").Where((item, index) => index % 2 != 0).ToList();
            if (values.Count < 2) throw new Exception(message: "Wrong file formatting!");
            List<string> searched = values.GetRange(0, values.Count - 1);
            return (searched.OrderBy(x => x.Length).ToList(), values.Last());
        }

        public static (List<List<int>>, int) ParseOrientedGraph(string dir)
        {
            return ParseGraph(dir, true);
        }

        public static List<List<int>> ParseUnorientedGraph(string dir)
        {
            return ParseGraph(dir, false).Item1;
        }

        private static (List<List<int>>, int) ParseGraph(string dir, bool oriented)
        {
            string contest = File.ReadAllText(dir);
            int start = 0;
            if (oriented)
            {
                int sPos = contest.LastIndexOf("Start=") + "Start=".Length;
                int length = contest.IndexOf(";") - sPos;
                start = int.Parse(contest.Substring(sPos, length));
            }
            var values = contest.Replace(" ", "").ReplaceLineEndings("")
                                .Split(new char[] { ',', '.' }).Where(x => x.Length == 4)
                                .Select(x => new { FromNode = ToInt(x[0]), ToNode = ToInt(x[1]), Weight = ToInt(x[3]) });
            int size = Math.Max(values.OrderByDescending(x => x.ToNode).First().ToNode, values.OrderByDescending(x => x.FromNode).First().FromNode);
            List<List<int>> graph = new(Enumerable.Range(0, size).Select(i => Enumerable.Repeat(-1, size).ToList()));
            foreach (var v in values)
            {
                if (oriented)
                {
                    graph[v.FromNode - 1][v.ToNode - 1] = v.Weight;
                }
                else if (graph[v.FromNode - 1][v.ToNode - 1] > v.Weight || graph[v.FromNode - 1][v.ToNode - 1] == -1)
                {
                    graph[v.FromNode - 1][v.ToNode - 1] = v.Weight;
                    graph[v.ToNode - 1][v.FromNode - 1] = v.Weight;
                }
            }
            return (graph, start);
        }

        private static int ToInt(char c) => (int)char.GetNumericValue(c);
    }
}