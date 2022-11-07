using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace FourthLab.Structure
{
    internal static class Menu
    {
        public enum Algo
        {
            KarpRabins = 0,
            Dijkstras = 1,
            Prims = 2
        };

        public static void Start()
        {
            try
            {
                (Algo, string) input = UserInput();
                switch (input.Item1)
                {
                    case Algo.KarpRabins:
                        (List<string>, string) valuesKR = Parser.ParseStrings(input.Item2);
                        PrintResultsKarpRabing(KarpRabinsAlgo.Solve(valuesKR.Item1, valuesKR.Item2), valuesKR);
                        break;
                    case Algo.Dijkstras:
                        (List<List<int>>, int) valuesD = Parser.ParseOrientedGraph(input.Item2);
                        PrintResultsDijkstra(DijkstrasAlgo.Solve(valuesD.Item1, valuesD.Item2 - 1), valuesD.Item1, valuesD.Item2);
                        break;
                    case Algo.Prims:
                        List<List<int>> valuesP = Parser.ParseUnorientedGraph(input.Item2);
                        PrintResultsPrim(PrimsAlgo.Solve(valuesP), valuesP);
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

        private static (Algo, string) UserInput()
        {
            try
            {
                Console.WriteLine("Choose Algorithm:\n(0) Karp-Rabin\n(1) Dijkstra\n(2) Prim");
                string? algo = Console.ReadLine();
                Console.WriteLine("Enter txt directory:");
                string? dir = Console.ReadLine();
                if (algo == null || dir == null) throw new ArgumentNullException("Argument are null!");
                Algo algos = algo switch
                {
                    "1" => Algo.Dijkstras,
                    "2" => Algo.Prims,
                    _ => Algo.KarpRabins
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

        private static void PrintResultsKarpRabing((List<List<int>>, List<int>) found, (List<string>, string) values)
        {
            Console.WriteLine("Choosen algorithm: Karp-Rabin's\n");
            Console.WriteLine($"Searched: \"{string.Join("\" \"", values.Item1)}\"");
            Console.WriteLine(String.Format("{0,10}", "In: ") + '\"' + values.Item2 + '\"');
            for (int i = 0; i < found.Item2.Count; i++)
            {
                Console.WriteLine(string.Format("{0, -20}", "\nFor searched:") + values.Item1[i]);
                Console.WriteLine($"Amount of matches: {found.Item2[i]}");
                Console.Write(string.Format("{0,-19}","Match indexes: "));
                foreach (var index in found.Item1[i])
                {
                    Console.Write($"{index} ");
                }
                Console.Write('\n');
            }
        }

        private static void PrintResultsDijkstra((List<int>, List<int>) results, List<List<int>> graph, int start)
        {
            Console.WriteLine("Choosen algorithm: Dijkstra's\n");
            Console.WriteLine("Starting oriented graph as a adjecency matrix:\n");
            PrintMatrix(graph);
            Console.WriteLine($"\nStarting vertex: {start}");
            for (int i = 0; i < graph.Count; i++)
            {
                if (i == start - 1) continue;
                Console.WriteLine($"\nvertex {i + 1}");
                Console.WriteLine($"Distance: {results.Item1[i]}");
                Console.WriteLine($"Path from {start} vertex: " + StringPath(results.Item2, i, start - 1));
            }
        }

        private static string StringPath(List<int> parent, int from, int to)
        {
            if (parent[from] == -1) return "no path";
            int i = from;
            string result = ";";
            while (i != to)
            {
                result = $" -> {i + 1}" + result;
                i = parent[i];
            }
            return (to + 1).ToString() + result;
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