using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthLab.Structure
{
    internal static class Menu
    {
        public static void Start()
        {
            try
            {
                (Algos.Algo, string) input = UserInput();
                switch (input.Item1)
                {
                    case Algos.Algo.KarpRabins:
                        PrintResultsKarpRabing(Algos.KarpRabinAlgo(Parser.ParseStrings(input.Item2)));
                        break;
                    case Algos.Algo.Dijkstras:
                        PrintResultsDijkstra(Algos.DijkstraAlgo(Parser.ParseOrientedGraph(input.Item2)));
                        break;
                    case Algos.Algo.Prims:
                        PrintResultsPrim(Algos.PrimsAlgo(Parser.ParseUnorientedGraph(input.Item2)));
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} Try again!");
                Start();
            }
        }

        public static (Algos.Algo, string) UserInput()
        {
            try
            {
                Console.WriteLine("Choose Algorithm:\n(0) Karp-Rabin\n(1) Dijkstra\n(2) Prim");
                string? algo = Console.ReadLine();
                Console.WriteLine("Enter txt directory:");
                string? dir = Console.ReadLine();
                if (algo == null || dir == null) throw new ArgumentNullException("Argument are null!");
                Algos.Algo algos = algo switch
                {
                    "1" => Algos.Algo.Dijkstras,
                    "2" => Algos.Algo.Prims,
                    _ => Algos.Algo.KarpRabins
                };
                return (algos, dir);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} Try again!");
                return UserInput();
            }
        }

        public static void PrintResultsKarpRabing((List<int>, int) subStrings)
        {

        }

        public static void PrintResultsDijkstra(List<int> lenghts)
        {

        }

        public static void PrintResultsPrim(List<int> mst)
        {

        }
    }
}