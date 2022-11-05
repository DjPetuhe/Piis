using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthLab.Structure
{
    internal static class Parser
    {
        public static List<List<int>> ParseOrientedGraph(string dir)
        {
            return ParseGraph(dir, true);
        }
        public static List<List<int>> ParseUnorientedGraph(string dir)
        {
            return ParseGraph(dir, false);
        }
        private static List<List<int>> ParseGraph(string dir, bool oriented)
        {
            return new List<List<int>>();
        }
        public static (string, string) ParseStrings(string dir)
        {
            return ("", "");
        }
    }
}