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
            Path path = graph.FindePath(graph.GetNode(1), graph.GetNode(4));
            Console.WriteLine(path.Distance);
            Console.ReadLine();
        }
    }
}
