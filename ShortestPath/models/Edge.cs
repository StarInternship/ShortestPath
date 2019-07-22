namespace ShortestPath.models
{
    public class Edge
    {
        public int From { get; }
        public int To { get; }
        public double Weight { get; }

        public Edge(int from, int to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}