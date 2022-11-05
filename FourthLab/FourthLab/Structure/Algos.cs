using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthLab.Structure
{
    internal class Algos
    {
        public enum Algo
        {
            KarpRabins = 0,
            Dijkstras = 1,
            Prims = 2
        };

        public static (List<int>, int) KarpRabinAlgo((string, string) strings) { return (new List<int>(), 0); }

        public static List<int> DijkstraAlgo(List<List<int>> graph) { return new List<int>(); }

        public static List<int> PrimsAlgo(List<List<int>> graph) { return new List<int>(); }

    }
}