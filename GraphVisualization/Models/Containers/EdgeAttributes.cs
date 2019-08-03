using ShortestPath.models;

namespace GraphVisualization.Models
{
    public class EdgeAttributes
    {
        public string text { get; set; }

        public EdgeAttributes(Edge edge)
        {
            this.text = edge.Weight + "";
        }
    }
}