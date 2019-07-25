using System;
using System.Diagnostics;

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
            if (currentNode.Exploring) return new State { ReachState = ReachState.UNREACHABLE };

            switch (currentNode.State.ReachState)
            {
                case ReachState.UNREACHABLE:
                    if (currentDistance >= currentNode.Distance)
                    {
                        return currentNode.State;
                    }
                    break;
                //case ReachState.REACHED:
                //    if (currentDistance + currentNode.State.DistanceToTarget <= maxDistance)
                //    {
                //        break;
                //        return currentNode.State;
                //    }
                //    else
                //    {
                //        return new State { ReachState = ReachState.UNREACHABLE };
                //    }
            }
            if (currentNode.Equals(target))
            {
                currentNode.State.ReachState = ReachState.REACHED;
                currentNode.State.DistanceToTarget = 0;
                currentNode.Distance = Math.Min(currentNode.Distance, currentDistance);
                return currentNode.State;
            }

            currentNode.State.ReachState = ReachState.UNREACHABLE;
            currentNode.Distance = Math.Min(currentNode.Distance, currentDistance);
            currentNode.Exploring = true;

            currentNode.Outs.ForEach(edge =>
            {
                if (currentDistance + GetWeight(edge) > maxDistance)
                    return;

                var state = Explore(edge.To, currentDistance + GetWeight(edge));
                if (state.ReachState == ReachState.REACHED)
                {
                    currentNode.State.ReachState = state.ReachState;
                    currentNode.State.DistanceToTarget = Math.Min(currentNode.State.DistanceToTarget, state.DistanceToTarget + GetWeight(edge));
                    result.AddEdge(edge.From.Index, edge.To.Index, edge.Weight);
                }
            });

            currentNode.Exploring = false;
            return currentNode.State;
        }

        private double GetWeight(Edge edge) => findAllPaths ? 1 : edge.Weight;
    }
}
