using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NelderMead.Structure
{
    public class NelderMeadAlgo
    {
        const int N = 3;
        const int T = 1;
        const double ALPHA = 1;
        const double BETA_BOT = 0.4;
        const double BETA_UP = 0.6;
        const double GAMMA_BOT = 2;
        const double GAMMA_UP = 3;
        const double SIGMA = 0.5;
        const double EPSILON = 0.1;
        static double d1 = T / (N * Math.Pow(2, 0.5)) * (Math.Pow(N + 1, 0.5) + N - 1);
        static double d2 = T / (N * Math.Pow(2, 0.5)) * (Math.Pow(N + 1, 0.5) - 1);

        private Point starterPoint;
        private Point centroid = new();
        private Point reflectionPoint = new();
        private Point expandedPoint = new();
        private Point contractionPoint = new();
        private List<Point> simplex = new();
        private int iterations;

        bool finished;

        public NelderMeadAlgo(Point point, int iterationsNumber)
        {
            starterPoint = point;
            simplex.Add(starterPoint);
            iterations = iterationsNumber;
        }

        public List<Point> Solve()
        {
            finished = false;
            EvaluateSimplex();
            while (!finished && iterations > 0)
            {
                iterations--;
                if (EvaluateReflection()) continue;
                if (EvaluateExpansion()) continue;
                if (EvaluateContraction()) continue;
                for (int i = 1; i < simplex.Count; i++)
                    simplex[i] = simplex[0] + SIGMA * (simplex[i] - simplex[0]);
            }
            return simplex;
        }

        private void EvaluateSimplex()
        {
            for (int i = 1; i < N + 1; i++)
            {
                Point current = new();
                for (int j = 0; j < Point.DIMENSIONS; j++)
                {
                    if (j == i - 1) current[j] = simplex[0][j] + d1;
                    else current[j] = simplex[0][j] + d2;
                }
                simplex.Add(current);
            }
        }

        private bool EvaluateReflection()
        {
            simplex = simplex.OrderBy(p => p.EvaluateFunction()).ToList();
            finished = ShouldStop();
            if (finished) return true;
            centroid = simplex.Take(N).Aggregate((acc, cur) => acc + cur) / N;
            reflectionPoint = centroid + ALPHA * (centroid - simplex.Last());
            if (ReflectionFit())
            {
                simplex[N] = (Point)reflectionPoint.Clone();
                return true;
            }
            return false;
        }

        private bool ReflectionFit()
        {
            if (reflectionPoint.EvaluateFunction() < simplex[0].EvaluateFunction()) return false;
            if (reflectionPoint.EvaluateFunction() >= simplex[N - 1].EvaluateFunction()) return false;
            return true;
        }

        private bool EvaluateExpansion()
        {
            if (reflectionPoint.EvaluateFunction() < simplex[0].EvaluateFunction())
            {
                expandedPoint = centroid + ((GAMMA_UP + GAMMA_BOT) / 2) * (reflectionPoint - centroid);
                if (expandedPoint.EvaluateFunction() < reflectionPoint.EvaluateFunction())
                    simplex[N] = (Point)expandedPoint.Clone();
                else simplex[N] = (Point)reflectionPoint.Clone();
                return true;
            }
            return false;
        }

        private bool EvaluateContraction()
        {
            if (reflectionPoint.EvaluateFunction() < simplex[N].EvaluateFunction())
            {
                contractionPoint = centroid + (BETA_UP + BETA_BOT) / 2 * (reflectionPoint - centroid);
                if (contractionPoint.EvaluateFunction() < reflectionPoint.EvaluateFunction())
                {
                    simplex[N] = (Point)contractionPoint.Clone();
                    return true;
                }
            }
            else
            {
                contractionPoint = centroid + (BETA_UP + BETA_BOT) / 2 * (simplex[N] - centroid);
                if (contractionPoint.EvaluateFunction() < simplex[N].EvaluateFunction())
                {
                    simplex[N] = (Point)contractionPoint.Clone();
                    return true;
                }
            }
            return false;
        }

        private bool ShouldStop()
        {
            double sum = 0;
            for (int i = 0; i < simplex.Count; i++)
            {
                sum += Math.Pow(simplex[i].EvaluateFunction() - centroid.EvaluateFunction(), 2);
            }
            sum *= ((double)1 / (N + 1));
            return Math.Pow(sum, 0.5) <= EPSILON;
        }
    }
}
