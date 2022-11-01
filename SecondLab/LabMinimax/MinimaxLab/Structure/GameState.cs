
namespace MinimaxLab.Structure
{
    internal class GameState
    {
        public (int, int) Player { get; set; }
        public (int, int) Enemy { get; set; }
        public int Heuristic { get; private set; } = 0;
        public bool GameEnded { get; private set; } = false;

        public GameState((int,int) player, (int,int) enemy, Game g)
        {
            this.Player = player;
            this.Enemy = enemy;
            if (Player == g.finish || Player == Enemy) GameEnded = true;
        }

        public void UpdateHeuristic(Game g, int depth)
        {
            Heuristic = CalculateHeuristic(g, depth);
        }

        private int CalculateHeuristic(Game g, int depth)
        {
            if (Player == g.finish)
            {
                return 10000 + depth;
            }
            if (Player == Enemy)
            {
                return -10000 - depth;
            }
            LiAlgo.FindMove(g, Player, g.finish, true, out int distToFinish);
            LiAlgo.FindMove(g, Player, Enemy, true, out int distToEnemy);
            return (distToEnemy * distToFinish - (int)Math.Pow(distToFinish, 2)) - (distToEnemy * distToFinish - (int)Math.Pow(distToEnemy, 2));
        }

        public List<GameState> GenerateNewStates(Game g, bool PlayerTurn)
        {
            List<GameState> newStates = new();
            if (PlayerTurn)
            {
                List<(int, int)> newPos = GenerateNewPos(g, Player);
                foreach (var pos in newPos)
                {
                    newStates.Add(new GameState(pos, Enemy, g));
                }
            }
            else
            {
                List<(int, int)> newPos = GenerateNewPos(g, Enemy);
                foreach (var pos in newPos)
                {
                    newStates.Add(new GameState(Player, pos, g));
                }
            }
            return newStates;
        }

        private static bool Possible(Game g, (int, int) curr)
        {
            if (curr.Item1 < 0 || curr.Item2 < 0) return false;
            if (curr.Item1 > g.height - 1 || curr.Item2 > g.width - 1) return false;
            if (!g.matrix[curr.Item1][curr.Item2]) return false;
            return true;
        }

        private static List<(int, int)> GenerateNewPos(Game g, (int, int) oldPos)
        {
            List<(int, int)> poses = new();
            List<int> x_axis = new() { -1, 0, 1, 0 };
            List<int> y_axis = new() { 0, -1, 0, 1 };
            for (int i = 0; i < x_axis.Count; i++)
            {
                if (Possible(g, (oldPos.Item1 + y_axis[i], oldPos.Item2 + x_axis[i])))
                {
                    poses.Add((oldPos.Item1 + y_axis[i], oldPos.Item2 + x_axis[i]));
                }
            }
            return poses;
        }
    }
}
