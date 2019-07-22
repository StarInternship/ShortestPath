﻿using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    public class Graph
    {
        private readonly Node[] nodes;

        public Graph(int n)
        {
            nodes = new Node[n];
            for (int i = 0; i < n; i++)
            {
                nodes[i] = new Node(i);
            }
        }

        public Node GetNode(int index) => nodes[index];

        public void AddEdge(int from, int to, double weight) => GetNode(from).AddEgde(new Edge(from, to, weight));

        public List<Path> FindPath(int src, int dest)
        {
            return FindPath(GetNode(src), GetNode(dest));
        }

        public List<Path> FindPath(Node source, Node target)
        {
            Reset();
            source.Distance = 0;
            PriorityQueue currentNodes = new PriorityQueue();
            currentNodes.Add(source);

            while (!currentNodes.IsEmpty())
            {
                Node node = currentNodes.Pop();

                if (node.Visited) continue;
                node.Visited = true;

                node.Outs.ForEach(edge => UpdateEdgeDestination(target, currentNodes, node, edge));
            }
            return CreatePaths(source, target);
        }

        private void Reset()
        {
            foreach (Node node in nodes)
            {
                node.Reset();
            }
        }

        private List<Path> CreatePaths(Node source, Node target)
        {
            List<Path> path = new List<Path>();

            return path;
        }


        private void UpdateEdgeDestination(Node target, PriorityQueue currentNodes, Node node, Edge edge)
        {
            if (PosiblePath(target, node, edge))
            {
                if (IsAShorterPath(node, edge)) {
                    currentNodes.Add(GetNode(edge.To));
                    GetNode(edge.To).RecreateInEdges(edge , node.Distance + edge.Weight);
                }
                if (IsEqualPath(node, edge)) {
                    GetNode(edge.To).AddInEdge(edge);
                }
            }
        }

        // this path is a shorter path to edge.to?
        private bool IsAShorterPath(Node node, Edge edge)
        {
            return node.Distance + edge.Weight < GetNode(edge.To).Distance;

        }
        private bool IsEqualPath(Node node, Edge edge)
        {
            return node.Distance + edge.Weight == GetNode(edge.To).Distance;
        }

        // if we already found a shorter path to target, it returns false
        private static bool PosiblePath(Node target, Node node, Edge edge)
        {
            return target.Distance > node.Distance + edge.Weight;
        }
    }
}
