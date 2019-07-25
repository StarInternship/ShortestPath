using ShortestPath.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShortestPath
{
    static class Program
    {
      public static Stopwatch stopwatch { get; set; }

        static void Main(string[] args)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Graph graph = new GraphReader().Read(@"../../../TestFiles/K4.txt");
            Console.WriteLine("graph read done. duration: " + stopwatch.ElapsedMilliseconds + " ms.");
            while (true)
            {
                Console.Write("from: ");
                string source = Console.ReadLine();
                Console.Write("dest: ");
                string destination = Console.ReadLine();
                Console.Write("max distance: ");
                int max = int.Parse(Console.ReadLine());

                stopwatch.Restart();
                var result = graph.FindPaths(source, destination, true);

                Console.WriteLine("duration: " + stopwatch.ElapsedMilliseconds + " ms. edges: ");

                foreach (var node in result.Nodes.Values)
                {
                    node.Outs.ForEach(edge => Console.WriteLine(edge));
                }
            }
        }
    }
}
