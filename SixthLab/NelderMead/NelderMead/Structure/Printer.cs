using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NelderMead.Structure
{
    public static class Printer
    {
        public static void PrintResults(List<Point> simplex)
        {
            foreach (var s in simplex)
            {
                string f = double.IsNegativeInfinity(s.EvaluateFunction()) ? "-inf" : Math.Round(s.EvaluateFunction(), 2).ToString();
                Console.WriteLine($"X:{Math.Round(s.X, 2)}  Y:{Math.Round(s.Y, 2)}  Z:{Math.Round(s.Z, 2)}  F:{f}");
            }
        }
    }
}
