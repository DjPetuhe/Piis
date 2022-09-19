using Pathfinders.Structure;

namespace Pathfinders
{
    class Program
    {
        static void Main()
        {
            Labyrinth l = Parser.ReadMatrix();
            l.ShowLabyrinth();
        }
    }
}