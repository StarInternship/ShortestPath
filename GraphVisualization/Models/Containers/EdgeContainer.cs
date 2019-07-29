using System.Collections.Generic;

namespace GraphVisualization.Models
{
    public class EdgeContainer
    {

        EdgeAttributes attributes { get; set; }
        int id { get; set; }
        int source { get; set; }
        int target { get; set; }

    }
}