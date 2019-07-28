using ShortestPath.models;
using System;
using System.Diagnostics;

namespace ShortestPath
{
    static class Program
    {
        private static readonly Stopwatch Stopwatch = new Stopwatch();

        static void Main()
        {
            Stopwatch.Start();
            Graph graph = new GraphReader().ReadGraph(@"../../../TestFiles/BigGraphAllpathSearch.csv");
            Console.WriteLine("graph read done. duration: " + Stopwatch.ElapsedMilliseconds + " ms.");

            while (true)
            {
                Console.Write("from: ");
                string source = Console.ReadLine();
                Console.Write("dest: ");
                string destination = Console.ReadLine();
                Console.Write("find all paths? ");
                bool findAllPaths = !Console.ReadLine().Equals("0");
                int max = int.MaxValue;
                if (findAllPaths)
                {
                    Console.Write("max distance: ");
                    max = int.Parse(Console.ReadLine());
                }
                Console.Write("result name: ");
                var resultWriter = new ResultWriter(Console.ReadLine());

                Stopwatch.Restart();
                var result = new PathFinder(graph, source, destination, findAllPaths, max).Find();

                Console.WriteLine("duration: " + Stopwatch.ElapsedMilliseconds + " ms. edges: " + result.AllEdges.Count);

                foreach (var edge in result.AllEdges)
                {
                    resultWriter.WriteLine(edge);
                }
            }
        }
    }
}
