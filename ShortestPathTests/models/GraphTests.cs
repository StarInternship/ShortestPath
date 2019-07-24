using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ShortestPath.models.Tests
{
    [TestClass()]
    public class GraphTests
    {
        [TestMethod()]
        public void SimpleGraphShortestPathSearch()
        {
            Graph graph = new Graph();
            graph.AddEdge("0", "1", 1);
            graph.AddEdge("1", "2", 2);
            graph.AddEdge("1", "3", 3);
            graph.AddEdge("2", "3", 1);

            var actual = graph.FindPaths("0", "3", false);
            var expected = new List<Path>
                {
                    new Path
                    {
                        graph.GetNode("1").Outs[1],
                        graph.GetNode("0").Outs[0]
                    },
                    new Path
                    {
                        graph.GetNode("2").Outs[0],
                        graph.GetNode("1").Outs[0],
                        graph.GetNode("0").Outs[0]
                    }
                };

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                CollectionAssert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod()]
        public void ALittleComplicatedShortestPathGraphSearch()
        {
            Graph graph = new Graph();
            graph.AddEdge("0", "1", 1);
            graph.AddEdge("0", "2", 1);
            graph.AddEdge("0", "3", 2);
            graph.AddEdge("2", "3", 1);
            graph.AddEdge("1", "3", 1);
            graph.AddEdge("3", "4", 1);

            var actual = graph.FindPaths("0", "4", false);
            var expected = new List<Path>
                {
                    new Path
                    {
                        graph.GetNode("3").Outs[0],
                        graph.GetNode("0").Outs[2]
                    },
                    new Path
                    {
                        graph.GetNode("3").Outs[0],
                        graph.GetNode("1").Outs[0],
                        graph.GetNode("0").Outs[0]
                    },
                    new Path
                    {
                        graph.GetNode("3").Outs[0],
                        graph.GetNode("2").Outs[0],
                        graph.GetNode("0").Outs[1]
                    }
                };

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                CollectionAssert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod()]
        public void VisitedGraphShortestPathSearch()
        {
            Graph graph = new Graph();
            graph.AddEdge("0", "2", 1);
            graph.AddEdge("0", "1", 10);
            graph.AddEdge("2", "1", 1);
            graph.AddEdge("1", "3", 1);
            graph.AddEdge("3", "4", 20);

            var actual = graph.FindPaths("0", "4", false);
            var expected = new List<Path>
            {
                new Path
                {
                    new Edge(graph.GetNode("3"), graph.GetNode("4"), 20),
                    new Edge(graph.GetNode("1"), graph.GetNode("3"), 1),
                    new Edge(graph.GetNode("2"), graph.GetNode("1"), 1),
                    new Edge(graph.GetNode("0"), graph.GetNode("2"), 1),
                }
            };

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                CollectionAssert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}