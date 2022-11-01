
namespace MinimaxLab.Structure
{
    internal class Game
    {
        public enum Algo
        {
            Minimax = 0,
            MinimaxWithPrunings = 1,
            Negamax = 2,
            NegamaxWithPrunings = 3,
            NegaScout = 4
        };

        public readonly List<List<bool>> matrix;
        public readonly (int, int) finish;
        public readonly int height;
        public readonly int width;
        public readonly Algo algo;
        public GameState State { get; private set; }

        public Game(List<List<bool>> matrix, (int, int) player, (int, int) enemy, (int, int) finish, int height, int width, Algo algo)
        {
            this.matrix = matrix;
            this.finish = finish;
            this.height = height;
            this.width = width;
            this.algo = algo;
            State = new(player, enemy, this);
        }

        public void RunGame()
        {
            ShowLabyrinth();
            Console.WriteLine("\nPress any button to start...");
            Console.ReadKey();
            while(!IsFinished(State))
            {
                System.Threading.Thread.Sleep(1000);
                MovePlayer();
                MoveEnemy();
                Console.Clear();
                ShowLabyrinth();
            }
            Console.WriteLine("\nGame is finished!");
        }

        private void MovePlayer()
        {
            State.Player = algo switch
            {
                Algo.Minimax => MinimaxAlgos.Minimax(this, State, 5, true).Item1.Player,
                Algo.MinimaxWithPrunings => MinimaxAlgos.AlphaBeta(this, State, 5, int.MinValue + 1, int.MaxValue, true).Item1.Player,
                Algo.Negamax => MinimaxAlgos.Negamax(this, State, 5, true, 1).Item1.Player,
                Algo.NegamaxWithPrunings => MinimaxAlgos.NegamaxAlphaBeta(this, State, 5, int.MinValue + 1, int.MaxValue, true, 1).Item1.Player,
                Algo.NegaScout => MinimaxAlgos.NegaScout(this, State, 5, int.MinValue + 1, int.MaxValue, true, 1).Item1.Player,
                _ => throw new NotImplementedException()
            };
        }

        private void MoveEnemy()
        {
            State.Enemy = LiAlgo.FindMove(this, State.Player, State.Enemy, false);
        }

        private bool IsFinished(GameState current)
        {
            return current.Player == finish || current.Player == current.Enemy;
        }

        private void ShowLabyrinth()
        {
            Console.WriteLine(new String('#', width + 2));
            for (int i = 0; i < height; i++)
            {
                Console.Write("#");
                for (int j = 0; j < width; j++)
                {
                    if ((i, j) == State.Player && (i, j) == State.Enemy) Console.Write("L");
                    else if ((i, j) == State.Player && (i, j) == finish) Console.Write("W");
                    else if ((i, j) == State.Player) Console.Write("P");
                    else if ((i, j) == State.Enemy) Console.Write("E");
                    else if ((i, j) == finish) Console.Write("F");
                    else Console.Write(matrix[i][j] ? " " : "#");
                }
                Console.Write("#\n");
            }
            Console.WriteLine(new String('#', width + 2));
        }
    }
}
