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
            {
                Graph graph = new Graph(4);
                graph.AddEdge(0, 1, 1);
                graph.AddEdge(1, 2, 2);
                graph.AddEdge(1, 3, 3);
                graph.AddEdge(2, 3, 1);

                var actual = graph.FindPath(0, 3);
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

                for (int i = 0; i < expected.Count; i++)
                {
                    CollectionAssert.AreEqual(expected[i], actual[i]);
                }
            }


            {
                Graph graph = new Graph(5);
                graph.AddEdge(0, 1, 1);
                graph.AddEdge(0, 2, 1);
                graph.AddEdge(0, 3, 2);
                graph.AddEdge(2, 3, 1);
                graph.AddEdge(1, 3, 1);
                graph.AddEdge(3, 4, 1);

                var actual = graph.FindPath(0, 4);
                var expected = new List<Path>
                {
                    new Path
                    {
                        graph.GetNode(0).Outs[2], graph.GetNode(3).Outs[0]
                    },
                    new Path
                    {
                        graph.GetNode(0).Outs[0], graph.GetNode(1).Outs[0], graph.GetNode(3).Outs[0]
                    },
                    new Path
                    {
                        graph.GetNode(0).Outs[1], graph.GetNode(2).Outs[0], graph.GetNode(3).Outs[0]
                    }
                };

                if (expected.Count != actual.Count)
                {
                    Assert.Fail();
                }

                for (int i = 0; i < expected.Count; i++)
                {
                    CollectionAssert.AreEqual(expected[i], actual[i]);
                }
            }
        }
    }
}