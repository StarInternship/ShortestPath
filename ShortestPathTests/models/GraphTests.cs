using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ShortestPath.models.Tests
{
    [TestClass()]
    public class GraphTests
    {
        [TestMethod()]
        public void K3Test()
        {
            Graph graph = new Graph();
            graph.AddEdge("0", "0", 1);
            graph.AddEdge("0", "1", 1);
            graph.AddEdge("0", "2", 1);
            graph.AddEdge("1", "0", 1);
            graph.AddEdge("1", "1", 1);
            graph.AddEdge("1", "2", 1);
            graph.AddEdge("2", "0", 1);
            graph.AddEdge("2", "1", 1);
            graph.AddEdge("2", "2", 1);

            var actual = new PathFinder(graph, "0", "1", true, 3);
            var expected = new ResultGraph();
        }
    }
}