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

            var actual = graph.FindShortestPaths("0", "3");
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

            var actual = graph.FindShortestPaths("0", "4");
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

            var actual = graph.FindShortestPaths("0", "4");
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

        [TestMethod()]
        public void SimpleAllPathsSearch()
        {
            Graph graph = new Graph();
            graph.AddEdge("1", "2", 1);
            graph.AddEdge("1", "4", 1);
            graph.AddEdge("2", "2", 1);
            graph.AddEdge("2", "3", 1);
            graph.AddEdge("3", "1", 1);
            graph.AddEdge("3", "6", 1);
            graph.AddEdge("4", "5", 1);
            graph.AddEdge("4", "6", 1);
            graph.AddEdge("5", "4", 1);
            graph.AddEdge("5", "6", 1);
            graph.AddEdge("6", "7", 1);

            var actual = graph.FindAllPaths("1", "7");
            var expected = new List<Path>
            {
                new Path
                {
                    new Edge(graph.GetNode("6"), graph.GetNode("7"), 1),
                    new Edge(graph.GetNode("4"), graph.GetNode("6"), 1),
                    new Edge(graph.GetNode("1"), graph.GetNode("4"), 1),
                },
                new Path
                {
                    new Edge(graph.GetNode("6"), graph.GetNode("7"), 1),
                    new Edge(graph.GetNode("3"), graph.GetNode("6"), 1),
                    new Edge(graph.GetNode("2"), graph.GetNode("3"), 1),
                    new Edge(graph.GetNode("1"), graph.GetNode("2"), 1),
                },
                new Path
                {
                    new Edge(graph.GetNode("6"), graph.GetNode("7"), 1),
                    new Edge(graph.GetNode("5"), graph.GetNode("6"), 1),
                    new Edge(graph.GetNode("4"), graph.GetNode("5"), 1),
                    new Edge(graph.GetNode("1"), graph.GetNode("4"), 1),
                },
            };

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                CollectionAssert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod()]
        public void CompleteGraphAllPathsSearch()
        {
            Graph graph = new Graph();
            graph.AddEdge("0", "0", 1);
            graph.AddEdge("0", "1", 1);
            graph.AddEdge("0", "2", 1);
            graph.AddEdge("0", "3", 1);
            graph.AddEdge("1", "0", 1);
            graph.AddEdge("1", "1", 1);
            graph.AddEdge("1", "2", 1);
            graph.AddEdge("1", "3", 1);
            graph.AddEdge("2", "0", 1);
            graph.AddEdge("2", "1", 1);
            graph.AddEdge("2", "2", 1);
            graph.AddEdge("2", "3", 1);
            graph.AddEdge("3", "0", 1);
            graph.AddEdge("3", "1", 1);
            graph.AddEdge("3", "2", 1);
            graph.AddEdge("3", "3", 1);

            var actual = graph.FindAllPaths("0", "1");
            var expected = new List<Path>
            {
                new Path
                {
                    new Edge(graph.GetNode("0"), graph.GetNode("1"), 1),
                },
                new Path
                {
                    new Edge(graph.GetNode("2"), graph.GetNode("1"), 1),
                    new Edge(graph.GetNode("0"), graph.GetNode("2"), 1),
                },
                new Path
                {
                    new Edge(graph.GetNode("3"), graph.GetNode("1"), 1),
                    new Edge(graph.GetNode("0"), graph.GetNode("3"), 1),
                },
                new Path
                {
                    new Edge(graph.GetNode("2"), graph.GetNode("1"), 1),
                    new Edge(graph.GetNode("3"), graph.GetNode("2"), 1),
                    new Edge(graph.GetNode("0"), graph.GetNode("3"), 1),
                },
                new Path
                {
                    new Edge(graph.GetNode("3"), graph.GetNode("1"), 1),
                    new Edge(graph.GetNode("2"), graph.GetNode("3"), 1),
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