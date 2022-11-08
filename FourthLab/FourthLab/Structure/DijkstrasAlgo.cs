using System.Linq;
using System.Collections.Generic;

namespace FourthLab.Structure
{
    internal class DijkstrasAlgo
    {
        public static (List<int>, List<int>) Solve(List<List<int>> graph, int start) 
        {
            List<int> nodes = new(Enumerable.Range(0, graph.Count));
            List<int> parent = new(Enumerable.Repeat(-1, graph.Count).ToList());
            List<int> dist = new(Enumerable.Repeat(int.MaxValue, graph.Count).ToList()) { [start] = 0 };
            while (nodes.Count > 0)
            {
                int curr = nodes.OrderBy(x => dist[x]).First();
                nodes.Remove(curr);
                List<int> neighbors = Enumerable.Range(0, graph.Count).Where(i => graph[curr][i] > 0).ToList();
                for (int i = 0; i < neighbors.Count; i++)
                {
                    if (!nodes.Contains(neighbors[i])) continue;
                    int currDist = dist[curr] + graph[curr][neighbors[i]];
                    if (currDist < dist[neighbors[i]])
                    {
                        dist[neighbors[i]] = currDist;
                        parent[neighbors[i]] = curr;
                    }
                }
            }
            return (dist, parent);
        }
    }
}
