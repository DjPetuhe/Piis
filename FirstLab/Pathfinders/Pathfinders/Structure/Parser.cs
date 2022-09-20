using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinders.Structure
{
    internal class Parser
    {
        public static Labyrinth ReadMatrix()
        {
            try
            {
                (string, int, int) values = UserInput();
                return ToBoolMatrix(File.ReadLines(values.Item1).ToList(), values.Item2, values.Item3);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} Try again!");
                return ReadMatrix();
            }
        }

        static (string, int, int) UserInput()
        {
            try
            {
                Console.WriteLine("Enter file directory:");
                string? dir = Console.ReadLine();
                Console.WriteLine("Enter matrix height:");
                string? height = Console.ReadLine();
                Console.WriteLine("Enter matrix width:");
                string? width = Console.ReadLine();
                if (dir == null || height == null || width == null) throw new ArgumentNullException("Some arguments are null!");
                (string, int, int) values = (dir, int.Parse(height), int.Parse(width));
                if (values.Item2 < 1 || values.Item3 < 1 || values.Item2 > 500 || values.Item3 > 500) throw new Exception(message: "Too big or too small values!");
                return values;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} Try again!");
                return UserInput();
            }
        }
        static Labyrinth ToBoolMatrix(List<string> listMatrix, int height, int width)
        {
            List<List<bool>> matrix = new();
            (int, int) startIndex = (-1, -1);
            (int, int) endIndex = (-1, -1);
            if (listMatrix.Count < height) throw new Exception(message: "Enetered matrix height doesn`t fit with the given matrix");
            for (int i = 0; i < height; i++)
            {
                List<bool> line = new();
                if (listMatrix[i].Length < width) throw new Exception(message: "Enetered matrix width doesn`t fit with the given matrix");
                for (int j = 0; j < width; j++)
                {
                    line.Add(listMatrix[i][j] == '1' || listMatrix[i][j] == 'S' || listMatrix[i][j] == 'E');
                    if (listMatrix[i][j] == 'S') startIndex = (i, j);
                    if (listMatrix[i][j] == 'E') endIndex = (i, j);
                }
                matrix.Add(line);
            }
            if (startIndex == (-1, -1) || endIndex == (-1, -1)) throw new Exception(message: "No starting or ending point!");
            return new Labyrinth(matrix, startIndex, endIndex, height, width);
        }
    }
}
