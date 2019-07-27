using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    public class Node
    {
        public string Index { get; }
        public List<Edge> Outs { get; } = new List<Edge>();
        public State State { get; set; } = new State();
        public double Distance { get; set; } = double.MaxValue;
        public List<Edge> LastInEdges { get; set; } = new List<Edge>();
        public bool Exploring { get; set; }

        public Node(string index) => Index = index;

        public void Reset()
        {
            State.ReachState = ReachState.NOT_VISITTED;
            State.DistanceToTarget = double.MaxValue;
            Distance = Double.MaxValue;
            LastInEdges = new List<Edge>();
        }

        public void AddOut(Edge edge) => Outs.Add(edge);

        public void AddLastInEdge(Edge edge) => LastInEdges.Add(edge);

        public void RecreateInEdges(Edge edge, double newDistance)
        {
            LastInEdges.Clear();
            LastInEdges.Add(edge);
            Distance = newDistance;
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

    public class State
    {
        public double DistanceToTarget { get; set; } = double.MaxValue;
        public ReachState ReachState { get; set; } = ReachState.NOT_VISITTED;
    }

    public enum ReachState
    {
        NOT_VISITTED, UNREACHABLE, REACHED
    }
}
