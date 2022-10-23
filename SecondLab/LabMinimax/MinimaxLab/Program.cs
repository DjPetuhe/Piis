using MinimaxLab.Structure;

namespace MinimaxLab
{
    class Program
    {
        static void Main()
        {
            Game g = Parser.ReadMatrix();
            Console.Clear();
            g.RunGame();
        }
    }
}
//test.txt 3x10
//test2.txt 3x10
//test3.txt 5x10