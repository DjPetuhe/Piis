
namespace SimplexLab.Structure
{
    internal static class Printer
    {
        public static void PrintUnsolvable()
        {
            Console.WriteLine("System is unsolvable!");
        }
        
        public static void PrintCurrentStep(double[,] A, double[] B, double[] C)
        {
            for (int i = 0; i < C.Length; i++)
            {
                Console.Write(string.Format("{0,5}", $"{Math.Round(C[i], 2)} "));
            }
            Console.WriteLine("|\n" + new string('-', (A.GetLength(1) + 1) * 5));
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++) Console.Write(string.Format("{0,5}", $"{Math.Round(A[i, j], 2)} "));
                Console.Write("| " + string.Format("{0,3}", $"{Math.Round(B[i], 2)}"));
                Console.Write('\n');
            }
            Console.Write('\n');
        }

        public static void PrintResults(double[,] A, double[] B, double[] C)
        {
            PrintCurrentStep(A, B, C);
            Console.WriteLine("Min = " +C.Sum());
        }
    }
}
