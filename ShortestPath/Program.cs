using ShortestPath.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Graph graph = new GraphReader().Read(@"C:\Graph\hadi");
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            while (true)
            {
                Console.Write("from: ");
                int src = int.Parse(Console.ReadLine());
                Console.Write("dest: ");
                int dest = int.Parse(Console.ReadLine());
                stopwatch.Restart();
                var path = graph.FindPath(src, dest);
                if (path.Equals(Path.NOT_FOUND))
                {
                    Console.WriteLine("not found " + stopwatch.ElapsedMilliseconds);
                }
                else
                {
                    Console.WriteLine("find a path in " + stopwatch.ElapsedMilliseconds + " with " + path.Distance);
                    Console.Write("(" + src + ")");
                    foreach (Edge edge in path)
                    {
                        Console.Write(" -" + edge.Weight + "-> (" + edge.To + ")");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
