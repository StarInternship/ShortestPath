using ShortestPath.models;
using System;
using System.Diagnostics;

namespace ShortestPath
{
    static class Program
    {
        private static Stopwatch Stopwatch { get; set; }

        static void Main()
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            Graph graph = new GraphReader().ReadGraph(@"../../../TestFiles/newTest.csv ");
            Console.WriteLine("graph read done. duration: " + Stopwatch.ElapsedMilliseconds + " ms.");
            while (true)
            {
                Console.Write("from: ");
                string source = Console.ReadLine();
                Console.Write("dest: ");
                string destination = Console.ReadLine();
                //Console.Write("find all paths: ");
                bool findAllPaths = true; // Console.ReadLine().Equals("1");
                Console.Write("max distance: ");
                int max = int.Parse(Console.ReadLine());
                Console.Write("result name: ");
                string resultName = Console.ReadLine();

                Stopwatch.Restart();
                var result = new PathFinder(graph, source, destination, findAllPaths, max).Find();

                Console.WriteLine("duration: " + Stopwatch.ElapsedMilliseconds + " ms. edges: " + result.AllEdges.Count);

                foreach (var edge in result.AllEdges)
                {
                    ResultWriter.WriteLine(resultName, edge);
                }
            }
        }
    }
}
