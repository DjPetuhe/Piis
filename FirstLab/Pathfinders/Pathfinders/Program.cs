using Pathfinders.Structure;

namespace Pathfinders
{
    class Program
    {
        static void Main()
        {
            Labyrinth l = Parser.ReadMatrix();
            Console.Clear();
            Console.WriteLine("\nCurrent labyrinth:\n");
            l.ShowLabyrinth();
            LiAlgo alg1 = new(l);
            Console.WriteLine("\nLee algorithm path:\n");
            l.ShowPath(alg1.FindPath());
        }
    }
}
//first.txt 10x20
//second.txt 7x9
//third.txt 3x7