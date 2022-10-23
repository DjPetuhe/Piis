
namespace MinimaxLab.Structure
{
    internal static class MinimaxAlgos
    {
        public static (GameState, int) Minimax(Game g, GameState current, int depth, bool PlayerTurn)
        {
            if (depth == 0 || current.GameEnded)
            {
                current.UpdateHeuristic(g, depth);
                return (current, current.Heuristic);
            }
            List<GameState> gameStates = current.GenerateNewStates(g, PlayerTurn);
            GameState saved = current;
            if (PlayerTurn)
            {
                int tempHeuristic = int.MinValue;
                foreach (var state in gameStates)
                {
                    (GameState, int) tempState = Minimax(g, state, depth - 1, false);
                    if (tempState.Item2 > tempHeuristic)
                    {
                        tempHeuristic = Math.Max(tempHeuristic, tempState.Item2);
                        saved = state;
                    }
                }
                return (saved, tempHeuristic);
            }
            else
            {
                int tempHeuristic = int.MaxValue;
                foreach (var state in gameStates)
                {
                    (GameState, int) tempState = Minimax(g, state, depth - 1, true);
                    if (tempState.Item2 < tempHeuristic)
                    {
                        tempHeuristic = Math.Min(tempHeuristic, tempState.Item2);
                        saved = state;
                    }
                }
                return (saved, tempHeuristic);
            }
        }

        public static (GameState, int) AlphaBeta(Game g, GameState current, int depth, int alpha, int beta, bool PlayerTurn)
        {
            if (depth == 0 || current.GameEnded)
            {
                current.UpdateHeuristic(g, depth);
                return (current, current.Heuristic);
            }
            List<GameState> gameStates = current.GenerateNewStates(g, PlayerTurn);
            GameState saved = current;
            if (PlayerTurn)
            {
                int tempHeuristic = int.MinValue;
                foreach (var state in gameStates)
                {
                    (GameState, int) tempState = AlphaBeta(g, state, depth - 1, alpha, beta, false);
                    if (tempState.Item2 > tempHeuristic)
                    {
                        tempHeuristic = Math.Max(tempHeuristic, tempState.Item2);
                        saved = state;
                        if (tempHeuristic >= beta) break;
                        alpha = Math.Max(alpha, tempHeuristic);
                    }
                }
                return (saved, tempHeuristic);
            }
            else
            {
                int tempHeuristic = int.MaxValue;
                foreach (var state in gameStates)
                {
                    (GameState, int) tempState = AlphaBeta(g, state, depth - 1, alpha, beta, true);
                    if (tempState.Item2 < tempHeuristic)
                    {
                        tempHeuristic = Math.Min(tempHeuristic, tempState.Item2);
                        saved = state;
                        if (tempHeuristic <= alpha) break;
                        beta = Math.Min(beta, tempHeuristic);
                    }
                }
                return (saved, tempHeuristic);
            }
        }
    }
}
