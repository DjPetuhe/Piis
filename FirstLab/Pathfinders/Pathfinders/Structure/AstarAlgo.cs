using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinders.Structure
{
    internal class AstarAlgo
    {
        private Labyrinth l;
        private List<List<bool>> visited;
        public AstarAlgo(Labyrinth l)
        {
            this.visited = new();
            this.l = l;
            foreach (var matrixLine in l.matrix)
            {
                List<bool> line = new();
                foreach (var _ in matrixLine)
                {
                    line.Add(false);
                }
                visited.Add(line);
            }
        }
        class Cell
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int G { get; set; }
            public int H { get; set; }
            public Cell? Parent;
            public bool IsStart { get; set; } = false;
            public Cell(int y, int x, (int,int) endPoint, Cell? parent = null)
            {
                this.X = x;
                this.Y = y;
                if (parent is not null)
                {
                    this.Parent = parent;
                    this.H = (Math.Abs(endPoint.Item2 - x) + Math.Abs(endPoint.Item1 - y)) * 10;
                    if (Math.Abs(parent.X - this.X) + Math.Abs(parent.Y - this.Y) == 2) this.G = parent.G + 14;
                    else this.G = parent.G + 10;
                }
                else this.IsStart = true;
            }
            public Cell() { }
        }
        
        public List<(int, int)> FindPath()
        {
            List<Cell> nodes = new();
            nodes.Add(new Cell(l.startPoint.Item1, l.startPoint.Item2, l.endPoint));
            bool finished = false;
            Cell last = new ();
            while (nodes.Count > 0 && !finished)
            {
                Cell current = nodes.OrderBy(x => x.G + x.H).First();
                visited[current.Y][current.X] = true;
                nodes.Remove(current);
                List<Cell> neighbors = GetUnmarkedNeighbors(current);
                foreach (var neighbor in neighbors)
                {
                    if ((neighbor.Y, neighbor.X) == l.endPoint)
                    {
                        finished = true;
                        last = neighbor;
                    }
                    bool add = true;
                    foreach (var node in nodes)
                    {
                        if ((neighbor.Y, neighbor.X) == (node.Y, node.X))
                        {
                            add = false;
                            if ((neighbor.H + neighbor.G) < (node.H + node.G))
                            {
                                node.H = neighbor.H;
                                node.G = neighbor.G;
                                node.Parent = neighbor.Parent;
                            }
                        }
                    }
                    if (add) nodes.Add(neighbor);
                }
            }
            List<(int, int)> path = new();
            if (finished)
            {
                Cell current = last;
                path.Add((current.Y, current.X));
                while (current.Parent is not null)
                {
                    current = current.Parent;
                    path.Add((current.Y, current.X));
                }
            }
            return path;
        }
        private bool IsFine(int i, int j)
        {
            if (i < 0 || j < 0) return false;
            if (i > l.height - 1 || j > l.width - 1) return false;
            if (visited[i][j] || !l.matrix[i][j]) return false;
            return true;
        }
        private List<Cell> GetUnmarkedNeighbors(Cell curr)
        {
            List<Cell> neighbors = new();
            if (IsFine(curr.Y, curr.X - 1))
            {
                neighbors.Add(new Cell(curr.Y, curr.X - 1, l.endPoint, curr));
            }
            if (IsFine(curr.Y - 1, curr.X))
            {
                neighbors.Add(new Cell(curr.Y - 1, curr.X, l.endPoint, curr));
            }
            if (IsFine(curr.Y, curr.X + 1))
            {
                neighbors.Add(new Cell(curr.Y, curr.X + 1, l.endPoint, curr));
            }
            if (IsFine(curr.Y + 1, curr.X))
            {
                neighbors.Add(new Cell(curr.Y + 1, curr.X, l.endPoint, curr));
            }
            if (IsFine(curr.Y - 1, curr.X - 1) &&
                (l.matrix[curr.Y][curr.X - 1] || l.matrix[curr.Y - 1][curr.X]))
            {
                neighbors.Add(new Cell(curr.Y - 1, curr.X - 1, l.endPoint, curr));
            }
            if (IsFine(curr.Y - 1, curr.X + 1) &&
                (l.matrix[curr.Y - 1][curr.X] || l.matrix[curr.Y][curr.X + 1]))
            {
                neighbors.Add(new Cell(curr.Y - 1, curr.X + 1, l.endPoint, curr));
            }
            if (IsFine(curr.Y + 1, curr.X + 1) &&
                (l.matrix[curr.Y][curr.X + 1] || l.matrix[curr.Y + 1][curr.X]))
            {
                neighbors.Add(new Cell(curr.Y + 1, curr.X + 1, l.endPoint, curr));
            }
            if (IsFine(curr.Y + 1, curr.X - 1) &&
                (l.matrix[curr.Y + 1][curr.X] || l.matrix[curr.Y][curr.X - 1]))
            {
                neighbors.Add(new Cell(curr.Y + 1, curr.X - 1, l.endPoint, curr));
            }
            return neighbors;
        }
    }
}
