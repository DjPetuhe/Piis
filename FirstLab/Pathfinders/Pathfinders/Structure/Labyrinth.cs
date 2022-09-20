using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinders.Structure
{
    internal class Labyrinth
    {
        public Labyrinth(List<List<bool>> matrix, (int, int) startPoint, (int, int) endPoint, int height, int width)
        {
            this.matrix = matrix;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.height = height;
            this.width = width;
        }
        public readonly List<List<bool>> matrix;
        public readonly (int, int) startPoint;
        public readonly (int, int) endPoint;
        public readonly int height;
        public readonly int width;
        public void ShowLabyrinth()
        {
            Console.WriteLine(new String('#', width + 2));
            for (int i = 0; i < height; i++)
            {
                Console.Write("#");
                for (int j = 0; j < width; j++)
                {
                    if ((i, j) == startPoint) Console.Write("S");
                    else if ((i, j) == endPoint) Console.Write("E");
                    else Console.Write(matrix[i][j] ? "." : "#");
                }
                Console.Write("#\n");
            }
            Console.WriteLine(new String('#', width + 2));
        }
        public void ShowPath(List<(int, int)> path)
        {
            if (path.Count == 0) Console.WriteLine("\nAlgorithm didnt find the path!\n");
            else
            {
                Console.WriteLine(new String('#', width + 2));
                for (int i = 0; i < height; i++)
                {
                    Console.Write("#");
                    for (int j = 0; j < width; j++)
                    {
                        if ((i, j) == startPoint) Console.Write("S");
                        else if ((i, j) == endPoint) Console.Write("E");
                        else if (IsPath(i, j, path)) Console.Write("o");
                        else Console.Write(matrix[i][j] ? "." : "#");
                    }
                    Console.Write("#\n");
                }
                Console.WriteLine(new String('#', width + 2));
            }
        }
        private bool IsPath(int i, int j, List<(int, int)> path)
        {
            foreach (var el in path)
            {
                if (el == (i, j))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
