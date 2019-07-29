using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    /// <summary>
    /// A model of Node of a graph
    /// </summary>
    public class Node
    {
        /// <summary>
        /// an string of node index
        /// </summary>
        public string Index { get; }
        /// <summary>
        /// out edges of the node
        /// </summary>
        public List<Edge> Outs { get; } = new List<Edge>();
        /// <summary>
        /// reach and distance to target state of node
        /// </summary>
        public State State { get; } = new State();
        /// <summary>
        /// distance between source and this node
        /// </summary>
        public double Distance { get; set; } = double.MaxValue;
        /// <summary>
        /// in edges of this node in discovered shortest paths.
        /// </summary>
        public List<Edge> LastInEdges { get; set; } = new List<Edge>();
        /// <summary>
        /// true if this node is exploring with path finder
        /// </summary>
        public bool Exploring { get; set; }

        /// <summary>
        /// sets index of node
        /// </summary>
        /// <param name="index"></param>
        public Node(string index) => Index = index;

        /// <summary>
        /// resets node properties for searching again
        /// </summary>
        public void Reset()
        {
            State.ReachState = ReachState.NOT_VISITTED;
            State.DistanceToTarget = double.MaxValue;
            Distance = Double.MaxValue;
            LastInEdges = new List<Edge>();
        }

        /// <summary>
        /// adds an out edge to the node
        /// </summary>
        /// <param name="edge">out edge</param>
        public void AddOut(Edge edge) => Outs.Add(edge);

        /// <summary>
        /// adds an in edge of this node in current discovered shortest path.
        /// </summary>
        /// <param name="edge">in edge</param>
        public void AddLastInEdge(Edge edge) => LastInEdges.Add(edge);

        /// <summary>
        /// clears and add an edge when a shorter path is discovered
        /// </summary>
        /// <param name="edge">new edge</param>
        public void RecreateInEdges(Edge edge)
        {
            LastInEdges.Clear();
            LastInEdges.Add(edge);
            Distance = edge.From.Distance + edge.Weight;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || !(obj is Node)) return false;
            return Index.Equals(((Node)obj).Index);
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode();
        }

        public override string ToString() => Index;
    }

    /// <summary>
    /// reach and distance to target state of node
    /// </summary>
    public class State
    {
        /// <summary>
        /// distance of node to target
        /// </summary>
        public double DistanceToTarget { get; set; } = double.MaxValue;
        /// <summary>
        /// reach state
        /// </summary>
        public ReachState ReachState { get; set; } = ReachState.NOT_VISITTED;
    }

    public enum ReachState
    {
        NOT_VISITTED, UNREACHABLE, REACHED
    }
}
