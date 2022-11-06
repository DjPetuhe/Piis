using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FourthLab.Structure
{
    internal static class Algos
    {
        public enum Algo
        {
            KarpRabins = 0,
            Dijkstras = 1,
            Prims = 2
        };

        private static readonly int variant = 26;

        public static (List<List<int>>, List<int>) KarpRabinAlgo(List<string> searched, string full) 
        {
            List<int> amounts = new (new int[searched.Count]);
            List<int> searchedHashs = new();
            List<List<int>> indexes = new();
            foreach (var str in searched)
            {
                searchedHashs.Add(HashString(str));
                indexes.Add(new List<int>());
            }
            List<int> sizes = searched.Select(x => x.Length).Distinct().OrderBy(x => x).ToList();
            int minSearchedSize = sizes.First();
            int maxSearchedSize = sizes.Last();
            for (int i = 0; i < full.Length - minSearchedSize; i++)
            {
                foreach (int size in sizes)
                {
                    if (i + size >= full.Length) break;
                    string curr = full[i..(i + size)];
                    int currHash = HashString(curr);
                    if (searchedHashs.Contains(currHash))
                    {
                        var matchedIndexes = Enumerable.Range(0, searchedHashs.Count).Where(i => searchedHashs[i].Equals(currHash)).ToList();
                        foreach (int index in matchedIndexes)
                        {
                            if (CompareStrings(searched[index], curr))
                            {
                                indexes[index].Add(i + 1);
                                amounts[index]++;
                            }
                        }
                    }
                }
            }
            return (indexes, amounts); 
        }

        private static int HashString(string value)
        {
            return value.GetHashCode() % variant;
        }

        private static bool CompareStrings(string cur, string searched)
        {
            if (cur.Length != searched.Length) return false;
            for (int i = 0; i < searched.Length; i++)
            {
                if (!searched[i].Equals(cur[i])) return false;
            }
            return true;
        }

        public static List<int> DijkstraAlgo(List<List<int>> graph) { return new List<int>(); }

        public static List<int> PrimsAlgo(List<List<int>> graph) { return new List<int>(); }

    }
}