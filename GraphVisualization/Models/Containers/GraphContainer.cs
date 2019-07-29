using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphVisualization.Models
{
    public class GraphContainer
    {
        public List<NodeContainer> nodes { get; set; } = new List<NodeContainer>();
        public List<EdgeContainer> edges { get; set; } = new List<EdgeContainer>();
        
    }
}