using System.Linq;
using System.Collections.Generic;

namespace FourthLab.Structure
{
    internal class PrimsAlgo
    {
        public static List<int> Solve(List<List<int>> graph) 
        {
            List<int> dist = new(Enumerable.Repeat(int.MaxValue, graph.Count).ToList()) { [0] = 0 };
            List<int> parent = Enumerable.Repeat(-1, graph.Count).ToList();
            List<int> nodes = Enumerable.Range(0, graph.Count).ToList();
            while (nodes.Count != 0)
            {
                int curr = nodes.MinBy(i => dist[i]);
                nodes.Remove(curr);
                List<int> neighbors = Enumerable.Range(0, graph.Count).Where(i => graph[curr][i] > 0).ToList();
                foreach (var neighbor in neighbors)
                {
                    if (nodes.Contains(neighbor) && graph[curr][neighbor] < dist[neighbor])
                    {
                        dist[neighbor] = graph[curr][neighbor];
                        parent[neighbor] = curr;
                    }
                }
            }
            return parent;
        }
    }
}
