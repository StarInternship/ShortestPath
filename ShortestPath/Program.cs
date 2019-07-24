using ShortestPath.models;
using System;
using System.Diagnostics;

namespace ShortestPath
{
    static class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Graph graph = new GraphReader().Read(@"../../../TestFiles/Q20Test.txt");
            Console.WriteLine("graph read done. duration: " + stopwatch.ElapsedMilliseconds + " ms.");
            while (true)
            {
                Console.Write("from: ");
                string source = Console.ReadLine();
                Console.Write("dest: ");
                string destination = Console.ReadLine();

                stopwatch.Restart();
                var paths = graph.FindPath(source, destination);

                if (paths.Count == 0)
                {
                    Console.WriteLine("not found. time: " + stopwatch.ElapsedMilliseconds + " ms.");
                }
                else
                {
                    Console.WriteLine("find " + paths.Count + " paths in " + stopwatch.ElapsedMilliseconds + " ms with distance of " + paths[0].Distance);

                    paths.ForEach(path =>
                    {
                        Console.Write("[" + source + "]");
                        foreach (Edge edge in path)
                        {
                            Console.Write(" -(" + edge.Weight + ")-> [" + edge.To.Index + "]");
                        }
                        Console.WriteLine();
                    });
                }
            }
        }
    }
}
