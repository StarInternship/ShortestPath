using System.Collections.Generic;
using ShortestPath.models;

namespace GraphVisualization.Models
{
    public class EdgeContainer
    {
        EdgeAttributes attributes { get; set; }
        string id { get; set; }
        string source { get; set; }
        string target { get; set; }


        public EdgeContainer(Edge edge)
        {
            this.source = edge.From.Index;
            this.target = edge.To.Index;
            this.id = edge.ToString();

        }

    }
}