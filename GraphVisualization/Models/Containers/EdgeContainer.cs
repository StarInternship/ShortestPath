using System.Collections.Generic;
using ShortestPath.models;

namespace GraphVisualization.Models
{
    public class EdgeContainer
    {
        public EdgeAttributes attributes { get; set; }
        public string id { get; set; }
        public string source { get; set; }
        public string target { get; set; }


        public EdgeContainer(Edge edge)
        {
            this.source = edge.From.Index;
            this.target = edge.To.Index;
            this.id = edge.ToString();
            attributes = new EdgeAttributes(edge);
        }

    }
}