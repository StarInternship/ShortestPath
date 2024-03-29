﻿using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ShortestPath.models
{
    /// <summary>
    /// A .csv file graph reader
    /// </summary>
    public class GraphReader
    {
        /// <summary>
        /// .csv row pattern
        /// </summary>
        private static readonly Regex regex = new Regex(@"^(.+),(.+),(\d+.?\d*)$");

        /// <summary>
        /// reads a line of the file as an edge of a graph
        /// </summary>
        /// <param name="graph">result graph</param>
        /// <param name="edge">current line</param>
        private static void ReadEdge(Graph graph, string edge)
        {
            var groups = regex.Matches(edge)[0].Groups;

            string source = groups[1].ToString();
            string destination = groups[2].ToString();
            double weight = int.Parse(groups[3].ToString());

            graph.AddEdge(source, destination, weight);
        }

        /// <summary>
        /// reads a graph result for test.
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns>result graph</returns>
        public Graph ReadGraph(string path)
        {
            if (File.Exists(path))
            {
                string[] edges = File.ReadAllLines(path);
                var graph = new Graph();

                foreach (var edge in edges)
                {
                    ReadEdge(graph, edge);
                }

                return graph;
            }
            return new Graph();
        }

    }
}
