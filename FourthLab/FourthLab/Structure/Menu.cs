using System;
using System.ComponentModel;
using System.Collections.Generic;

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
                        (string, string) valuesKR = Parser.ParseStrings(input.Item2);
                        PrintResultsKarpRabing(Algos.KarpRabinAlgo(valuesKR), valuesKR);
                        break;
                    case Algos.Algo.Dijkstras:
                        List<List<int>> valuesD = Parser.ParseOrientedGraph(input.Item2);
                        PrintResultsDijkstra(Algos.DijkstraAlgo(valuesD), valuesD);
                        break;
                    case Algos.Algo.Prims:
                        List<List<int>> valuesP = Parser.ParseUnorientedGraph(input.Item2);
                        PrintResultsPrim(Algos.PrimsAlgo(valuesP), valuesP);
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

        private static (Algos.Algo, string) UserInput()
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
                Console.Clear();
                return (algos, dir);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} Try again!");
                return UserInput();
            }
        }

        private static void PrintResultsKarpRabing((List<int>, int) subStrings, (string, string) values)
        {
            Console.WriteLine("Choosen algorithm: Karp-Rabin's\n");
            Console.WriteLine($"Searched: \"{values.Item1}\"");
            Console.WriteLine(String.Format("{0,10}", "In: ") + '\"' + values.Item2 + '\"');
            //TODO: print results
        }

        private static void PrintResultsDijkstra(List<int> lenghts, List<List<int>> values)
        {
            Console.WriteLine("Choosen algorithm: Dijkstra's\n");
            Console.WriteLine("Starting oriented graph as a adjecency matrix:\n");
            PrintMatrix(values);
            //TODO: print results
        }

        private static void PrintResultsPrim(List<int> mst, List<List<int>> values)
        {
            Console.WriteLine("Choosen algorithm: Prim's\n");
            Console.WriteLine("Starting unoriented graph as a adjecency matrix:\n");
            PrintMatrix(values);
            //TODO: print results
        }

        private static void PrintMatrix(List<List<int>> graph)
        {
            Console.Write("   |");
            for (int i = 0; i < graph.Count; i++)
            {
                Console.Write(string.Format("{0,4}", $"{i + 1} "));
            }
            Console.WriteLine("\n----" + new string('-', graph.Count * 4));
            for (int i = 0; i < graph.Count; i++)
            {
                Console.Write(string.Format("{0,4}", $"{i + 1}|"));
                foreach (var el in graph[i])
                {
                    if (el == -1) Console.Write(string.Format("{0,4}", "inf"));
                    else Console.Write(string.Format("{0,4}", $"{el} "));
                }
                Console.Write('\n');
            }
        }
    }
}