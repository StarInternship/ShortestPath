using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            var actual = new PathFinder(graph, "0", "1", true, 3).Find();
            var expected = new ResultGraph();
            expected.AddEdge("0", "1", 1);
            expected.AddEdge("0", "2", 1);
            expected.AddEdge("2", "1", 1);

            Assert.AreEqual(true, expected.AllEdges.SetEquals(actual.AllEdges));
        }

        [TestMethod()]
        public void K5()
        {
            TestGraph("newTest.csv", "2", "1", true, 5, "solved.csv");
        }

        private void TestGraph(string graphPath, string source, string destination, bool findAll, int max, string resultPath) {

            GraphReader reader = new GraphReader();

            Graph graph = reader.ReadGraph(@"../../../TestFiles/" + graphPath);


            var actual = new PathFinder(graph, source, destination, findAll, max).Find();

            ResultGraph expected = reader.ReadGraphResult(@"../../../results/" + resultPath);

            Assert.AreEqual(true, expected.AllEdges.SetEquals(actual.AllEdges));
        }
    }
}