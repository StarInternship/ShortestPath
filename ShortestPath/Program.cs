using ShortestPath.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new GraphReader().Read(@"C:\Graph\hadi");
            while (true)
            {
                Console.Write("from: ");
                int src = int.Parse(Console.ReadLine());
                Console.Write("dest: ");
                int dest = int.Parse(Console.ReadLine());
                var path = graph.FindPath(src, dest);
                Console.WriteLine(path.Distance);
            }
        }
    }
}
