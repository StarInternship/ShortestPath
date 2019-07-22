using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            graph.AddEdge(1, 2, 2);
            graph.AddEdge(1, 3, 3);
            graph.AddEdge(2, 3, 1);

            var actual = graph.FindPath(graph.GetNode(0), graph.GetNode(3));
            var expected = new List<Path>
            {
                new Path
                {
                    graph.GetNode(0).Outs[0], graph.GetNode(1).Outs[1]
                },
                new Path
                {
                    graph.GetNode(0).Outs[0], graph.GetNode(1).Outs[0], graph.GetNode(2).Outs[0]
                }
            };

            if (expected.Count != actual.Count)
            {
                Assert.Fail();
            }

            expected.ForEach(path =>
            {
                if(!actual.Contains(path))
                {
                    Assert.Fail();
                }
            });
        }
    }
}