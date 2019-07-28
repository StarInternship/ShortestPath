using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortestPath.models.Tests
{
    [TestClass()]
    public class GraphTests
    {
        private const string testFilesPath = @"../../../TestFiles/";
        private const string resultFilesPath = @"../../../results/";

        [TestMethod()]
        public void K3AllPathSearch() => 
            TestGraph("K3AllPathSearch.csv", "0", "1", true, 3, "K3AllPathSearch.csv");

        [TestMethod()]
        public void FindAllPaths() => 
            TestGraph("BigGraphAllpathSearch.csv", "2", "1", true, 5, "BigGraphAllpathSearch.csv");

        [TestMethod()]
        public void SimpleGraphShortestPathSearch() => 
            TestGraph("SimpleGraphShortestPathSearch.csv", "0", "3", false, int.MaxValue, "SimpleGraphShortestPathSearch.csv");

        [TestMethod()]
        public void ALittleComplicatedShortestPathGraphSearch() => 
            TestGraph("ALittleComplicatedShortestPathGraphSearch.csv", "0", "4", false, int.MaxValue, "ALittleComplicatedShortestPathGraphSearch.csv");

        [TestMethod()]
        public void VisitedGraphShortestPathSearch() => 
            TestGraph("VisitedGraphShortestPathSearch.csv", "0", "4", false, int.MaxValue, "VisitedGraphShortestPathSearch.csv");

        private void TestGraph(string graphPath, string source, string destination, bool findAll, int max, string resultPath) {

            GraphReader reader = new GraphReader();

            Graph graph = reader.ReadGraph(testFilesPath + graphPath);

            var actual = new PathFinder(graph, source, destination, findAll, max).Find();

            ResultGraph expected = reader.ReadGraphResult(resultFilesPath + resultPath);

            Assert.AreEqual(true, expected.AllEdges.SetEquals(actual.AllEdges));
        }
    }
}