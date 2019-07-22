using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortestPath.models.Tests
{
    [TestClass()]
    public class GraphTests
    {
        [TestMethod()]
        public void CreatePathTest()
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(1, 3, 1);
            graph.AddEdge(2, 3, 1);

            Path actual = graph.FindPath(graph.GetNode(0), graph.GetNode(3));
            Path expected = new Path();
            expected.AddLast(graph.GetNode(0).Outs[0]);
            expected.AddLast(graph.GetNode(1).Outs[1]);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}