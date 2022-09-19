using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                    if ((i, j) == startPoint) Console.Write("s");
                    else if ((i, j) == endPoint) Console.Write("e");
                    else Console.Write(matrix[i][j] == true ? "_" : "#");
                }
                Console.Write("#\n");
            }
            Console.WriteLine(new String('#', width + 2));
        }
    }
}
