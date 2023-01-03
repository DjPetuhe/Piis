using SimplexLab.Structure;

namespace SimplexLab
{
    class Program
    {
        static void Main()
        {
            double[,] A = new double[3, 5] {
                {2, 2, 1, 1, 1},
                {2, -1, 0, 1, 0},
                {1, 1, 0, 0, 1}
            };
            double[] B = new double[3] { 6, 2, 2 };
            double[] C = new double[5] { -3, 0, -1, 2, -1 };
            Simplex method = new(A, B, C);
            method.Solve(new List<int> { 2, 3, 4});
        }
    }
}