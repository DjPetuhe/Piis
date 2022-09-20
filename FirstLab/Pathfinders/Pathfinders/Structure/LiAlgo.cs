using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinders.Structure
{
    internal class LiAlgo
    {
        private Labyrinth l;
        private List<List<int>> way;
        public LiAlgo(Labyrinth l)
        {
            this.way = new();
            this.l = l;
            foreach (var matrixLine in l.matrix)
            {
                List<int> line = new();
                foreach (var el in matrixLine)
                {
                    if (el) line.Add(0);
                    else line.Add(-1);
                }
                way.Add(line);
            }
        }
        public List<(int, int)> FindPath()
        {
            Queue<(int, int)> nodes = new();
            nodes.Enqueue(l.startPoint);
            bool finished = false;
            while (nodes.Count > 0 && !finished)
            {
                (int, int) current = nodes.Dequeue();
                int d = way[current.Item1][current.Item2];
                List<(int, int)> neighbors = GetUnmarkedNeighbors(current, d);
                foreach (var neighbor in neighbors)
                {
                    if (neighbor == l.endPoint) finished = true;
                    else nodes.Enqueue(neighbor);
                }
            }
            List<(int, int)> path = new();
            if (finished)
            {
                (int, int) current = l.endPoint;
                path.Add(current);
                do
                {
                    current = FindBestNeighbor(current, way[current.Item1][current.Item2]);
                    path.Add(current);
                } while (current != l.startPoint);
            }
            return path;
        }
        private List<(int, int)> GetUnmarkedNeighbors((int, int) curr, int d)
        {
            List<(int, int)> neighbors = new();
            if (curr.Item2 - 1 >= 0 && way[curr.Item1][curr.Item2 - 1].Equals(0))
            {
                neighbors.Add((curr.Item1, curr.Item2 - 1));
                way[curr.Item1][curr.Item2 - 1] += d + 10;
            }
            if (curr.Item1 - 1 >= 0 && way[curr.Item1 - 1][curr.Item2].Equals(0))
            {
                neighbors.Add((curr.Item1 - 1, curr.Item2));
                way[curr.Item1 - 1][curr.Item2] += d + 10;
            }
            if (curr.Item2 + 1 <= l.width - 1 && way[curr.Item1][curr.Item2 + 1].Equals(0))
            {
                neighbors.Add((curr.Item1, curr.Item2 + 1));
                way[curr.Item1][curr.Item2 + 1] += d + 10;
            }
            if (curr.Item1 + 1 <= l.height - 1 && way[curr.Item1 + 1][curr.Item2].Equals(0))
            {
                neighbors.Add((curr.Item1 + 1, curr.Item2));
                way[curr.Item1 + 1][curr.Item2] += d + 10;
            }
            if (curr.Item1 - 1 >= 0 && curr.Item2 - 1 >= 0 &&
                (l.matrix[curr.Item1][curr.Item2 - 1] || l.matrix[curr.Item1 - 1][curr.Item2]) &&
                way[curr.Item1 - 1][curr.Item2 - 1].Equals(0))
            {
                neighbors.Add((curr.Item1 - 1, curr.Item2 - 1));
                way[curr.Item1 - 1][curr.Item2 - 1] += d + 14;
            }
            if (curr.Item1 - 1 >= 0 && curr.Item2 + 1 <= l.width - 1 &&
                (l.matrix[curr.Item1 - 1][curr.Item2] || l.matrix[curr.Item1][curr.Item2 + 1]) &&
                way[curr.Item1 - 1][curr.Item2 + 1].Equals(0))
            {
                neighbors.Add((curr.Item1 - 1, curr.Item2 + 1));
                way[curr.Item1 - 1][curr.Item2 + 1] += d + 14;
            }
            if (curr.Item1 + 1 <= l.height - 1 && curr.Item2 + 1 <= l.width - 1 &&
                (l.matrix[curr.Item1][curr.Item2 + 1] || l.matrix[curr.Item1 + 1][curr.Item2]) &&
                way[curr.Item1 + 1][curr.Item2 + 1].Equals(0))
            {
                neighbors.Add((curr.Item1 + 1, curr.Item2 + 1));
                way[curr.Item1 + 1][curr.Item2 + 1] += d + 14;
            }
            if (curr.Item1 + 1 <= l.height - 1 && curr.Item2 - 1 >= 0 &&
                (l.matrix[curr.Item1 + 1][curr.Item2] || l.matrix[curr.Item1][curr.Item2 - 1]) &&
                way[curr.Item1 + 1][curr.Item2 - 1].Equals(0))
            {
                neighbors.Add((curr.Item1 + 1, curr.Item2 - 1));
                way[curr.Item1 + 1][curr.Item2 - 1] += d + 14;
            }
            if (neighbors.Contains(l.startPoint))
            {
                neighbors.Remove(l.startPoint);
                way[l.startPoint.Item1][l.startPoint.Item2] = 0;
            }
            return neighbors;
        }
        private (int, int) FindBestNeighbor((int, int) curr, int d)
        {
            (int, int) best = (-1, -1);
            if (d <= 1.4) return l.startPoint;
            if (curr.Item2 - 1 >= 0 && way[curr.Item1][curr.Item2 - 1].Equals(d - 10))
            {
                best = (curr.Item1, curr.Item2 - 1);
            }
            else if (curr.Item1 - 1 >= 0 && way[curr.Item1 - 1][curr.Item2].Equals(d - 10))
            {
                best = (curr.Item1 - 1, curr.Item2);
            }
            else if (curr.Item2 + 1 <= l.width - 1 && way[curr.Item1][curr.Item2 + 1].Equals(d - 10))
            {
                best = (curr.Item1, curr.Item2 + 1);
            }
            else  if (curr.Item1 + 1 <= l.height - 1 && way[curr.Item1 + 1][curr.Item2].Equals(d - 10))
            {
                best = (curr.Item1 + 1, curr.Item2);
            }
            if (curr.Item1 - 1 >= 0 && curr.Item2 - 1 >= 0 && way[curr.Item1 - 1][curr.Item2 - 1].Equals(d - 14))
            {
                best = (curr.Item1 - 1, curr.Item2 - 1);
            }
            else if (curr.Item1 - 1 >= 0 && curr.Item2 + 1 <= l.width - 1 && way[curr.Item1 - 1][curr.Item2 + 1].Equals(d - 14))
            {
                best = (curr.Item1 - 1, curr.Item2 + 1);
            }
            else if (curr.Item1 + 1 <= l.height - 1 && curr.Item2 + 1 <= l.width - 1 && way[curr.Item1 + 1][curr.Item2 + 1].Equals(d - 14))
            {
                best = (curr.Item1 + 1, curr.Item2 + 1);
            }
            else if (curr.Item1 + 1 <= l.height - 1 && curr.Item2 - 1 >= 0 && way[curr.Item1 + 1][curr.Item2 - 1].Equals(d - 14))
            {
                best = (curr.Item1 + 1, curr.Item2 - 1);
            }
            return best;
        }
    }
}
