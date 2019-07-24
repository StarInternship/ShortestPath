namespace ShortestPath.models
{
    public class Edge
    {
        public Node From { get; }
        public Node To { get; }
        public double Weight { get; }

        public Edge(Node from, Node to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || !(obj is Edge)) return false;

            var other = (Edge)obj;
            return From.Equals(other.From) && To.Equals(other.To) && Weight == other.Weight;
        }
    }
}