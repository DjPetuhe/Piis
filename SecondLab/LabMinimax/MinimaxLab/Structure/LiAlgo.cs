
namespace MinimaxLab.Structure
{
    internal static class LiAlgo
    {
        private static List<List<int>> MakeWay(Game g, bool isEnemyWall)
        {
            List<List<int>> way = new();
            foreach (var matrixLine in g.matrix)
            {
                List<int> line = new();
                foreach (var el in matrixLine)
                {
                    if (el) line.Add(0);
                    else line.Add(-1);
                }
                way.Add(line);
            }
            if (isEnemyWall) way[g.State.Enemy.Item1][g.State.Enemy.Item2] = -1;
            return way;
        }
        public static (int, int) FindMove(Game g, (int, int) from, (int, int) to, bool isEnemyWall)
        {
            return FindMove(g, from, to, isEnemyWall ,out _);
        }
        
        public static (int, int) FindMove(Game g, (int,int) from, (int,int) to, bool isEnemyWall, out int distance)
        {
            List<List<int>> way = MakeWay(g, isEnemyWall);
            Queue<(int, int)> nodes = new();
            nodes.Enqueue(from);
            bool finished = false;
            while (nodes.Count > 0 && !finished)
            {
                (int, int) current = nodes.Dequeue();
                int d = way[current.Item1][current.Item2];
                List<(int, int)> neighbors = GetUnmarkedNeighbors(g, way, current, d, from);
                foreach (var neighbor in neighbors)
                {
                    if (neighbor == to) finished = true;
                    else nodes.Enqueue(neighbor);
                }
            }
            distance = way[to.Item1][to.Item2];
            if (finished)
            {
                return FindBestNeighbor(g, way, to, way[to.Item1][to.Item2], from);
            }
            return to;
        }
        private static List<(int, int)> GetUnmarkedNeighbors(Game g, List<List<int>> way, (int, int) curr, int d, (int,int) from)
        {
            List<(int, int)> neighbors = new();
            List<int> x_axis = new() { -1, 0, 1, 0 };
            List<int> y_axis = new() { 0, -1, 0, 1 };
            for (int i = 0; i < x_axis.Count; i++)
            {
                if (IsFine(g, way, curr.Item1 + y_axis[i], curr.Item2 + x_axis[i]))
                {
                    neighbors.Add((curr.Item1 + y_axis[i], curr.Item2 + x_axis[i]));
                    way[curr.Item1 + y_axis[i]][curr.Item2 + x_axis[i]] += d + 1;
                }
            }
            if (neighbors.Contains(from))
            {
                neighbors.Remove(from);
                way[from.Item1][from.Item2] = 0;
            }
            return neighbors;
        }
        private static bool IsFine(Game g, List<List<int>> way, int i, int j)
        {
            if (i < 0 || j < 0) return false;
            if (i > g.height - 1 || j > g.width - 1) return false;
            if (!way[i][j].Equals(0)) return false;
            return true;
        }
        private static (int, int) FindBestNeighbor(Game g, List<List<int>> way, (int, int) curr, int d, (int, int) from)
        {
            (int, int) best = (-1, -1);
            if (d == 1) return from;
            if (curr.Item2 - 1 >= 0 &&
                way[curr.Item1][curr.Item2 - 1].Equals(d - 1))
            {
                best = (curr.Item1, curr.Item2 - 1);
            }
            else if (curr.Item1 - 1 >= 0 &&
                way[curr.Item1 - 1][curr.Item2].Equals(d - 1))
            {
                best = (curr.Item1 - 1, curr.Item2);
            }
            else if (curr.Item2 + 1 <= g.width - 1 &&
                way[curr.Item1][curr.Item2 + 1].Equals(d - 1))
            {
                best = (curr.Item1, curr.Item2 + 1);
            }
            else if (curr.Item1 + 1 <= g.height - 1 &&
                way[curr.Item1 + 1][curr.Item2].Equals(d - 1))
            {
                best = (curr.Item1 + 1, curr.Item2);
            }
            return best;
        }
    }
}
