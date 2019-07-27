using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    public class PathFinder
    {
        private readonly Graph graph;
        private readonly Node source;
        private readonly Node target;
        private readonly bool findAllPaths;
        private readonly double maxDistance;
        private readonly ResultGraph result = new ResultGraph();

        public PathFinder(Graph graph, string source, string target, bool findAllPaths, double maxDistance)
        {
            this.graph = graph;
            this.source = graph.GetNode(source);
            this.target = graph.GetNode(target);
            this.findAllPaths = findAllPaths;
            this.maxDistance = maxDistance;
        }

        public ResultGraph Find()
        {
            graph.Reset();
            Explore(source, 0);
            return result;
        }

        private State Explore(Node currentNode, double currentDistance)
        {

            switch (currentNode.State.ReachState)
            {
                case ReachState.UNREACHABLE:
                    if (currentDistance >= currentNode.Distance)
                    {
                        return currentNode.State;
                    }
                    break;

            }

            if (currentNode.Equals(target))
            {
                return TargetReach(currentNode, currentDistance);
            }

            currentNode.State.ReachState = ReachState.UNREACHABLE;
            currentNode.Distance = currentDistance;
            currentNode.Exploring = true;

            currentNode.Outs.ForEach(edge => ExploreEdge(currentNode, currentDistance, edge));

            currentNode.Exploring = false;
            return currentNode.State;
        }

        private void ExploreEdge(Node currentNode, double currentDistance, Edge edge)
        {
            if (currentDistance + GetWeight(edge) > maxDistance) return;

            if (edge.To.Exploring)
            {
                return;
            }

            var state = Explore(edge.To, currentDistance + GetWeight(edge));

            if (state.ReachState == ReachState.REACHED)
            {
                currentNode.State.ReachState = state.ReachState;
                currentNode.State.DistanceToTarget = Math.Min(currentNode.State.DistanceToTarget, state.DistanceToTarget + GetWeight(edge));
                result.AddEdge(edge.From.Index, edge.To.Index, edge.Weight);
            }
        }

        private static State TargetReach(Node currentNode, double currentDistance)
        {
            currentNode.State.ReachState = ReachState.REACHED;
            currentNode.State.DistanceToTarget = 0;
            currentNode.Distance = currentDistance;
            return currentNode.State;
        }

        private double GetWeight(Edge edge) => findAllPaths ? 1 : edge.Weight;
    }
}
