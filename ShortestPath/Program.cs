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
            Graph graph = new GraphReader().Read(@"../../../TestFiles/hossein_test");
            Console.WriteLine("graph read done. duration: " + stopwatch.ElapsedMilliseconds + " ms.");
            while (true)
            {
                Console.Write("from: ");
                int source = int.Parse(Console.ReadLine());
                Console.Write("dest: ");
                int destination = int.Parse(Console.ReadLine());

                stopwatch.Restart();
                var path = graph.FindPath(source, destination);

                if (path.Equals(Path.NOT_FOUND))
                {
                    Console.WriteLine("not found. time: " + stopwatch.ElapsedMilliseconds + " ms.");
                }
                else
                {
                    Console.WriteLine("find a path in " + stopwatch.ElapsedMilliseconds + " ms with distance of " + path.Distance);
                    Console.Write("[" + source + "]");
                    foreach (Edge edge in path)
                    {
                        Console.Write(" -(" + edge.Weight + ")-> [" + edge.To + "]");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
