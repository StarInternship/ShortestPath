﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestPath.models
{
    /// <summary>
    /// A class to find path with various options.
    /// </summary>
    public class PathFinder
    {
        /// <summary>
        /// The searching graph.
        /// </summary>
        private readonly Graph graph;
        /// <summary>
        /// The source node of path.
        /// </summary>
        private readonly Node source;
        /// <summary>
        /// The target node of path.
        /// </summary>
        private readonly Node target;
        /// <summary>
        /// A boolean that shows to finder to find shortest paths or all paths.
        /// </summary>
        private readonly bool findAllPaths;
        /// <summary>
        /// If finder is finding all paths, this variable tells it the max distance of the paths.
        /// </summary>
        private readonly double maxDistance;
        /// <summary>
        /// directed path search?
        /// </summary>
        private readonly bool directed;

        /// <summary>
        /// Result subgraph will be saved here.
        /// </summary>
        private readonly Graph result = new Graph();

        /// <summary>
        /// sets all properties of path finder.
        /// </summary>
        /// <param name="graph">The searching graph.</param>
        /// <param name="source">The source node of path.</param>
        /// <param name="target">The target node of path.</param>
        /// <param name="findAllPaths">A boolean that shows to finder to find shortest paths or all paths.</param>
        /// <param name="maxDistance">If finder is finding all paths, this variable tells it the max distance of the paths.</param>
        /// <param name="directed">directed path or not</param>
        public PathFinder(Graph graph, string source, string target, bool findAllPaths, double maxDistance, bool directed)
        {
            this.graph = graph;
            this.source = graph.GetNode(source);
            this.target = graph.GetNode(target);
            this.findAllPaths = findAllPaths;
            this.maxDistance = maxDistance;
            this.directed = directed;
        }

        /// <summary>
        /// this method calculates the requested subgraph containig all edges of intended paths
        /// </summary>
        /// <returns>result subgraph</returns>
        public Graph Find()
        {
            graph.Reset();
            if (findAllPaths)
            {
                ExploreAllPathsWithUndirected(source, 0);
            }
            else
            {
                ExploreShortestPaths();
            }
            return result;
        }

        /// <summary>
        /// explores and finds all shortes paths
        /// </summary>
        private void ExploreShortestPaths()
        {
            source.Distance = 0;
            PriorityQueue currentNodes = new PriorityQueue();
            currentNodes.Add(source);

            while (!currentNodes.IsEmpty())
            {
                Node node = currentNodes.Pop();

                if (node.Exploring)
                    continue;
                node.Exploring = true;

                node.Outs.ForEach(
                    edge => UpdateOutEdgeDestination(currentNodes, edge)
                );
                if (!directed)
                {
                    node.Ins.ForEach(
                        edge => UpdateInEdgeSource(currentNodes, edge)
                    );
                }
            }
            CreateResultSubgraph();
        }

        /// <summary>
        /// updates "From" node of an edge.
        /// </summary>
        /// <param name="currentNodes">queue of current exploring nodes</param>
        /// <param name="edge">exploring edge</param>
        private void UpdateInEdgeSource(PriorityQueue currentNodes, Edge edge)
        {
            if (PosiblePath(edge.To, edge.Weight))
            {
                if (IsAShorterPath(edge.To, edge.From, edge.Weight))
                {
                    edge.From.RecreateOutEdges(edge);
                    currentNodes.Add(edge.From);
                }
                else if (IsEqualPath(edge.To, edge.From, edge.Weight))
                {
                    edge.From.AddLastOutEdge(edge);
                }
            }
        }


        /// <summary>
        /// updates "To" node of an edge.
        /// </summary>
        /// <param name="currentNodes">queue of current exploring nodes</param>
        /// <param name="edge">exploring edge</param>
        private void UpdateOutEdgeDestination(PriorityQueue currentNodes, Edge edge)
        {
            if (PosiblePath(edge.From, edge.Weight))
            {
                if (IsAShorterPath(edge.From, edge.To, edge.Weight))
                {
                    edge.To.RecreateInEdges(edge);
                    currentNodes.Add(edge.To);
                }
                else if (IsEqualPath(edge.From, edge.To, edge.Weight))
                {
                    edge.To.AddLastInEdge(edge);
                }
            }
        }

        /// <summary>
        /// is current exploring path a shorter path than previous path to edge.To?
        /// </summary>
        /// <param name="edge">exploring edge</param>
        /// <returns>true if that's a shorter path</returns>
        private bool IsAShorterPath(Node from, Node to, double weight) => from.Distance + weight < to.Distance;

        /// <summary>
        /// is current exploring path as long as previous path to edge.To?
        /// </summary>
        /// <param name="edge">exploring edge</param>
        /// <returns>true if that's as long as previous path</returns>
        private bool IsEqualPath(Node from, Node to, double weight) => from.Distance + weight == to.Distance;

        /// <summary>
        /// if we already found a shorter path to target, it returns false
        /// </summary>
        /// <param name="edge">exploring edge</param>
        /// <returns>true if we haven't already found a shorter path to target</returns>
        private bool PosiblePath(Node from, double weight) => target.Distance >= from.Distance + weight;

        /// <summary>
        /// create result subgraph for shortest path finding.
        /// </summary>
        private void CreateResultSubgraph()
        {
            var currentNodes = new HashSet<Node> { target };

            while (currentNodes.Count > 0)
            {
                var node = currentNodes.First();
                currentNodes.Remove(node);

                if (result.Exploring(node.Index)) continue;
                result.GetNode(node.Index).Exploring = true;

                node.LastInEdges.ForEach(edge =>
                {
                    result.AddEdge(edge.From.Index, edge.To.Index, edge.Weight);
                    currentNodes.Add(edge.From);
                });
                if (!directed)
                {
                    node.LastOutEdges.ForEach(edge =>
                    {
                        result.AddEdge(edge.From.Index, edge.To.Index, edge.Weight);
                        if (result.Exploring(edge.To.Index)) return;

                        currentNodes.Add(edge.To);
                    });
                }
            }
        }


        private State ExploreAllPathsWithUndirected(Node currentNode, double currentDistance)
        {
            if (currentNode.State.ReachState == ReachState.UNREACHABLE
                && currentDistance >= currentNode.Distance)
            {
                return currentNode.State;
            }

            if (currentNode.Equals(target))
            {
                return ReachedToTarget(currentDistance);
            }

            currentNode.State.ReachState = ReachState.UNREACHABLE;
            currentNode.Distance = currentDistance;

            currentNode.Exploring = true;
            currentNode.Outs.ForEach(edge => ExploreEdgeWithUndirected(currentDistance, edge));
            if (!directed)
                currentNode.Ins.ForEach(edge => ExploreBackWardEdgeWithUndirected(currentDistance, edge));
            currentNode.Exploring = false;

            return currentNode.State;
        }

        /// <summary>
        /// explore current edge, in find all paths
        /// </summary>
        /// <param name="currentDistance">current distance to source</param>
        /// <param name="edge">exploring edge</param>
        private void ExploreEdgeWithUndirected(double currentDistance, Edge edge)
        {
            if (currentDistance + 1 > maxDistance) return;

            if (edge.To.Exploring)
            {
                return;
            }

            var state = ExploreAllPathsWithUndirected(edge.To, currentDistance + 1);

            if (state.ReachState == ReachState.REACHED)
            {
                edge.From.State.ReachState = state.ReachState;
                edge.From.State.DistanceToTarget = Math.Min(edge.From.State.DistanceToTarget, state.DistanceToTarget + 1);
                result.AddEdge(edge.From.Index, edge.To.Index, edge.Weight);
            }
        }

        private void ExploreBackWardEdgeWithUndirected(double currentDistance, Edge edge)
        {
            if (currentDistance + 1 > maxDistance) return;

            if (edge.From.Exploring)
            {
                return;
            }

            var state = ExploreAllPathsWithUndirected(edge.From, currentDistance + 1);

            if (state.ReachState == ReachState.REACHED)
            {
                edge.To.State.ReachState = state.ReachState;
                edge.To.State.DistanceToTarget = Math.Min(edge.To.State.DistanceToTarget, state.DistanceToTarget + 1);
                result.AddEdge(edge.From.Index, edge.To.Index, edge.Weight);
            }
        }



        /// <summary>
        /// explores and finds all paths.
        /// </summary>
        /// <param name="currentNode">current node exploring</param>
        /// <param name="currentDistance">current distance to source</param>
        /// <returns>state of current path</returns>
        private State ExploreAllPaths(Node currentNode, double currentDistance)
        {
            if (currentNode.State.ReachState == ReachState.UNREACHABLE
                && currentDistance >= currentNode.Distance)
            {
                return currentNode.State;
            }

            if (currentNode.Equals(target))
            {
                return ReachedToTarget(currentDistance);
            }

            currentNode.State.ReachState = ReachState.UNREACHABLE;
            currentNode.Distance = currentDistance;

            currentNode.Exploring = true;
            currentNode.Outs.ForEach(edge => ExploreEdge(currentDistance, edge));
            currentNode.Exploring = false;

            return currentNode.State;
        }

        /// <summary>
        /// explore current edge, in find all paths
        /// </summary>
        /// <param name="currentDistance">current distance to source</param>
        /// <param name="edge">exploring edge</param>
        private void ExploreEdge(double currentDistance, Edge edge)
        {
            if (currentDistance + 1 > maxDistance) return;

            if (edge.To.Exploring)
            {
                return;
            }

            var state = ExploreAllPaths(edge.To, currentDistance + 1);

            if (state.ReachState == ReachState.REACHED)
            {
                edge.From.State.ReachState = state.ReachState;
                edge.From.State.DistanceToTarget = Math.Min(edge.From.State.DistanceToTarget, state.DistanceToTarget + 1);
                result.AddEdge(edge.From.Index, edge.To.Index, edge.Weight);
            }
        }

        /// <summary>
        /// change target state to REACHED
        /// </summary>
        /// <param name="distance">distance of current path to target</param>
        /// <returns>state of target node</returns>
        private State ReachedToTarget(double distance)
        {
            target.State.ReachState = ReachState.REACHED;
            target.State.DistanceToTarget = 0;
            target.Distance = distance;
            return target.State;
        }
    }
}
