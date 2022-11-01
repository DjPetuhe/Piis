
namespace MinimaxLab.Structure
{
    internal static class Parser
    {
        public static Game ReadMatrix()
        {
            try
            {
                (string, int, int, Game.Algo) values = UserInput();
                return ToBoolMatrix(File.ReadLines(values.Item1).ToList(), values.Item2, values.Item3, values.Item4);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} Try again!");
                return ReadMatrix();
            }
        }

        private static (string, int, int, Game.Algo) UserInput()
        {
            try
            {
                Console.WriteLine("Enter file directory:");
                string? dir = Console.ReadLine();
                Console.WriteLine("Enter matrix height:");
                string? height = Console.ReadLine();
                Console.WriteLine("Enter matrix width:");
                string? width = Console.ReadLine();
                Console.WriteLine("Choose algo:\n(0)Minimax\n(1)MinimaxAlphaBeta\n(2)Negamax\n(3)NegamaxAlphaBeta\n(4)NegaScout");
                string? algo = Console.ReadLine();
                if (dir == null || height == null || width == null || algo == null) throw new ArgumentNullException("Some arguments are null!");
                Game.Algo algos = algo switch
                {
                    "1" => Game.Algo.MinimaxWithPrunings,
                    "2" => Game.Algo.Negamax,
                    "3" => Game.Algo.NegamaxWithPrunings,
                    "4" => Game.Algo.NegaScout,
                    _ => Game.Algo.Minimax
                };
                (string, int, int, Game.Algo) values = (dir, int.Parse(height), int.Parse(width), algos);
                if (values.Item2 < 1 || values.Item3 < 1 || values.Item2 > 500 || values.Item3 > 500) throw new Exception(message: "Too big or too small values!");
                return values;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} Try again!");
                return UserInput();
            }
        }

        private static Game ToBoolMatrix(List<string> listMatrix, int height, int width, Game.Algo algos)
        {
            List<List<bool>> matrix = new();
            (int, int) player = (-1, -1);
            (int, int) enemy = (-1, -1);
            (int, int) finish = (-1, -1);
            if (listMatrix.Count < height) throw new Exception(message: "Enetered matrix height doesn`t fit with the given matrix");
            for (int i = 0; i < height; i++)
            {
                List<bool> line = new();
                if (listMatrix[i].Length < width) throw new Exception(message: "Enetered matrix width doesn`t fit with the given matrix");
                for (int j = 0; j < width; j++)
                {
                    line.Add(listMatrix[i][j] == '1' || listMatrix[i][j] == 'P' || listMatrix[i][j] == 'F' || listMatrix[i][j] == 'E');
                    if (listMatrix[i][j] == 'P') player = (i, j);
                    else if (listMatrix[i][j] == 'E') enemy = (i, j);
                    else if (listMatrix[i][j] == 'F') finish = (i, j);
                }
                matrix.Add(line);
            }
            if (player == (-1, -1) || finish == (-1, -1) || enemy == (-1, -1)) throw new Exception(message: "No starting or ending point!");
            return new Game (matrix, player, enemy, finish, height, width, algos);
        }
    }
}
