
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
                    int tempState = AlphaBeta(g, state, depth - 1, alpha, beta, false).Item2;
                    if (tempState > tempHeuristic)
                    {
                        tempHeuristic = tempState;
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
                    int tempState = AlphaBeta(g, state, depth - 1, alpha, beta, true).Item2;
                    if (tempState < tempHeuristic)
                    {
                        tempHeuristic = tempState;
                        saved = state;
                        if (tempHeuristic <= alpha) break;
                        beta = Math.Min(beta, tempHeuristic);
                    }
                }
                return (saved, tempHeuristic);
            }
        }

        public static (GameState, int) Negamax(Game g, GameState current, int depth, bool player, int turn)
        {
            if (depth == 0 || current.GameEnded)
            {
                current.UpdateHeuristic(g, depth);
                return (current, turn * current.Heuristic);
            }
            List<GameState> gameStates = current.GenerateNewStates(g, player);
            int tempHeuristic = int.MinValue;
            GameState saved = current;
            foreach (var state in gameStates)
            {
                int tempState = -Negamax(g, state, depth - 1, !player, -turn).Item2;
                if (tempState > tempHeuristic)
                {
                    tempHeuristic = tempState;
                    saved = state;
                }
            }
            return (saved, tempHeuristic);
        }

        public static (GameState, int) NegamaxAlphaBeta(Game g, GameState current, int depth, int alpha, int beta, bool player, int turn)
        {
            if (depth == 0 || current.GameEnded)
            {
                current.UpdateHeuristic(g, depth);
                return (current, turn * current.Heuristic);
            }
            List<GameState> gameStates = current.GenerateNewStates(g, player);
            int tempHeuristic = int.MinValue;
            GameState saved = current;
            foreach (var state in gameStates)
            {
                int tempState = -NegamaxAlphaBeta(g, state, depth - 1, -beta, -alpha, !player, -turn).Item2;
                if (tempState > tempHeuristic)
                {
                    tempHeuristic = tempState;
                    saved = state;
                }
                alpha = Math.Max(alpha, tempState);
                if (alpha >= beta) break;
            }
            return (saved, tempHeuristic);
        }

        public static (GameState, int) NegaScout(Game g, GameState current, int depth, int alpha, int beta, bool player, int turn)
        {
            if (depth == 0 || current.GameEnded)
            {
                current.UpdateHeuristic(g, depth);
                return (current, turn * current.Heuristic);
            }
            List<GameState> gameStates = current.GenerateNewStates(g, player);
            GameState saved = current;
            for (int i = 0; i < gameStates.Count; i++)
            {
                int tempHeuristic;
                int tempHeuristic2;
                if (i == 0) tempHeuristic = -NegaScout(g, gameStates[i], depth - 1, -beta, -alpha, !player, -turn).Item2;
                else
                {
                    tempHeuristic = -NegaScout(g, gameStates[i], depth - 1, -alpha - 1, -alpha, !player, -turn).Item2;
                    if (tempHeuristic > alpha && tempHeuristic < beta && depth > 0)
                    {
                        tempHeuristic2 = -NegaScout(g, gameStates[i], depth - 1, -beta , -tempHeuristic, !player, -turn).Item2;
                        if (tempHeuristic2 > tempHeuristic)
                        {
                            tempHeuristic = tempHeuristic2;
                        }
                    }
                }
                if (alpha < tempHeuristic)
                {
                    alpha = tempHeuristic;
                    saved = gameStates[i];
                }
                if (alpha >= beta) break;
            }
            return (saved, alpha);
        }
    }
}
