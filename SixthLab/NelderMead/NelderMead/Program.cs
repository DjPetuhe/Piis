using NelderMead.Structure;

namespace NelderMead
{
    class Program
    {
        static void Main()
        {
            Point starterPoint = new(1, 1, 1);
            NelderMeadAlgo algo = new(starterPoint, 1400);
            Printer.PrintResults(algo.Solve());
        }
    }
}