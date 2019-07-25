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
            Graph graph = new GraphReader().Read(@"../../../TestFiles/hossein_test");
            Console.WriteLine("graph read done. duration: " + stopwatch.ElapsedMilliseconds + " ms.");
            while (true)
            {
                Console.Write("from: ");
                string source = Console.ReadLine();
                Console.Write("dest: ");
                string destination = Console.ReadLine();
                //Console.Write("find all paths: ");
                //bool findAllPaths = Console.ReadLine().Equals("1");
                bool findAllPaths = true;
                Console.Write("max distance: ");
                int max = int.Parse(Console.ReadLine());

                stopwatch.Restart();
                var result = new PathFinder(graph, source, destination, findAllPaths, max).Find();

                Console.WriteLine("duration: " + stopwatch.ElapsedMilliseconds + " ms. edges: ");

                foreach (var edge in result.AllEdges)
                {
                    Console.WriteLine(edge);
                }
            }
        }
    }
}
