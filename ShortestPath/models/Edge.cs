namespace ShortestPath.models
{
    /// <summary>
    /// A model of a directed and weighted edge of a graph
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// starting node of the edge
        /// </summary>
        public Node From { get; }
        /// <summary>
        /// ending node of the edge
        /// </summary>
        public Node To { get; }
        /// <summary>
        /// weight of the edge
        /// </summary>
        public double Weight { get; }

        /// <summary>
        /// sets all properties
        /// </summary>
        /// <param name="from">starting node of the edge</param>
        /// <param name="to">ending node of the edge</param>
        /// <param name="weight">weight of the edge</param>
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

        public override string ToString() => From + "," + To + "," + Weight;

        public override int GetHashCode() => ToString().GetHashCode();
    }
}